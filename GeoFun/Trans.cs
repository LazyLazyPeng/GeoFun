using System;
using System.Collections.Generic;

namespace GeoFun
{
    public class Trans
    {
        /// <summary>
        /// 二维七参数，高程设置为固定值800
        /// </summary>
        /// <param name="dB"></param>
        /// <param name="dL"></param>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="para"></param>
        /// <param name="ell1"></param>
        /// <param name="ell2"></param>
        public void Seven2d(out double dB, out double dL, double b, double l, SevenPara para, Ellipsoid ell1, Ellipsoid ell2)
        {
            dB = 0d;
            dL = 0d;
            double dH = 0d;

            //double M = ell1.M(b);
            //double N = ell1.N(b);

            //dL = -Math.Sin(l) / N / Math.Cos(b) * para.XOff + Math.Cos(l) / N / Math.Cos(b) * para.YOff +
            //    Math.Tan(b) * Math.Cos(l) * para.XRot * Angle.S2R + Math.Tan(b) * Math.Sin(l) * para.YRot * Angle.S2R - para.ZRot * Angle.S2R;

            //dB = -Math.Sin(b) * Math.Cos(l) / M - Math.Sin(b) * Math.Sin(l) / M + Math.Cos(b) / M -
            //    Math.Sin(l) * para.XRot * Angle.S2R + Math.Cos(l) * para.YRot * Angle.S2R -
            //    -N / M * ell1.E1 * Math.Sin(b) * Math.Cos(b) * para.M * 1e-6 +
            //    N / M / ell1.A * ell1.E1 * Math.Sin(b) * Math.Cos(b) * (ell2.A - ell1.A) + (2 - ell1.E1 * Math.Sin(b) * Math.Sin(b)) / (1 - ell1.Alpha) * Math.Sin(b) * Math.Cos(b) * (ell2.Alpha - ell2.Alpha);

            Seven3d(out dB, out dL, out dH, b, l, 800, para, ell1, ell2);
        }

        //// 经测试，精度无法达到要求
        //public void Seven3d(out double dB, out double dL, out double dH, double b, double l, double h, SevenPara para, Ellipsoid ell1, Ellipsoid ell2)
        //{
        //    dB = 0d;
        //    dL = 0d;
        //    dH = 0d;

        //    double N = ell1.N(b);
        //    double M = ell1.M(b);

        //    //double p = 180 * 3600 / Angle.PI;
        //    double p = 1d;

        //    dL = p * -Math.Sin(l) / (N + h) / Math.Cos(b) * para.XOff + p * Math.Cos(l) / (N + h) / Math.Cos(b) * para.YOff +
        //        (N * (1 - ell1.E1) + h) / (N + h) * Math.Tan(b) * Math.Cos(l) * para.XRot * Angle.S2R + (N * (1 - ell1.E1) + h) / (N + h) * Math.Tan(b) * Math.Sin(l) * para.YRot * Angle.S2R - para.ZRot * Angle.S2R;
        //    dB = -p * Math.Sin(b) * Math.Cos(l) / (M + h) * para.XOff - p * Math.Sin(b) * Math.Sin(l) / (M + h) * para.YOff + Math.Cos(b) / (M + h) * para.ZOff -
        //        ((N + h) - N * ell1.E1 * Math.Pow(Math.Sin(b), 2)) / (M + h) * Math.Sin(l) * para.XRot * Angle.S2R + ((N + h) - N * ell1.E1 * Math.Pow(Math.Sin(b), 2)) / (M + h) * Math.Cos(l) * para.YRot * Angle.S2R -
        //        p * N / M * ell1.E1 * Math.Sin(b) * Math.Cos(b) * para.M * 1e-6 +
        //        p * N / M / ell1.A * ell1.E1 * Math.Sin(b) * Math.Cos(b) * (ell2.A - ell1.A) + p * (2 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) / (1 - ell1.Alpha) * Math.Sin(b) * Math.Cos(b) * (ell2.Alpha - ell1.Alpha);
        //    dH = Math.Cos(b) * Math.Cos(l) * para.XOff + Math.Cos(b) * Math.Sin(l) * para.YOff + Math.Sin(b) * para.ZOff -
        //        N * ell1.E1 * Math.Sin(b) * Math.Cos(b) * Math.Sin(l) * para.XRot * Angle.S2R + N * ell1.E1 * Math.Sin(b) * Math.Cos(b) * Math.Cos(l) * para.YRot * Angle.S2R +
        //        ((N + h) - N * ell1.E1 * Math.Pow(Math.Sin(b), 2)) * para.M * 1e-6 -
        //        N / ell1.A * (1 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) * (ell2.A - ell1.A) + M / (1 - ell1.Alpha) * (1 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) * Math.Pow(Math.Sin(b), 2) * (ell2.Alpha - ell1.Alpha);

        //    //dL = p * -Math.Sin(l) / (N + h) / Math.Cos(b) * para.XOff + p * Math.Cos(l) / (N + h) / Math.Cos(b) * para.YOff +
        //    //    Math.Tan(b) * Math.Cos(l) * para.XRot * Angle.S2R +  Math.Tan(b) * Math.Sin(l) * para.YRot * Angle.S2R - para.ZRot * Angle.S2R;
        //    //dB = -p * Math.Sin(b) * Math.Cos(l) / (M + h) * para.XOff - p * Math.Sin(b) * Math.Sin(l) / (M + h) * para.YOff + Math.Cos(b) / (M + h) * para.ZOff -
        //    //   Math.Sin(l) * para.XRot * Angle.S2R +  Math.Cos(l) * para.YRot * Angle.S2R -
        //    //    p * N / (M+h) * ell1.E1 * Math.Sin(b) * Math.Cos(b) * para.M * 1e-6 +
        //    //    p * N / (M+h) / ell1.A * ell1.E1 * Math.Sin(b) * Math.Cos(b) * (ell2.A - ell1.A) + p * M*(2 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) /(M+h)/ (1 - ell1.Alpha) * Math.Sin(b) * Math.Cos(b) * (ell2.Alpha - ell1.Alpha);
        //    //dH = Math.Cos(b) * Math.Cos(l) * para.XOff + Math.Cos(b) * Math.Sin(l) * para.YOff + Math.Sin(b) * para.ZOff -
        //    //    N * ell1.E1 * Math.Sin(b) * Math.Cos(b) * Math.Sin(l) * para.XRot * Angle.S2R + N * ell1.E1 * Math.Sin(b) * Math.Cos(b) * Math.Cos(l) * para.YRot * Angle.S2R +
        //    //    ((N + h) - N * ell1.E1 * Math.Pow(Math.Sin(b), 2)) * para.M * 1e-6 -
        //    //    N / ell1.A * (1 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) * (ell2.A - ell1.A) + M / (1 - ell1.Alpha) * (1 - ell1.E1 * Math.Pow(Math.Sin(b), 2)) * Math.Pow(Math.Sin(b), 2) * (ell2.Alpha - ell1.Alpha);
        //}

        /// <summary>
        /// 七参数转换
        /// </summary>
        /// <param name="dB">改正量(弧度)</param>
        /// <param name="dL">改正量(弧度)</param>
        /// <param name="dH">改正量(米)</param>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">精度(弧度)</param>
        /// <param name="H">大地高(米)</param>
        /// <param name="para">七参数</param>
        /// <param name="ell1">椭球1</param>
        /// <param name="ell2">椭球2</param>
        public void Seven3d(out double dB, out double dL, out double dH,
            double B, double L, double H,
            SevenPara para, Ellipsoid ell1, Ellipsoid ell2)
        {
            dB = 0d; dL = 0d; dH = 0d;

            double X, Y, Z, BB, LL, HH;

            Coordinate.BLH2XYZ(B, L, H, out X, out Y, out Z, ell1);

            double XX = (1 + para.M * 1e-6) * (X + Y * para.ZRot * Angle.S2R - Z * para.YRot * Angle.S2R) + para.XOff;
            double YY = (1 + para.M * 1e-6) * (-X * para.ZRot * Angle.S2R + Y + Z * para.XRot * Angle.S2R) + para.YOff;
            double ZZ = (1 + para.M * 1e-6) * (X * para.YRot * Angle.S2R - Y * para.XRot * Angle.S2R + Z) + para.ZOff;

            Coordinate.XYZ2BLH(XX, YY, ZZ, out BB, out LL, out HH, ell2);

            dB = BB - B;
            dL = LL - L;
            dH = HH - H;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dB">改正量(弧度)</param>
        /// <param name="dL">改正量(弧度)</param>
        /// <param name="dH">改正量(米)</param>
        /// <param name="B">纬度(弧度)</param>
        /// <param name="L">精度(弧度)</param>
        /// <param name="H">大地高(米)</param>>
        /// <param name="xoff">米</param>
        /// <param name="yoff">米</param>
        /// <param name="zoff">米</param>
        /// <param name="xrot">角度秒</param>
        /// <param name="yrot">角度秒</param>
        /// <param name="zrot">角度秒</param>
        /// <param name="m">ppm</param>
        /// <param name="a1">米</param>
        /// <param name="f1">扁率倒数</param>
        /// <param name="a2">米</param>
        /// <param name="f2">扁率倒数</param>
        public void Seven3d(out double dB, out double dL, out double dH,
            double B, double L, double H,
            double xoff, double yoff, double zoff, double xrot, double yrot, double zrot, double m,
            double a1, double f1, double a2, double f2)
        {
            SevenPara para = new SevenPara
            {
                XOff = xoff,
                YOff = yoff,
                ZOff = zoff,
                XRot = xrot,
                YRot = yrot,
                ZRot = zrot,
                M = m,
            };

            Ellipsoid ell1 = new Ellipsoid(a1, f1);
            Ellipsoid ell2 = new Ellipsoid(a2, f2);

            Seven3d(out dB, out dL, out dH, B, L, H, para, ell1, ell2);
        }

        public void Seven3d(List<double> B1, List<double> L1, List<double> H1,
            out List<double> B2, out List<double> L2, out List<double> H2,
            SevenPara sev, Ellipsoid ell1, Ellipsoid ell2)
        {
            B2 = new List<double>();
            L2 = new List<double>();
            H2 = new List<double>();

            if (B1 == null || L1 == null || H1 == null || sev == null || ell1 == null || ell2 == null) return;
            if (B1.Count != L1.Count) throw new ArgumentException("Number of coordinates is not uniform");
            if (B1.Count != H1.Count) throw new ArgumentException("Number of coordinates is not uniform");

            double dB, dL, dH;
            for (int i = 0; i < B1.Count; i++)
            {
                Seven3d(out dB, out dL, out dH, B1[i], L1[i], H1[i], sev, ell1, ell2);

                B2.Add(B1[i] + dB);
                L2.Add(L1[i] + dL);
                H2.Add(H1[i] + dH);
            }
        }

        public void Seven3d(List<double> B1, List<double> L1,
            out List<double> B2, out List<double> L2,
            SevenPara sev, Ellipsoid ell1, Ellipsoid ell2)
        {
            B2 = new List<double>();
            L2 = new List<double>();

            if (B1 == null || L1 == null || sev == null || ell1 == null || ell2 == null) return;
            if (B1.Count != L1.Count) throw new ArgumentException("Number of coordinates is not uniform");

            double dB, dL, dH;
            for (int i = 0; i < B1.Count; i++)
            {
                Seven2d(out dB, out dL, B1[i], L1[i], sev, ell1, ell2);

                B2.Add(B1[i] + dB);
                L2.Add(B1[i] + dL);
            }
        }

        public void Four(double x1, double y1, out double x2, out double y2,
            double dx, double dy, double r, double s)
        {
            double cos = Math.Cos(r);
            double sin = Math.Sin(r);
            x2 = (1 + s / 1e6) * (cos * (x1 + dx) + sin * (y1 + dy));
            y2 = (1 + s / 1e6) * (-sin * (x1 + dx) + cos * (y1 + dy));
        }

        public static void Four2d(ref double x, ref double y, double dx,  double dy,  double r,  double s, string mode = "ors")
        {
            double xx = x;
            double yy = y;

            s = 1 + s * 1e-6;
            double cos = Math.Cos(r);
            double sin = Math.Sin(r);

            if (mode.StartsWith("o"))
            {
                xx = x + dx;
                yy = y + dy;

                x = s * (cos * xx + sin * yy);
                y = s * (-sin * xx + yy * cos);
            }
            else
            {
                x = s * (cos * xx + sin * yy)+dx;
                y = s * (-sin * xx + yy * cos)+dy;
            }
        }
    }
}