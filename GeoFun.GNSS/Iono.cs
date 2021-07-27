using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GeoFun.MathUtils;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

using Math = System.Math;

namespace GeoFun.GNSS
{
    public class Iono
    {
        public static double PI = 3.1415926535897932;

        /// <summary>
        /// 将stec转换为vtec
        /// </summary>
        /// <param name="stec"></param>
        /// <param name="ele">高度角(弧度)</param>
        /// <param name="earthRadius">地球半径(默认6371000)</param>
        /// <param name="ionoHeight">电离层单层模型高度(默认450000)</param>
        /// <returns></returns>
        public static double STEC2VTEC(double stec, double ele, double earthRadius = 6371100, double ionoHeight = 450000)
        {
            double sinz = Math.Sin(PI / 2d - ele);
            double sinzz = earthRadius / (earthRadius + ionoHeight) * sinz;
            double coszz = Math.Sqrt(1d - sinzz * sinzz);
            return stec * coszz;
        }

        /// <summary>
        /// 计算穿刺点坐标
        /// </summary>
        /// <param name="xSat"></param>
        /// <param name="ySat"></param>
        /// <param name="zSat"></param>
        /// <param name="xRec"></param>
        /// <param name="yRec"></param>
        /// <param name="zRec"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="earthR"></param>
        /// <param name="ionoH"></param>
        public static void CalIPP(double xSat, double ySat, double zSat,
            double xRec, double yRec, double zRec,
            out double x, out double y, out double z,
            double earthR = 63781000, double ionoH = 450000)
        {
            x = y = z = 0d;

            Vector<double> op1 = new DenseVector(new double[] { xRec, yRec, zRec });
            Vector<double> op2 = new DenseVector(new double[] { xSat, ySat, zSat });

            Vector<double> p1p2 = op1 - op2;
            p1p2 = p1p2 / p1p2.L2Norm();

            double a = p1p2.DotProduct(p1p2);
            double b = 2 * p1p2.DotProduct(op1);
            double c = op1.DotProduct(op1) - Math.Pow(earthR + ionoH, 2);

            double t1, t2;
            double delta = b * b - 4 * a * c;
            if (delta < 1e-14) return;

            else
            {
                t1 = (-b + Math.Sqrt(delta)) / 2d / a;
                t2 = (-b - Math.Sqrt(delta)) / 2d / a;

                var oi1 = op1 + t1 * p1p2;
                var oi2 = op1 + t2 * p1p2;

                var i1p2 = op2 - oi1;
                var i2p2 = op2 - oi2;

                double d1 = i1p2.DotProduct(i1p2);
                double d2 = i2p2.DotProduct(i2p2);

                if (d1 < d2)
                {
                    x = oi1[0];
                    y = oi1[1];
                    z = oi1[2];
                }
                else
                {
                    x = oi1[0];
                    y = oi1[1];
                    z = oi1[2];
                }
            }
        }

        /// <summary>
        /// 单站模型
        /// </summary>
        public static void SingleStation(OStation station)
        {
        }

        /// <summary>
        /// 多站模型
        /// </summary>
        /// <param name="stations"></param>
        public static void MultiStations(List<OStation> stations)
        {
        }

        /// <summary>
        /// 多项式模型
        /// </summary>
        /// <param name="frames"></param>
        public static void FitPolynomialModel(List<OFrame> frames, int type = 0, 
            double b0 = 0d, double l0 = 0d,
            int xOrder = 2, int yOrder = 4)
        {
            if (frames is null || frames.Count < 1) return;

            // not all the satellites' DCB will be estimated
            // the index of receiver
            Dictionary<string, int> recIndex = new Dictionary<string, int>();
            // the index of satellite
            Dictionary<string, int> satIndex = new Dictionary<string, int>();

            // 常数矩阵
            List<double> L = new List<double>();
            List<double> l = new List<double>();

            int indexRec = 0;
            int indexSat = 0;
            int obsCnt = 0;
            for (int i = 0; i < frames.Count; i++)
            {
                foreach (var rec in frames[i].Arcs.Keys)
                {
                    if (!recIndex.ContainsKey(rec))
                    {
                        satIndex.Add(rec, indexRec);
                        indexRec++;
                    }

                    // 一个站观测到的所有弧段
                    var arcsPerStation = frames[i].Arcs[rec];
                    foreach (var prn in arcsPerStation.Keys)
                    {
                        // 一个站观测的的一颗卫星的所有弧段
                        var arcsPerSat = arcsPerStation[prn];
                        if (!satIndex.ContainsKey(prn))
                        {
                            satIndex.Add(prn, indexRec);
                            indexSat++;
                        }

                        obsCnt += arcsPerSat.Sum(a => a.Length);

                        foreach (var arcs in arcsPerStation[prn])
                        {
                            for (int j = 0; j < arcs.Length; j++)
                            {
                                L.Add(arcs[j]["SP4"] * 9.52437);
                            }
                        }
                    }
                }
            }

            // 多项式模型系数参数个数
            int pmParaNumPerFrame = (xOrder + 1) * (yOrder + 1);
            int pmParaNum = pmParaNumPerFrame*frames.Count;
            // 接收机DCB系数个数
            int recDCBParaNum = recIndex.Count;
            // 卫星DCB系数个数
            int satDCBParaNum = satIndex.Count;

            // 不估计卫星DCB
            if (type == 1)
            {
                satDCBParaNum = 0;
            }
            // 未知数总个数
            int paraNum = pmParaNum + recDCBParaNum + satDCBParaNum;

            // 设计矩阵
            double[,] B = new double[paraNum, paraNum];

            List<double> coli, colj;

            //初始化数组
            for (int i = 0; i < paraNum; i++)
            {
                for (int j = 0; j < paraNum; j++)
                {
                    B[i, j] = 0d;
                }
            }

            double value = 0d;
            for (int i = 0; i < paraNum; i++)
            {
                // 第i列
                coli = GetColumn(frames, recIndex, satIndex, b0, l0, i, 0);
                for (int j = 0; j < paraNum; j++)
                {
                    // 第j列
                    colj = GetColumn(frames, recIndex, satIndex, b0, l0, j, 0);

                    B[i, j] = Multipy(coli, colj);

                    value = Multipy(coli, L);
                    l.Add(value);
                }
            }

            if (type == 0)
            {
                L.Add(0);
            }

            Matrix<double> mb = DenseMatrix.OfArray(B);
            Vector<double> vl = DenseVector.OfArray(l.ToArray());

            Vector<double> x = mb.Inverse() * vl;

            List<PolynomialIonoModel> pms = new List<PolynomialIonoModel>();
            for(int i =0;i<frames.Count;i++)
            {
                PolynomialIonoModel pm = new PolynomialIonoModel(xOrder, yOrder);
                for(int m = 0; m<=xOrder; m++)
                {
                    for(int n = 0; n<=yOrder; n++)
                    {
                        pm.Factor[m, n] = x[i * pmParaNumPerFrame + m * (yOrder + 1) + n];
                    }
                }
            }

            Dictionary<string, double> recDCB = new Dictionary<string, double>();
            foreach(var kv in recIndex)
            {
                var name = kv.Key;
                int ind = kv.Value;

                double dcb = x[pmParaNum + ind];
                recDCB.Add(name, dcb);
            }

            if (type == 0)
            {
                Dictionary<string, double> satDCB = new Dictionary<string, double>();
                foreach (var kv in satIndex)
                {
                    var prn = kv.Key;
                    var ind = kv.Value;

                    double dcb = x[pmParaNum + recDCBParaNum + ind];
                    satDCB.Add(prn, dcb);
                }
            }
        }

        public static List<double> GetColumn(List<OFrame> frames,
            Dictionary<string, int> recIndex,
            Dictionary<string, int> satIndex,
            double b0, double l0,
            int colInd, int type = 0,
            int xorder = 2, int yorder = 4)
        {
            // 每个时间段的待求参数个数
            int pmParaNumPerFrame = (xorder + 1) * (yorder + 1);
            // 所有待求参数的总个数
            int pmParaNum = pmParaNumPerFrame * frames.Count;
            int recParaNum = recIndex.Count;
            int satParaNum = satIndex.Count;

            int i = 0;
            int j = 0;
            double b, l;
            double fz = 0d;
            double phi = 0d;
            double s = 0d;
            int ri;

            List<double> col = new List<double>(2000000);
            if (colInd <= pmParaNum)
            {
                for (int n = 0; n < frames.Count; n++)
                {
                    int indexSmall = n * pmParaNumPerFrame;
                    int indexLarge = indexSmall + pmParaNum - 1;
                    bool flag = (colInd >= indexSmall && colInd <= indexLarge);

                    var frame = frames[n];
                    foreach (var kv1 in frame.Arcs)
                    {
                        string rec = kv1.Key;
                        var arcsPerRec = kv1.Value;
                        foreach (var kv2 in arcsPerRec)
                        {
                            string prn = kv2.Key;
                            var arcsPerSat = kv2.Value;
                            foreach (var arc in arcsPerSat)
                            {
                                if (flag)
                                {
                                    for (int o = 0; o < arc.Length; o++)
                                    {
                                        b = arc[o].IPP[0];
                                        l = arc[o].IPP[1];
                                        fz = MathHelper.CalIonoFactor(arc[o].Elevation);
                                        GetPhiS(arc[o].IPP[0], arc[o].IPP[1], arc[o].Epoch.TotalSeconds, b0, l0, frame.CenterTime.TotalSeconds, out phi, out s);
                                        col.Add(Math.Pow(phi, i) * Math.Pow(s, j) * fz);
                                    }
                                }
                                else
                                {
                                    for (int o = 0; o < arc.Length; o++)
                                    {
                                        col.Add(0);
                                    }
                                }
                            }
                        }
                    }
                }

                if (type == 0)
                {
                    col.Add(0);
                }

                return col;
            }

            else if (colInd <= pmParaNum + recIndex.Count)
            {
                foreach (var frame in frames)
                {
                    foreach (var kv1 in frame.Arcs)
                    {
                        string rec = kv1.Key;
                        ri = recIndex[rec];
                        bool flag = false;
                        if (pmParaNum + ri == colInd) flag = true;

                        var arcsPerRec = kv1.Value;
                        foreach (var kv2 in arcsPerRec)
                        {
                            string prn = kv2.Key;
                            var arcsPerSat = kv2.Value;
                            foreach (var arc in arcsPerSat)
                            {
                                for (int m = 0; m < arc.Length; m++)
                                {
                                    fz = MathHelper.CalIonoFactor(arc[m].Elevation);
                                    if (flag) col.Add(9.52437 * fz);
                                    else col.Add(0);
                                }
                            }
                        }
                    }
                }

                if (type == 0)
                {
                    col.Add(0);
                }
            }

            else
            {
                foreach (var frame in frames)
                {
                    foreach (var kv1 in frame.Arcs)
                    {
                        string rec = kv1.Key;
                        var arcsPerRec = kv1.Value;
                        foreach (var kv2 in arcsPerRec)
                        {
                            string prn = kv2.Key;
                            ri = satIndex[prn];
                            bool flag = false;
                            if (ri + pmParaNum + recParaNum == colInd - 1)
                            {
                                flag = true;
                            }

                            var arcsPerSat = kv2.Value;
                            foreach (var arc in arcsPerSat)
                            {
                                for (int m = 0; m < arc.Length; m++)
                                {
                                    fz = MathHelper.CalIonoFactor(arc[m].Elevation);
                                    if (flag) col.Add(-9.52437 * fz);
                                    else col.Add(0);
                                }
                            }
                        }
                    }
                }
                if (type == 0)
                {
                    col.Add(1);
                }
            }

            return col;
        }

        private static double Multipy(List<double> coli, List<double> colj)
        {
            if (coli is null || colj is null || coli.Count != colj.Count) return 0d;

            double value = coli.Zip(colj, (ci, cj) => ci * cj).Sum();
            return value;
        }

        public static void GetPhiS(double b, double l, double t, double l0, double b0, double t0, out double phi, out double s)
        {
            phi = (b - b0);
            s = (l - l0) + (t - t0) / 3600d * 15d * Angle.D2R;
        }
    }
}
