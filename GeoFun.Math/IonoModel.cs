using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GeoFun.MathUtils
{
    public class IonoModel
    {
        public virtual void Calculate(double lon, double lat)
        {

        }

        public static bool CalPolynomialModel(
            int degree, int order,
            List<string> stations,
            List<LinkedList<int>> prn,
            List<LinkedList<double>> lat,
            List<LinkedList<double>> lon,
            List<LinkedList<double>> sp4,
            List<LinkedList<double>> ele,
            Dictionary<string, double> satelliteDCB,
            out PolynominalIonoModel pm,
            out Dictionary<string, double> receiverDCB
            )
        {
            pm = new PolynominalIonoModel(2, 4);
            receiverDCB = new Dictionary<string, double>();
            return true;
        }

        /// <summary>
        /// 计算球谐函数模型,同时估计测站和接收机DCB
        /// </summary>
        /// <param name="degree">球谐函数阶数(中国地区一般为9，参见耿长江)</param>
        /// <param name="order">球谐函数次数</param>
        /// <param name="stations">测站名</param>
        /// <param name="prn">卫星编号</param>
        /// <param name="lat">地磁纬度</param>
        /// <param name="lon">日固经度</param>
        /// <param name="sp4">平滑后的P4</param>
        /// <param name="ele">高度角(弧度)</param>
        /// <param name="spm">输出的球谐模型</param>
        /// <param name="receiverDCB">接收机的dcb</param>
        /// <param name="satelliteDCB">卫星的dcb</param>
        /// <returns></returns>
        public static bool CalSphericalHarmonicModel(
            int degree, int order,
            List<string> stations,
            List<LinkedList<int>> prn,
            List<LinkedList<double>> lat,
            List<LinkedList<double>> lon,
            List<LinkedList<double>> sp4,
            List<LinkedList<double>> ele,
            out SphericalHarmonicIonoModel spm,
            out Dictionary<string, double> receiverDCB,
            out Dictionary<string, double> satelliteDCB)
        {
            spm = null;
            receiverDCB = new Dictionary<string, double>();
            satelliteDCB = new Dictionary<string, double>();

            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (sp4 is null || sp4.Count() <= 0) return false;

            // 获取到本次观测中的所有卫星编号，以估计卫星DCB
            // hashset是非重复集合，自动剔除重复元素
            HashSet<int> prns = new HashSet<int>();
            for (int i = 0; i < prn.Count(); i++)
            {
                foreach (var p in prn[i])
                {
                    prns.Add(p);
                }
            }

            // 测站数量
            int stationNum = stations.Count();
            // 卫星数量
            int satelliteNum = prns.Count();
            // 观测值数量
            int obsNum = (from item in sp4
                          select item.Count()).Sum();

            // 球谐系数参数个数
            int spmParaNum = (degree + 1) * (degree + 1);
            // 接收机DCB参数个数
            int recParaNum = stationNum;
            // 卫星DCB参数个数
            int satParaNum = satelliteNum;

            // 参数个数
            int paraNum = spmParaNum + stationNum + satelliteNum;
            if (obsNum < paraNum)
            {
                throw new Exception("观测值个数无法满足建模要求!");
            }

            // 球谐系数起始索引位置
            int spmStart = 0;
            // 接收机DCB系数起始位置
            int recStart = spmParaNum;
            // 卫星DCB系数起始位置
            int satStart = spmParaNum + recParaNum;

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(obsNum + 1, (int)paraNum);
            Vector<double> L = new DenseVector(obsNum + 1);
            int obsIndex = 0;
            int satIndex = 0;
            int recIndex = 0;
            for (int i = 0; i < sp4.Count(); i++)
            {
                for (int j = 0; j < paraNum; j++)
                {
                    B[i, j] = 0d;
                }

                recIndex = i;

                var sp4i = sp4[i].First;
                var lati = lat[i].First;
                var loni = lon[i].First;
                var prni = prn[i].First;
                var elei = ele[i].First;
                for (int j = 0; j < sp4[i].Count; j++)
                {
                    satIndex = prni.Value;

                    double sp4j = sp4i.Value;
                    double latj = lati.Value;
                    double lonj = loni.Value;
                    double elej = elei.Value;
                    double fz = MathHelper.CalIonoFactor(elej);

                    // Anm，Bnm系数
                    int col = 0;
                    for (int n = 0; n <= degree; n++)
                    {
                        for (int m = 0; m <= n; m++)
                        {
                            double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - latj);
                            B[obsIndex, col] = Pnm * System.Math.Cos(m * lonj) * fz;
                            col++;
                            // Bn0不需要估计,因为sin(0)乘任何数都是0
                            if (m == 0) continue;
                            B[obsIndex, col] = Pnm * System.Math.Sin(m * lonj) * fz;
                            col++;
                        }
                    }

                    // 接收机DCB系数
                    B[obsIndex, recStart + recIndex] = 9.52437;
                    // 卫星DCB系数
                    B[obsIndex, satStart + satIndex - 1] = -9.52437;

                    L[obsIndex] = sp4j * 9.52437;

                    obsIndex++;

                    sp4i = sp4i.Next;
                    lati = lati.Next;
                    loni = loni.Next;
                    prni = prni.Next;
                    elei = elei.Next;
                }
            }

            // 加入卫星DCB之和为0的约束
            for (int i = 0; i < paraNum; i++)
            {
                B[obsNum, i] = 0d;
            }
            foreach (var p in prns)
            {
                B[obsNum, satStart + p - 1] = 1;
            }

            ////// 求解
            var btb = B.Transpose() * B;
            var btbi = btb.Inverse();
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            // 单位权中误差(平方)
            double sigma = x.DotProduct(x) / (obsNum + 1 - paraNum);
            // 协因数阵
            Matrix<double> Qxx = sigma * btbi;


            // 提取球谐系数
            int index = 0;
            List<double[]> anm = new List<double[]>();
            List<double[]> bnm = new List<double[]>();
            for (int i = 0; i <= degree; i++)
            {
                anm.Add(new double[i + 1]);
                bnm.Add(new double[i + 1]);
                for (int j = 0; j <= i; j++)
                {
                    anm[i][j] = x[index];
                    index++;
                    bnm[i][j] = 0d;
                    if (j != 0)
                    {
                        bnm[i][j] = x[index];
                        index++;
                    }
                }
            }

            Vector<double> spmFactor = new DenseVector(spmParaNum);
            for (int i = 0; i < spmParaNum; i++)
            {
                spmFactor[i] = x[i];
            }
            spm = new SphericalHarmonicIonoModel();
            spm.Degree = degree;
            spm.Order = order;
            spm.Factor = spmFactor;
            spm.Anm = anm;
            spm.Bnm = bnm;

            Dictionary<string, double> receiverDCBRMS = new Dictionary<string, double>();
            Dictionary<string, double> satelliteDCBRMS = new Dictionary<string, double>();
            // 提取接收机dcb
            for (int i = 0; i < stationNum; i++)
            {
                receiverDCB.Add(stations[i], x[recStart + i]);
                receiverDCBRMS.Add(stations[i], Math.Sqrt(Qxx[recStart + i, recStart + i]));
            }

            // 提取卫星DCB
            foreach (var p in prns)
            {
                satelliteDCB.Add(string.Format("G{0:00}", p), x[satStart + p - 1]);
                satelliteDCBRMS.Add(string.Format("G{0:00}", p), Math.Sqrt(Qxx[satStart + p - 1, satStart + p - 1]));
            }

            return true;
        }

        /// <summary>
        /// 计算球谐函数模型,不估计测站和接收机DCB
        /// </summary>
        /// <param name="degree">球谐函数阶数(中国地区一般为9，参见耿长江)</param>
        /// <param name="order">球谐函数次数</param>
        /// <param name="stations">测站名</param>
        /// <param name="prn">卫星编号</param>
        /// <param name="lat">地磁纬度</param>
        /// <param name="lon">日固经度</param>
        /// <param name="sp4">平滑后的P4</param>
        /// <param name="ele">高度角(弧度)</param>
        /// <param name="spm">输出的球谐模型</param>
        /// <param name="receiverDCB">接收机的dcb(米)</param>
        /// <param name="satelliteDCB">卫星的dcb(米)</param>
        /// <returns></returns>
        public static bool CalSphericalHarmonicModel(
            int degree, int order,
            List<string> stations,
            List<LinkedList<int>> prn,
            List<LinkedList<double>> lat,
            List<LinkedList<double>> lon,
            List<LinkedList<double>> sp4,
            List<LinkedList<double>> ele,
            out SphericalHarmonicIonoModel spm,
            Dictionary<string, double> receiverDCB = null,
            Dictionary<string, double> satelliteDCB = null)
        {
            spm = new SphericalHarmonicIonoModel();

            if (receiverDCB is null) receiverDCB = new Dictionary<string, double>();
            if (satelliteDCB is null) satelliteDCB = new Dictionary<string, double>();

            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (sp4 is null || sp4.Count() <= 0) return false;

            Dictionary<int, double> satDCB = new Dictionary<int, double>();
            foreach (var key in satelliteDCB.Keys)
            {
                satDCB.Add(int.Parse(key.Substring(1)), satelliteDCB[key]);
            }

            // 获取到本次观测中的所有卫星编号，以估计卫星DCB
            // hashset是非重复集合，自动剔除重复元素
            HashSet<int> prns = new HashSet<int>();
            for (int i = 0; i < prn.Count(); i++)
            {
                foreach (var p in prn[i])
                {
                    prns.Add(p);
                }
            }

            // 测站数量
            int stationNum = stations.Count();
            // 卫星数量
            int satelliteNum = prns.Count();
            // 观测值数量
            int obsNum = (from item in sp4
                          select item.Count()).Sum();

            // 球谐系数参数个数
            int spmParaNum = (degree + 1) * (degree + 1);

            // 参数个数
            int paraNum = spmParaNum;
            if (obsNum < paraNum)
            {
                throw new Exception("观测值个数无法满足建模要求!");
            }

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(obsNum, (int)paraNum);
            Vector<double> L = new DenseVector(obsNum);
            int obsIndex = 0;
            int satIndex = 0;
            int recIndex = 0;
            string recName;
            double sdcb = 0d;
            double rdcb = 0d;
            for (int i = 0; i < sp4.Count(); i++)
            {
                for (int j = 0; j < paraNum; j++)
                {
                    B[i, j] = 0d;
                }

                recIndex = i;
                recName = stations[i];

                var sp4i = sp4[i].First;
                var lati = lat[i].First;
                var loni = lon[i].First;
                var prni = prn[i].First;
                var elei = ele[i].First;
                for (int j = 0; j < sp4[i].Count; j++)
                {
                    satIndex = prni.Value;

                    double sp4j = sp4i.Value;
                    double latj = lati.Value;
                    double lonj = loni.Value;
                    double elej = elei.Value;
                    int prnj = prni.Value;

                    // dcb
                    rdcb = 0d;
                    sdcb = 0d;
                    receiverDCB.TryGetValue(recName, out rdcb);
                    satDCB.TryGetValue(prnj, out sdcb);

                    // 投影函数
                    double fz = MathHelper.CalIonoFactor(elej);

                    // Anm，Bnm系数
                    int col = 0;
                    for (int n = 0; n <= degree; n++)
                    {
                        for (int m = 0; m <= n; m++)
                        {
                            double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - latj);
                            B[obsIndex, col] = Pnm * System.Math.Cos(m * lonj) * fz;
                            col++;
                            // Bn0不需要估计,因为sin(0)乘任何数都是0
                            if (m == 0) continue;
                            B[obsIndex, col] = Pnm * System.Math.Sin(m * lonj) * fz;
                            col++;
                        }
                    }

                    L[obsIndex] = sp4j * 9.52437 + 9.52437 * (sdcb - rdcb);

                    obsIndex++;

                    sp4i = sp4i.Next;
                    lati = lati.Next;
                    loni = loni.Next;
                    prni = prni.Next;
                    elei = elei.Next;
                }
            }

            ////// 求解
            var btb = B.Transpose() * B;
            var btbi = btb.Inverse();
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            // 提取球谐系数
            int index = 0;
            List<double[]> anm = new List<double[]>();
            List<double[]> bnm = new List<double[]>();
            for (int i = 0; i <= degree; i++)
            {
                anm.Add(new double[i + 1]);
                bnm.Add(new double[i + 1]);
                for (int j = 0; j <= i; j++)
                {
                    anm[i][j] = x[index];
                    index++;
                    bnm[i][j] = 0d;

                    if (j != 0)
                    {
                        bnm[i][j] = x[index];
                        index++;
                    }
                }
            }


            // 提取球谐系数
            Vector<double> spmFactor = new DenseVector(spmParaNum);
            for (int i = 0; i < spmParaNum; i++)
            {
                spmFactor[i] = x[i];
            }
            spm.Degree = degree;
            spm.Order = order;
            spm.Factor = spmFactor;
            spm.Anm = anm;
            spm.Bnm = bnm;

            return true;
        }

        /// <summary>
        /// 计算球谐函数模型,同时估计测站DCB,卫星DCB由星历提供
        /// </summary>
        /// <param name="degree">球谐函数阶数(中国地区一般为9，参见耿长江)</param>
        /// <param name="order">球谐函数次数</param>
        /// <param name="stations">测站名</param>
        /// <param name="prn">卫星编号</param>
        /// <param name="lat">地磁纬度</param>
        /// <param name="lon">日固经度</param>
        /// <param name="sp4">平滑后的P4</param>
        /// <param name="ele">高度角(弧度)</param>
        /// <param name="spm">输出的球谐模型</param>
        /// <param name="receiverDCB">接收机的dcb</param>
        /// <param name="satelliteDCB">卫星的dcb</param>
        /// <returns></returns>
        public static bool CalSphericalHarmonicModel(
            int degree, int order,
            List<string> stations,
            List<LinkedList<int>> prn,
            List<LinkedList<double>> lat,
            List<LinkedList<double>> lon,
            List<LinkedList<double>> sp4,
            List<LinkedList<double>> ele,
            Dictionary<string, double> satelliteDCB,
            out SphericalHarmonicIonoModel spm,
            out Dictionary<string, double> receiverDCB)
        {
            spm = null;
            receiverDCB = new Dictionary<string, double>();
            if (satelliteDCB is null)
                satelliteDCB = new Dictionary<string, double>();

            Dictionary<int, double> satDCB = new Dictionary<int, double>();
            foreach (var key in satelliteDCB.Keys)
            {
                satDCB.Add(int.Parse(key.Substring(1)), satelliteDCB[key]);
            }

            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (sp4 is null || sp4.Count() <= 0) return false;

            // 获取到本次观测中的所有卫星编号，以估计卫星DCB
            // hashset是非重复集合，自动剔除重复元素
            HashSet<int> prns = new HashSet<int>();
            for (int i = 0; i < prn.Count(); i++)
            {
                foreach (var p in prn[i])
                {
                    prns.Add(p);
                }
            }

            // 测站数量
            int stationNum = stations.Count();
            // 卫星数量
            int satelliteNum = prns.Count();
            // 观测值数量
            int obsNum = (from item in sp4
                          select item.Count()).Sum();

            // 球谐系数参数个数
            int spmParaNum = (degree + 1) * (degree + 1);
            // 接收机DCB参数个数
            int recParaNum = stationNum;

            // 参数个数
            int paraNum = spmParaNum + stationNum;
            if (obsNum < paraNum)
            {
                throw new Exception("观测值个数无法满足建模要求!");
            }

            // 球谐系数起始索引位置
            int spmStart = 0;
            // 接收机DCB系数起始位置
            int recStart = spmParaNum;

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(obsNum, (int)paraNum);
            Vector<double> L = new DenseVector(obsNum);
            int obsIndex = 0;
            int satIndex = 0;
            int recIndex = 0;
            double sdcb = 0;
            for (int i = 0; i < sp4.Count(); i++)
            {
                for (int j = 0; j < paraNum; j++)
                {
                    B[i, j] = 0d;
                }

                recIndex = i;

                var sp4i = sp4[i].First;
                var lati = lat[i].First;
                var loni = lon[i].First;
                var prni = prn[i].First;
                var elei = ele[i].First;
                for (int j = 0; j < sp4[i].Count; j++)
                {
                    satIndex = prni.Value;

                    double sp4j = sp4i.Value;
                    double latj = lati.Value;
                    double lonj = loni.Value;
                    double elej = elei.Value;
                    int prnj = prni.Value;

                    sdcb = 0d;
                    satDCB.TryGetValue(prnj, out sdcb);

                    double fz = MathHelper.CalIonoFactor(elej);

                    // Anm，Bnm系数
                    int col = 0;
                    for (int n = 0; n <= degree; n++)
                    {
                        for (int m = 0; m <= n; m++)
                        {
                            double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - latj);
                            B[obsIndex, col] = Pnm * System.Math.Cos(m * lonj) * fz;
                            col++;
                            // Bn0不需要估计,因为sin(0)乘任何数都是0
                            if (m == 0) continue;
                            B[obsIndex, col] = Pnm * System.Math.Sin(m * lonj) * fz;
                            col++;
                        }
                    }

                    // 接收机DCB系数
                    B[obsIndex, recStart + recIndex] = 9.52437;

                    L[obsIndex] = sp4j * 9.52437 + 9.52437 * sdcb;

                    obsIndex++;

                    sp4i = sp4i.Next;
                    lati = lati.Next;
                    loni = loni.Next;
                    prni = prni.Next;
                    elei = elei.Next;
                }
            }

            ////// 求解
            var btb = B.Transpose() * B;
            var btbi = btb.Inverse();
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            //提取球谐系数
            int index = 0;
            List<double[]> anm = new List<double[]>();
            List<double[]> bnm = new List<double[]>();
            for (int i = 0; i <= degree; i++)
            {
                anm.Add(new double[i + 1]);
                bnm.Add(new double[i + 1]);
                for (int j = 0; j <= i; j++)
                {
                    anm[i][j] = x[index];
                    bnm[i][j] = 0d;
                    if (j == 0) bnm[i][j] = x[index + 1];
                    if (j == 0) index += 1;
                    else index += 2;
                }
            }


            // 提取球谐系数
            Vector<double> spmFactor = new DenseVector(spmParaNum);
            for (int i = 0; i < spmParaNum; i++)
            {
                spmFactor[i] = x[i];
            }
            SphericalHarmonicIonoModel model = new SphericalHarmonicIonoModel();
            model.Degree = degree;
            model.Order = order;
            model.Factor = spmFactor;
            spm.Anm = anm;
            spm.Bnm = bnm;

            // 提取接收机dcb
            for (int i = 0; i < stationNum; i++)
            {
                receiverDCB.Add(stations[i], x[recStart + i]);
            }

            return true;
        }
    }
}
