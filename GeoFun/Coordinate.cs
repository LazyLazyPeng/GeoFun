using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics.Mcmc;

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
        public static void XYZ2BLH(List<double> X, List<double> Y, List<double> Z, out List<double> B, out List<double> L, out List<double> H, double a, double f)
        {
            Ellipsoid ell = new Ellipsoid(a = a, f = f);
            XYZ2BLH(X, Y, Z, out B, out L, out H, ell);
        }

        public static void XYZ2BLH(double[] xyz, out double B, out double L, out double H, Ellipsoid ell)
        {
            XYZ2BLH(xyz[0], xyz[1], xyz[2], out B, out L, out H, ell);
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

            if (L < 0) L += 2 * Angle.PI;
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

        public static List<BLH> XYZ2BLH(List<XYZ> xyzList, double a, double f)
        {
            if (xyzList is null) return null;
            List<BLH> blhList = new List<BLH>(xyzList.Count);

            double b = 0; double l = 0; double h = 0;
            foreach (var xyz in xyzList)
            {
                XYZ2BLH(xyz.X, xyz.Y, xyz.Z, out b, out l, out h, a, f);

                BLH blh = new BLH();
                blh.B = b;
                blh.L = l;
                blh.H = h;
                blhList.Add(blh);
            }

            return blhList;
        }

        /// <summary>
        /// xyz转站心坐标系
        /// </summary>
        /// <param name="b">站心纬度(弧度)</param>
        /// <param name="l">站心经度(弧度)</param>
        /// <param name="p0">站心坐标(m) xyz</param>
        /// <param name="p2">待求点的xyz</param>
        /// <returns></returns>
        public static double[] ECEF2ENU(double b, double l, double[] p0, double[] p2)
        {
            if (p0 is null)
            {
                throw new ArgumentNullException("p0");
            }

            if (p2 is null)
            {
                throw new ArgumentNullException("p2");
            }

            double sinb = Math.Sin(b);
            double sinl = Math.Sin(l);
            double cosb = Math.Cos(b);
            double cosl = Math.Cos(l);
            double dx = p2[0] - p0[0];
            double dy = p2[1] - p0[1];
            double dz = p2[2] - p0[2];


            double[] pp = new double[3];
            pp[0] = -sinb * cosl * dx - sinb * sinl * dy + cosb * dz;
            pp[1] = -sinl * dx + cosl * dy;
            pp[2] = cosb * cosl * dx + cosb * sinl * dy + sinb * dz;
            return pp;
        }

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
        /// 计算太阳经度
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="gmt">时区 东8区为+8 西8区为-8</param>
        public static double SunLon(int hour, int minute, double second, int gmt)
        {
            double l = Math.PI - (hour - gmt + minute / 60d + second / 3600d) * 15 / 360 * 2 * Math.PI;
            if (l < -Math.PI) l += Math.PI * 2;
            if (l > Math.PI) l -= Math.PI * 2;
            return l;
        }

        /// <summary>
        /// 将地理坐标系坐标转为地磁坐标系坐标
        /// </summary>
        /// <param name="inB">待求点B(弧度)</param>
        /// <param name="inL">待求点L(弧度)</param>
        /// <param name="geomagB">地磁北极B(弧度)</param>
        /// <param name="geomagL">地磁北极L(弧度)</param>
        /// <param name="outB">输出B(弧度)</param>
        /// <param name="outL">输出L(弧段)</param>
        /// <param name="ell">椭球</param>
        public static void Geomagnetic(double inB, double inL, double geomagB, double geomagL, out double outB, out double outL, Ellipsoid ell)
        {
            if (ell is null) ell = Ellipsoid.ELLIP_WGS84;

            double x, y, z;
            BLH2XYZ(inB, inL, 0, out x, out y, out z, ell);
            Vector<double> xx = new DenseVector(3);
            xx[0] = x;
            xx[1] = y;
            xx[2] = z;

            Matrix<double> r1 = new DenseMatrix(3, 3);
            r1[0, 0] = Math.Cos(geomagL);
            r1[0, 1] = Math.Sin(geomagL);
            r1[0, 2] = 0;
            r1[1, 0] = -Math.Sin(geomagL);
            r1[1, 1] = Math.Cos(geomagL);
            r1[1, 2] = 0;
            r1[2, 0] = 0;
            r1[2, 1] = 0;
            r1[2, 2] = 1;

            Matrix<double> r2 = new DenseMatrix(3, 3);
            r2[0, 0] = Math.Cos(Angle.PI / 2 - geomagB);
            r2[0, 1] = 0;
            r2[0, 2] = -Math.Sin(Angle.PI / 2 - geomagB);
            r2[1, 0] = 0;
            r2[1, 1] = 1;
            r2[1, 2] = 0;
            r2[2, 0] = Math.Sin(Angle.PI / 2 - geomagB);
            r2[2, 1] = 0;
            r2[2, 2] = Math.Cos(Angle.PI / 2 - geomagB);

            Vector<double> result = r1 * r2 * xx;

            x = result[0];
            y = result[1];
            z = result[2];

            double h;
            XYZ2BLH(x, y, z, out outB, out outL, out h, ell);
        }

        /// <summary>
        /// 将参心地固坐标系转换到日固地磁坐标系
        /// </summary>
        /// <param name="b">待求点b(弧度)</param>
        /// <param name="l">待求点l(弧度)</param>
        /// <param name="hour">时 (UTC)</param>
        /// <param name="minute">分</param>
        /// <param name="second">秒</param>
        /// <param name="geomagB">地磁纬度(弧度)</param>
        /// <param name="geomagL">地磁经度(弧度)</param>
        /// <param name="sgb">输出纬度(弧度)</param>
        /// <param name="sgl">输出经度(弧度)</param>
        /// <remarks>
        /// 耿长江，利用地基GNSS数据实时监测电离层延迟理论与方法研究，武汉大学，2011
        /// </remarks>
        public static void SunGeomagnetic(
            double b, double l,
            int hour, int minute, double second,
            double geomagB, double geomagL,
            out double sgb, out double sgl)
        {
            // 计算此时太阳经纬度(太阳直射经度，赤道)
            double sunB = 0;
            double sunL = SunLon(hour, minute, second, 0);

            // 计算太阳点在地磁坐标系下的坐标
            double sunGB, sunGL;
            Geomagnetic(sunB, sunL, geomagB, geomagL, out sunGB, out sunGL, Ellipsoid.ELLIP_WGS84);

            // 待求点在地磁坐标系下的坐标
            double inGB, inGL;
            Geomagnetic(b, l, geomagB, geomagL, out inGB, out inGL, Ellipsoid.ELLIP_WGS84);

            sgb = inGB;
            sgl = inGL - sunGL;

            if (sgl > Math.PI) sgl -= Math.PI;
            if (sgl < -Math.PI) sgl += Math.PI;
        }
    }
}
