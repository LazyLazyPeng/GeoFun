using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeoFun
{
    public class SevenPara
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 平移参数(m)
        /// </summary>
        public double XOff { get; set; } = 0d;

        /// <summary>
        /// 平移参数(m)
        /// </summary>
        public double YOff { get; set; } = 0d;

        /// <summary>
        /// 平移参数(m)
        /// </summary>
        public double ZOff { get; set; } = 0d;

        /// <summary>
        /// 旋转(秒)
        /// </summary>
        public double XRot
        {
            get
            {
                return (double)rx.AllSeconds;
            }
            set
            {
                rx.AllSeconds = (decimal)value;
            }
        }

        /// <summary>
        /// 旋转(秒)
        /// </summary>
        public double YRot
        {
            get
            {
                return (double)ry.AllSeconds;
            }
            set
            {
                ry.AllSeconds = (decimal)value;
            }
        }

        /// <summary>
        /// 旋转(秒)
        /// </summary>
        public double ZRot
        {
            get
            {
                return (double)rz.AllSeconds;
            }
            set
            {
                if (rz is null) rz = new Angle();
                rz.AllSeconds = (decimal)value;
            }
        }

        private Angle rx = new Angle();
        public Angle RX
        {
            get
            {
                return rx;
            }
            set
            {
                rx = value;
            }
        }

        private Angle ry = new Angle();
        public Angle RY
        {
            get
            {
                return ry;
            }
            set
            {
                ry = value;
            }
        }

        private Angle rz = new Angle();
        public Angle RZ
        {
            get
            {
                return rz;
            }
            set
            {
                rz = value;
            }
        }


        /// <summary>
        /// 尺度参数(ppm)
        /// </summary>
        public double M { get; set; } = 0d;

        public static SevenPara CalParaYao(List<double> b1, List<double> l1, List<double> h1,
            List<double> b2, List<double> l2, List<double> h2,
            Ellipsoid ell1, Ellipsoid ell2)
        {
            double[] p = new double[49];
            double[] x = new double[7];
            double[] l = new double[3000];
            double[] w = new double[7];
            double[] v = new double[3000];
            char[] dh = new char[20];
            double targetx = 0, targety = 0, targetz = 0;
            double sourcex = 0, sourcey = 0d, sourcez = 0;
            double targetb = 0d, targetl = 0d, targeth = 0d;
            double sourceb = 0d, sourcel = 0d, sourceh = 0d;
            double[,] a = new double[1500, 7];
            double pvv, m0, targeta, targetf, sourcea, sourcef, targete, sourcee; ;
            double[] m = new double[7];
            int i, j, n, k;
            n = l1.Count;

            int num = 7;
            x[0] = x[1] = x[2] = x[3] = x[4] = x[5] = x[6] = 0;
            m[0] = m[1] = m[2] = m[3] = m[4] = m[5] = m[6] = 0;

            sourcea = ell1.A;
            targeta = ell2.A;
            sourcee = ell1.E1;
            targete = ell2.E1;

            int[] pos = { 3, 4, 5, 6 };
            double P0 = 360d * 180d / Angle.PI;
            for (i = 0; i < l1.Count; i++)
            {
                //sourceb = Angle.gd(b1[i]);
                //sourcel = Angle.gd(l1[i]);
                sourceb = b1[i];
                sourcel = l1[i];
                sourceh = h1[i];
                //targetb = Angle.gd(b2[i]);
                //targetl = Angle.gd(l2[i]);
                targetb = b2[i];
                targetl = l2[i];
                targeth = h2[i];

                Coordinate.compute_xyz(sourceb, sourcel, sourceh, sourcea, sourcee, ref sourcex, ref sourcey, ref sourcez);
                Coordinate.compute_xyz(targetb, targetl, targeth, targeta, targete, ref targetx, ref targety, ref targetz);

                a[3 * i + 0, 0] = 1;
                a[3 * i + 0, 1] = 0;
                a[3 * i + 0, 2] = 0;
                a[3 * i + 0, pos[0]] = sourcex / 1000000.0;
                a[3 * i + 0, pos[1]] = 0;
                a[3 * i + 0, pos[2]] = -sourcez / P0;
                a[3 * i + 0, pos[3]] = sourcey / P0;

                a[3 * i + 1, 0] = 0;
                a[3 * i + 1, 1] = 1;
                a[3 * i + 1, 2] = 0;
                a[3 * i + 1, pos[0]] = sourcey / 1000000.0;
                a[3 * i + 1, pos[1]] = sourcez / P0;
                a[3 * i + 1, pos[2]] = 0;
                a[3 * i + 1, pos[3]] = -sourcex / P0;

                a[3 * i + 2, 0] = 0;
                a[3 * i + 2, 1] = 0;
                a[3 * i + 2, 2] = 1;
                a[3 * i + 2, pos[0]] = sourcez / 1000000.0;
                a[3 * i + 2, pos[1]] = -sourcey / P0;
                a[3 * i + 2, pos[2]] = sourcex / P0;
                a[3 * i + 2, pos[3]] = 0;


                l[3 * i + 0] = -targetx + sourcex;
                l[3 * i + 1] = -targety + sourcey;
                l[3 * i + 2] = -targetz + sourcez;
            }

            for (i = 0; i < num; i++)
            {
                for (j = i; j < num; j++)
                {
                    p[i * num + j] = 0;
                    for (k = 0; k < 3 * n; k++)
                        p[i * num + j] += a[k, j] * a[k, i];
                    p[j * num + i] = p[i * num + j];
                }
            }
            for (i = 0; i < num; i++)
            {
                w[i] = 0.0;
                for (j = 0; j < 3 * n; j++)
                    w[i] += a[j, i] * l[j];
            }
            MatrixHelper.Inv(ref p, num);
            for (i = 0; i < num; i++)
            {
                x[i] = 0;
                for (j = 0; j < num; j++)
                    x[i] += -p[i * num + j] * w[j];
            }
            pvv = 0;
            for (i = 0; i < 3 * n; i++)
            {
                v[i] = l[i];
                for (j = 0; j < num; j++)
                    v[i] += a[i, j] * x[j];
                pvv += v[i] * v[i];
            }
            m0 = Math.Sqrt(pvv / (3 * n - num + 0.000000001));

            SevenPara sev = new SevenPara
            {
                XOff = x[0],
                YOff = x[1],
                ZOff = x[2],
                M = x[3],
                XRot = x[4],
                YRot = x[5],
                ZRot = x[6],
            };
            return sev;
        }

        /// <summary>
        /// 计算七参数(椭球1到椭球2)
        /// </summary>
        /// <param name="b1">纬度(弧度)</param>
        /// <param name="l1">经度(弧度)</param>
        /// <param name="h1">大地高</param>
        /// <param name="b2">纬度(弧度)</param>
        /// <param name="l2">经度(弧度)</param>
        /// <param name="h2">大地高</param>
        /// <param name="ell1">椭球1</param>
        /// <param name="ell2">椭球2</param>
        /// <returns></returns>
        public static SevenPara CalPara(List<double> b1, List<double> l1, List<double> h1,
            List<double> b2, List<double> l2, List<double> h2,
            Ellipsoid ell1, Ellipsoid ell2)
        {
            if (b1 is null || l1 is null || b2 is null || l2 is null || ell1 is null || ell2 is null) throw new ArgumentNullException("公共点不能为空");
            if ((h1 is null) || (h2 is null)) throw new ArgumentNullException("至少需要一个坐标系的高程值");

            if (h1 is null) h1 = h2;
            if (h2 is null) h2 = h1;

            var leng = new List<int> { b1.Count, l1.Count, h1.Count, b2.Count, l2.Count, h2.Count };
            var pointNum = leng.Min();

            if (pointNum < 3) throw new ArgumentException("公共点不足");

            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            List<double> Z1 = new List<double>();

            List<double> X2 = new List<double>();
            List<double> Y2 = new List<double>();
            List<double> Z2 = new List<double>();

            Trans trans = new Trans();
            for (int i = 0; i < pointNum; i++)
            {
                double x, y, z;
                Coordinate.BLH2XYZ(b1[i], l1[i], h1[i], out x, out y, out z, ell1);
                X1.Add(x); Y1.Add(y); Z1.Add(z);

                Coordinate.BLH2XYZ(b2[i], l2[i], h2[i], out x, out y, out z, ell2);
                X2.Add(x); Y2.Add(y); Z2.Add(z);
            }

            return CalPara(X1, Y1, Z1, X2, Y2, Z2);

        }

        /// <summary>
        /// 计算七参数
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="Z1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <param name="Z2"></param>
        /// <returns></returns>
        public static SevenPara CalPara(List<double> X1, List<double> Y1, List<double> Z1,
            List<double> X2, List<double> Y2, List<double> Z2)
        {
            var pointNum = (new List<int> { X1.Count, Y1.Count, Z1.Count, X2.Count, Y2.Count, Z2.Count }).Min();

            Matrix<double> matrix = new DenseMatrix(pointNum * 3, 7);
            Vector<double> vector = new DenseVector(pointNum * 3);

            for (int i = 0; i < pointNum; i++)
            {
                matrix[i * 3, 0] = 1;
                matrix[i * 3, 1] = 0;
                matrix[i * 3, 2] = 0;
                matrix[i * 3, 3] = X1[i];
                matrix[i * 3, 4] = 0;
                matrix[i * 3, 5] = -Z1[i];
                matrix[i * 3, 6] = Y1[i];

                matrix[i * 3 + 1, 0] = 0;
                matrix[i * 3 + 1, 1] = 1;
                matrix[i * 3 + 1, 2] = 0;
                matrix[i * 3 + 1, 3] = Y1[i];
                matrix[i * 3 + 1, 4] = Z1[i];
                matrix[i * 3 + 1, 5] = 0;
                matrix[i * 3 + 1, 6] = -X1[i];

                matrix[i * 3 + 2, 0] = 0;
                matrix[i * 3 + 2, 1] = 0;
                matrix[i * 3 + 2, 2] = 1;
                matrix[i * 3 + 2, 3] = Z1[i];
                matrix[i * 3 + 2, 4] = -Y1[i];
                matrix[i * 3 + 2, 5] = X1[i];
                matrix[i * 3 + 2, 6] = 0;

                vector[i * 3] = X2[i];
                vector[i * 3 + 1] = Y2[i];
                vector[i * 3 + 2] = Z2[i];
            }

            var BTPB = (matrix.Transpose().Multiply(matrix)).Inverse();
            var BTPL = matrix.Transpose().Multiply(vector);
            var result = BTPB.Multiply(BTPL);

            double dX = result[0];
            double dY = result[1];
            double dZ = result[2];

            double a1 = result[3];
            double a2 = result[4];
            double a3 = result[5];
            double a4 = result[6];

            double m = (a1 - 1) * 1e6;
            double rx = a2 / a1;
            double ry = a3 / a1;
            double rz = a4 / a1;

            rx *= Angle.R2D * 3600;
            ry *= Angle.R2D * 3600;
            rz *= Angle.R2D * 3600;

            return new SevenPara
            {
                XOff = dX,
                YOff = dY,
                ZOff = dZ,

                XRot = rx,
                YRot = ry,
                ZRot = rz,

                M = m,
            };
        }

        /// <summary>
        /// 计算七参数(椭球1到椭球2)
        /// </summary>
        /// <param name="b1">纬度(弧度)</param>
        /// <param name="l1">经度(弧度)</param>
        /// <param name="h1">大地高</param>
        /// <param name="b2">纬度(弧度)</param>
        /// <param name="l2">经度(弧度)</param>
        /// <param name="h2">大地高</param>
        /// <param name="ell1">椭球1</param>
        /// <param name="ell2">椭球2</param>
        /// <param name="maxRes">最大残差(m)</param>
        /// <returns></returns>
        public static SevenPara CalParaIter(List<double> b1, List<double> l1, List<double> h1,
            List<double> b2, List<double> l2, List<double> h2,
            Ellipsoid ell1, Ellipsoid ell2)
        {
            if (b1 is null || l1 is null || b2 is null || l2 is null || ell1 is null || ell2 is null) throw new ArgumentNullException("公共点不能为空");
            if ((h1 is null) || (h2 is null)) throw new ArgumentNullException("至少需要一个坐标系的高程值");

            if (h1 is null) h1 = h2;
            if (h2 is null) h2 = h1;

            var leng = new List<int> { b1.Count, l1.Count, h1.Count, b2.Count, l2.Count, h2.Count };
            var pointNum = leng.Min();

            if (pointNum < 3) throw new ArgumentException("公共点不足");

            if (pointNum == 3) return CalPara(b1, l1, h1, b2, l2, h2, ell1, ell2);

            //// 最终选取的公共点坐标
            List<double> b1Com = new List<double>();
            List<double> l1Com = new List<double>();
            List<double> b2Com = new List<double>();
            List<double> l2Com = new List<double>();
            List<double> h1Com = new List<double>();
            List<double> h2Com = new List<double>();

            SevenPara sev = new SevenPara();
            Trans trans = new Trans();
            var indexes = Enumerable.Range(0, pointNum).ToList();

            //// 最大残差
            var maxDiff = 1d;
            var maxInde = 0;
            while (indexes.Count > 3)
            {
                b1Com.Clear();
                l1Com.Clear();
                h1Com.Clear();
                b2Com.Clear();
                l2Com.Clear();
                h2Com.Clear();

                foreach (var index in indexes)
                {
                    b1Com.Add(b1[index]);
                    l1Com.Add(l1[index]);
                    h1Com.Add(h1[index]);

                    b2Com.Add(b2[index]);
                    l2Com.Add(l2[index]);
                    h2Com.Add(h2[index]);
                }

                sev = CalPara(b1Com, l1Com, h1Com, b2Com, l2Com, h2Com, ell1, ell2);

                //// 转换后的blh
                List<double> b2Trans, l2Trans, h2Trans;
                trans.Seven3d(b1Com, l1Com, h1Com, out b2Trans, out l2Trans, out h2Trans, sev, ell1, ell2);

                //// 转换后的xyz
                List<double> X2Trans, Y2Trans, Z2Trans;
                Coordinate.BLH2XYZ(b2Trans, l2Trans, h2Trans, out X2Trans, out Y2Trans, out Z2Trans, Ellipsoid.ELLIP_CGCS2000);

                //// 原始的xyz
                List<double> X2Tem, Y2Tem, Z2Tem;
                Coordinate.BLH2XYZ(b2Com, l2Com, h2Com, out X2Tem, out Y2Tem, out Z2Tem, Ellipsoid.ELLIP_CGCS2000);

                ///// 计算残差(单位米)
                var diff = (from i in Enumerable.Range(0, b2Com.Count)
                            select new
                            {
                                Index = i,
                                Diff = Math.Sqrt(Math.Pow(X2Tem[i] - X2Trans[i], 2) + Math.Pow(Y2Trans[i] - Y2Tem[i], 2) + Math.Pow(Z2Trans[i] - Z2Tem[i], 2))
                            }).ToList();

                double sigma = Math.Sqrt(diff.Sum(d => d.Diff * d.Diff) / indexes.Count);

                var max = diff.OrderByDescending(d => d.Diff).First();

                maxInde = max.Index;
                maxDiff = max.Diff;

                if (maxDiff > sigma * 3d)
                {
                    indexes.Remove(maxInde);
                }
                else
                {
                    break;
                }
            }

            return sev;
        }

        override
        public string ToString()
        {
            return string.Format("{0:f4} {1:f4} {2:f4} {3:f10} {4:f10} {5:f10} {6:f10}",
                XOff, YOff, ZOff, XRot, YRot, ZRot, M);
        }
    }
}