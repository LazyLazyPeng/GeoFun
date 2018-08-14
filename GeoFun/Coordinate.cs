using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class Coordinate
    {
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
    }
}
