using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace GeoFun
{
    public class Coordinate
    {
        public static void compute_xyz(double b, double l, double h, double a, double e, ref double x, ref double y, ref double z)
        {
            double n;
            n = a / Math.Sqrt(1 - e * Math.Pow(Math.Sin(b), 2));
            x = (n + h) * Math.Cos(b);
            y = x * Math.Sin(l);
            x *= Math.Cos(l);
            z = (n * (1 - e) + h) * Math.Sin(b);
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">经度(弧度)</param>
        /// <param name="H">大地高(m)</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="a">椭球长半轴(m)</param>
        /// <param name="f">扁率倒数</param>
        public static void BLH2XYZ(double B, double L, double H, out double X, out double Y, out double Z, double a, double f)
        {
            Ellipsoid ell = new Ellipsoid(a = a, f = f);
            BLH2XYZ(B, L, H, out X, out Y, out Z, ell);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">经度(弧度)</param>
        /// <param name="H">大地高(m)</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="a">椭球长半轴(m)</param>
        /// <param name="f">扁率倒数</param>
        public static void BLH2XYZ(List<double> B, List<double> L, List<double> H, out List<double> X, out List<double> Y, out List<double> Z, double a, double f)
        {
            Ellipsoid ell = new Ellipsoid(a = a, f = f);
            BLH2XYZ(B, L, H, out X, out Y, out Z, ell);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">经度(弧度)</param>
        /// <param name="H">大地高(m)</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="a">椭球长半轴(m)</param>
        /// <param name="f">扁率倒数</param>
        public static void XYZ2BLH(double X, double Y, double Z, out double B, out double L, out double H, double a, double f)
        {
            Ellipsoid ell = new Ellipsoid(a = a, f = f);
            XYZ2BLH(X, Y, Z, out B, out L, out H, ell);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">经度(弧度)</param>
        /// <param name="H">大地高(m)</param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="a">椭球长半轴(m)</param>
        /// <param name="f">扁率倒数</param>
        public static void XYZ2BLH(List<double> X, List<double> Y, List<double> Z, out List<double> B, out List<double> L, out List<double> H,double a, double f) 
        {
            Ellipsoid ell = new Ellipsoid(a = a, f = f);
            XYZ2BLH(X, Y, Z, out B, out L, out H, ell);
        }

        /// <summary>
        /// 大地坐标转空间直角坐标
        /// </summary>
        /// <param name="B">弧度</param>
        /// <param name="L"></param>
        /// <param name="H"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="ell"></param>
        public static void BLH2XYZ(double B, double L, double H, out double X, out double Y, out double Z, Ellipsoid ell)
        {
            X = (ell.N(B) + H) * Math.Cos(B) * Math.Cos(L);
            Y = (ell.N(B) + H) * Math.Cos(B) * Math.Sin(L);
            Z = (ell.N(B) * (1 - ell.E1) + H) * Math.Sin(B);
        }

        public static void BLH2XYZ(List<double> B, List<double> L, List<double> H, out List<double> X, out List<double> Y, out List<double> Z, Ellipsoid ell)
        {
            X = new List<double>();
            Y = new List<double>();
            Z = new List<double>();

            if (B == null || L == null || H == null) return;
            if (B.Count != L.Count) throw new Exception("坐标参数个数不一致");
            if (B.Count != H.Count) throw new Exception("坐标参数个数不一致");

            double x = 0d, y = 0d, z = 0d;
            for (int i = 0; i < B.Count; i++)
            {
                BLH2XYZ(B[i], L[i], H[i], out x, out y, out z, ell);

                X.Add(x); Y.Add(y); Z.Add(z);
            }
        }

        public static void XYZ2BLH(double X, double Y, double Z, out double B, out double L, out double H, Ellipsoid ell)
        {
            B = 0d;
            L = 0d;
            H = 0d;

            L = Math.Atan2(Y, X);

            double R = Math.Sqrt(X * X + Y * Y);
            double t0 = Z / R;
            double ti = t0;
            double p = 1;
            double k = 1 + ell.E2;
            double tii = ti + 1;
            int count = 0;
            while (Math.Abs(tii - ti) > 1e-13 && count < 20)
            {
                ti = tii;
                p = ell.c * ell.E1 / R;
                tii = t0 + p * ti / Math.Sqrt(k + ti * ti);

                count++;
            }

            B = Math.Atan(tii);

            H = Z / Math.Sin(B) - ell.N(B) * (1 - ell.E1);
        }

        public static void XYZ2BLH(List<double> X, List<double> Y, List<double> Z, out List<double> B, out List<double> L, out List<double> H, Ellipsoid ell)
        {
            B = new List<double>();
            L = new List<double>();
            H = new List<double>();

            if (X == null || Y == null || Z == null) return;
            if (X.Count != Y.Count) throw new Exception("坐标参数个数不一致");
            if (X.Count != Z.Count) throw new Exception("坐标参数个数不一致");

            double b, l, h;
            for (int i = 0; i < X.Count; i++)
            {
                XYZ2BLH(X[i], Y[i], Z[i], out b, out l, out h, ell);

                B.Add(b); L.Add(l); H.Add(h);
            }
        }

        public static List<BLH> XYZ2BLH(List<XYZ> xyzList,double a, double f)
        {
            if (xyzList is null) return null;
            List<BLH> blhList = new List<BLH>(xyzList.Count);

            double b = 0;double l = 0; double h = 0;
            foreach(var xyz in xyzList)
            {
                XYZ2BLH(xyz.X, xyz.Y, xyz.Z, out b, out l, out h,a,f);

                BLH blh = new BLH();
                blh.B = b;
                blh.L = l;
                blh.H = h;
                blhList.Add(blh);
            }

            return blhList;
        }

        public static void CalIPP(double xSat, double ySat, double zSat,
            double xRec, double yRec, double zRec,
            out double x, out double y, out double z,
            double earthR = 63781000,double ionoH = 450000)
        {
            x = y = z = 0d;

            Vector<double> op1 = new DenseVector(new double[] { xRec, yRec, zRec });
            Vector<double> op2 = new DenseVector(new double[] { xSat, ySat, zSat });

            Vector<double> p1p2 = op1 - op2;
            p1p2=p1p2/p1p2.L2Norm();

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

                if(d1<d2)
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
    }
}
