using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class Projection : Object
    {
        public static double PI = 3.1415926535897932;

        /// <summary>
        /// 迭代的最大次数，防止无限循环，或者因为意外的输入导致大量循环
        /// </summary>
        public static double MAXWHILELOOP = 100;

        /// <summary>
        /// 长半径(m)
        /// </summary>
        public double A { get; set; }
        /// <summary>
        /// 第一偏心率平方
        /// </summary>
        public double E1 { get; set; }
        /// <summary>
        /// 中央子午线(弧度) 
        /// </summary>
        public double L0 { get; set; } = 0;
        private double h0 = 0d;
        /// <summary>
        /// 投影抬高(m)
        /// </summary>
        public double DH { get; set; }
        /// <summary>
        /// 东方向平移(m)
        /// </summary>
        public double FalseEasting { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ell"></param>
        /// <param name="l0">中央子午线(弧度)</param>
        /// <param name="dh">投影太高(米)</param>
        /// <param name="false_easting">西向平移量(米)，默认500000m</param>
        public Projection(Ellipsoid ell, double l0 = 0, double dh = 0, double false_easting = 500000d)
        {
            A = ell.A;
            E1 = ell.E1;
            L0 = l0;
            DH = dh;
            h0 = dh;
            FalseEasting = false_easting;
        }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="a">长半径(m)</param>
        /// <param name="e1">第一偏心率平方</param>
        /// <param name="l0">中央子午线(弧度)</param>
        /// <param name="dh">投影抬高(m)</param>
        /// <param name="delta">东方向偏移量(默认500000m)</param>
        public Projection(double a = 6378137d, double e1 = 0.0066943800229008, double l0 = 0d, double dh = 0d, double delta = 500000d)
        {
            A = a;
            E1 = e1;
            L0 = l0;
            DH = dh;
            h0 = dh;
            FalseEasting = delta;
        }

        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="b">纬度(弧度)</param>
        /// <param name="l">经度(弧度)</param>
        /// <param name="x">东方向(m)</param>
        /// <param name="y">北方向(m)</param>
        public void Proj(double b, double l, ref double x, ref double y)
        {
            DH = h0;
            BL2xy(b, l, ref y, ref x);
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="x">经度(弧度),东方向(m)</param>
        /// <param name="y">纬度(弧度),北方向(m)</param>
        public void Proj(ref List<double> x, ref List<double> y)
        {
            DH = h0;
            double xtemp = 0d, ytemp = 0d;

            if (x == null || y == null || x.Count != y.Count) return;

            for (int i = 0; i < x.Count; i++)
            {
                BL2xy(y[i], x[i], ref ytemp, ref xtemp);

                x[i] = xtemp; y[i] = ytemp;
            }
        }
        /// <summary>
        /// 高斯正算
        /// </summary>
        /// <param name="x">经度(弧度),东方向(m)</param>
        /// <param name="y">纬度(弧度),北方向(m)</param>
        public void Proj(ref double x, ref double y)
        {
            DH = h0;
            BL2xy(y, x, ref y, ref x);
        }

        /// <summary>
        /// 高斯反算
        /// </summary>
        /// <param name="x">东方向(m)</param>
        /// <param name="y">北方向(m)</param>
        /// <param name="b">纬度(弧度)</param>
        /// <param name="l">经度(弧度)</param>
        public void Inverse(double x, double y, ref double b, ref double l)
        {
            DH = -h0;
            xy2BL(y, x, ref b, ref l);
        }
        /// <summary>
        /// 高斯反算
        /// </summary>
        /// <param name="x">经度(弧度),东方向(m)</param>
        /// <param name="y">纬度(弧度),北方向(m)</param>
        public void Inverse(ref List<double> x, ref List<double> y)
        {
            DH = -h0;
            double ltemp = 0d, btemp = 0d;

            if (x == null || y == null || x.Count != y.Count) return;

            for (int i = 0; i < x.Count; i++)
            {
                xy2BL(y[i], x[i], ref btemp, ref ltemp);

                x[i] = ltemp;
                y[i] = btemp;
            }
        }
        /// <summary>
        /// 高斯反算
        /// </summary>
        /// <param name="x">东方向(m),经度(弧度)</param>
        /// <param name="y">北方向(m),纬度(弧度)</param>
        public void Inverse(ref double x, ref double y)
        {
            DH = -h0;
            double b = 0d;
            double l = 0d;
            xy2BL(y, x, ref b, ref l);
            y = b;
            x = l;
        }

        /// <summary>
        /// 投影换带计算
        /// </summary>
        /// <param name="x">东方向(m)</param>
        /// <param name="y">北方向(m)</param>
        /// <param name="l1">中央子午线(度)</param>
        /// <param name="h1">投影抬高(m)</param>
        /// <param name="l2">中央子午线(度)</param>
        /// <param name="h2">投影抬高(m)</param>
        public void Reproj(ref double x, ref double y, double l1, double h1, double l2, double h2)
        {
            if (DoubleHelper.Equals(l1,l2) && DoubleHelper.Equals(h1,h2))
                return;

            L0 = l1;
            h0 = h1;

            Inverse(ref x, ref y);

            L0 = l2;
            h0 = h2;

            Proj(ref x, ref y);
        }
        /// <summary>
        /// 投影换带计算
        /// </summary>
        /// <param name="x">东方向(m)</param>
        /// <param name="y">北方向(m)</param>
        /// <param name="l1">中央子午线(度)</param>
        /// <param name="h1">投影抬高(m)</param>
        /// <param name="l2">中央子午线(度)</param>
        /// <param name="h2">投影抬高(m)</param>
        public void Reproj(ref List<double> x, ref List<double> y, double l1, double h1, double l2, double h2)
        {
            if (x == null || y == null) return;

            if (x.Count != y.Count) return;

            double xx = 0d, yy = 0d;
            for (int i = 0; i < x.Count; i++)
            {
                xx = x[i];
                yy = y[i];

                Reproj(ref xx, ref yy, l1, h1, l2, h2);

                x[i] = xx;
                y[i] = yy;
            }
        }

        /// <summary>
        /// 由BL计算xy
        /// </summary>
        /// <param name="b">纬度：弧度</param>
        /// <param name="l">经度：弧度</param>
        /// <param name="x">高斯平面坐标</param>
        /// <param name="y">高斯平面坐标</param>
        public void BL2xy(double b, double l, ref double x, ref double y)
        {
            double E2 = E1 / (1 - E1);
            double W = Math.Sqrt(1 - E1 * Math.Sin(b) * Math.Sin(b));
            double V = Math.Sqrt(1 + E2 * Math.Cos(b) * Math.Cos(b));
            ///////////////////////////投影高程面引起的变化
            double M = A * (1 - E1) / Math.Pow(W, 3);
            //B = e * Math.Sin(B) * Math.Cos(B) * (1 - e * Math.Sin(B) * Math.Sin(B))/(M*W*Math.Sqrt(1-e))*DH+B;///以平均曲率半径变化推算长半轴变化
            b = E1 * Math.Sin(b) * Math.Cos(b) / (M) * DH + b;
            W = Math.Sqrt(1 - E1 * Math.Sin(b) * Math.Sin(b));
            V = Math.Sqrt(1 + E2 * Math.Cos(b) * Math.Cos(b));
            double aa = W * DH + A;///////以某点卯酉圈曲率半径变化推算长半轴变化，偏心率不变
                                   //////////////////////////////////////////////////////////////
            double N = aa / W;
            double it2 = E2 * Math.Cos(b) * Math.Cos(b);
            double t = Math.Tan(b);
            double X = CalMeridian(b, aa);
            double dl = (l - L0);
            x = X + N / 2.0 * Math.Sin(b) * Math.Cos(b) * dl * dl + N / 24.0 * Math.Sin(b) * Math.Pow(Math.Cos(b), 3) * (5 - t * t + 9 * it2 + 4 * it2 * it2) * Math.Pow(dl, 4)
                + N / 720.0 * Math.Sin(b) * Math.Pow(Math.Cos(b), 5) * (61 - 58 * t * t + Math.Pow(t, 4)) * Math.Pow(dl, 6);
            y = N * Math.Cos(b) * dl + N / 6.0 * Math.Pow(Math.Cos(b), 3) * (1 - t * t + it2) * dl * dl * dl
                + N / 120.0 * Math.Pow(Math.Cos(b), 5) * (5 - 18 * t * t + Math.Pow(t, 4) + 14 * it2 - 58 * it2 * t * t) * Math.Pow(dl, 5);

            //// 平移量
            y += FalseEasting;
        }

        /// <summary>
        /// 由高斯平面坐标计算
        /// </summary>
        public void xy2BL(double x, double y, ref double b, ref double l)
        {
            //// 平移量
            y -= FalseEasting;

            xy2BL0(x, y, ref b, ref l);
            ///////////////////////////投影高程面引起的变化
            double e2 = E1 / (1 - E1);
            double W = Math.Sqrt(1 - E1 * Math.Sin(b) * Math.Sin(b));
            double V = Math.Sqrt(1 + e2 * Math.Cos(b) * Math.Cos(b));
            double M = this.A * (1 - E1) / Math.Pow(W, 3);
            //B = e * Math.Sin(B) * Math.Cos(B) * (1 - e * Math.Sin(B) * Math.Sin(B))/(M*W*Math.Sqrt(1-e))*DH+B;///以平均曲率半径变化推算长半轴变化
            double dB = E1 * Math.Sin(b) * Math.Cos(b) / (M) * DH;
            W = Math.Sqrt(1 - E1 * Math.Sin(b) * Math.Sin(b));
            V = Math.Sqrt(1 + e2 * Math.Cos(b) * Math.Cos(b));
            double aa = W * (-DH) + this.A;///////以某点卯酉圈曲率半径变化推算长半轴变化，偏心率不变

            //double e1 = e / (1 - e);
            double A = aa * (1 - E1) * (1 + 3.0 / 4.0 * E1 + 45.0 / 64.0 * Math.Pow(E1, 2) + 175.0 / 256.0 * Math.Pow(E1, 3) + 11025.0 / 16384.0 * Math.Pow(E1, 4));
            double B = aa * (1 - E1) * (3.0 / 4.0 * E1 + 45.0 / 64.0 * Math.Pow(E1, 2) + 175.0 / 256.0 * Math.Pow(E1, 3) + 11025.0 / 16384.0 * Math.Pow(E1, 4));
            double C = aa * (1 - E1) * (15.0 / 32.0 * Math.Pow(E1, 2) + 175.0 / 384.0 * Math.Pow(E1, 3) + 3675.0 / 8192.0 * Math.Pow(E1, 4));
            double D = aa * (1 - E1) * (35.0 / 96.0 * Math.Pow(E1, 3) + 735.0 / 2048.0 * Math.Pow(E1, 4));
            double E = aa * (1 - E1) * (315.0 / 1024.0 * Math.Pow(E1, 4));
            double dBf = 10000;
            double Bf = 0;
            double Bf0 = x / A;

            int count = 0;
            while (Math.Abs(dBf) > 0.00000000001 && count < MAXWHILELOOP)
            {
                double FBf = -B * Math.Cos(Bf0) * Math.Sin(Bf0)
                        - C * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 3)
                        - D * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 5)
                        - E * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 7);
                Bf = (x - FBf) / A;
                dBf = Bf - Bf0;
                Bf0 = Bf;

                count++;
            }

            double Mf = aa * (1 - E1) * Math.Pow(1 - E1 * Math.Pow(Math.Sin(Bf), 2), -1.5);
            double Nf = aa * Math.Pow(1 - E1 * Math.Pow(Math.Sin(Bf), 2), -0.5);
            double tf = Math.Tan(Bf);
            double it2f = e2 * Math.Pow(Math.Cos(Bf), 2);
            b = Bf - tf / (2 * Mf * Nf) * y * y + tf / (24 * Mf * Math.Pow(Nf, 3)) * (5 + 3 * tf * tf + it2f - 9 * it2f * tf * tf) * Math.Pow(y, 4)
                - tf / (720.0 * Mf * Math.Pow(Nf, 5)) * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(y, 6);
            b = b + dB;
            l = L0 + 1.00 / (Nf * Math.Cos(Bf)) * y - 1.0 / (6 * Math.Pow(Nf, 3) * Math.Cos(Bf)) * (1 + 2 * tf * tf + it2f) * Math.Pow(y, 3) +
                1.0 / (120.0 * Math.Pow(Nf, 5) * Math.Cos(Bf)) * (5 + 28 * tf * tf + 24 * tf * tf * tf * tf + 6 * it2f + 8 * it2f * tf * tf) * Math.Pow(y, 5);
        }

        /// <summary>
        /// 求对应纬度的子午线弧长
        /// </summary>
        /// <param name="B">弧度</param>
        /// <returns></returns>
        public double CalMeridian(double B, double aa)
        {
            double m0 = aa * (1 - E1);
            double m2 = 3.0 / 2.0 * E1 * m0;
            double m4 = 5.0 / 4.0 * E1 * m2;
            double m6 = 7.0 / 6.0 * E1 * m4;
            double m8 = 9.0 / 8.0 * E1 * m6;

            double a0 = m0 + m2 / 2.0 + 3.0 / 8.0 * m4 + 5.0 / 16.0 * m6 + 35.0 / 128.0 * m8;
            double a2 = m2 / 2.0 + m4 / 2.0 + 15.0 / 32.0 * m6 + 7.0 / 16.0 * m8;
            double a4 = m4 / 8.0 + 3.0 / 16.0 * m6 + 7.0 / 32.0 * m8;
            double a6 = m6 / 32.0 + m8 / 16.0;
            double a8 = m8 / 128.0;

            double X = a0 * B - a2 / 2.0 * Math.Sin(2 * B) + a4 / 4.0 * Math.Sin(4 * B) - a6 / 6.0 * Math.Sin(6 * B) + a8 / 8.0 * Math.Sin(8 * B);
            return X;
        }

        /// <summary>
        /// 由高斯平面坐标计算
        /// </summary>
        public void xy2BL0(double x, double y, ref double b, ref double l)
        {
            double E2 = E1 / (1 - E1);
            double AA = this.A * (1 - E1) *
                (1 + 3.0 / 4.0 * E1 +
                45.0 / 64.0 * Math.Pow(E1, 2) +
                175.0 / 256.0 * Math.Pow(E1, 3) +
                11025.0 / 16384.0 * Math.Pow(E1, 4));
            double B = this.A * (1 - E1) *
                (3.0 / 4.0 * E1 +
                45.0 / 64.0 * Math.Pow(E1, 2) +
                175.0 / 256.0 * Math.Pow(E1, 3) +
                11025.0 / 16384.0 * Math.Pow(E1, 4));
            double C = this.A * (1 - E1) *
                (15.0 / 32.0 * Math.Pow(E1, 2) +
                175.0 / 384.0 * Math.Pow(E1, 3) +
                3675.0 / 8192.0 * Math.Pow(E1, 4));
            double D = this.A * (1 - E1) *
                (35.0 / 96.0 * Math.Pow(E1, 3) +
                735.0 / 2048.0 * Math.Pow(E1, 4));
            double E = this.A * (1 - E1) *
                (315.0 / 1024.0 * Math.Pow(E1, 4));
            double dBf = 10000;
            double Bf = 0;
            double Bf0 = x / AA;

            int k = 0; ////最多循环10000次
            while (Math.Abs(dBf) > 0.00000000001 && k < MAXWHILELOOP)
            {
                double FBf = -B * Math.Cos(Bf0) * Math.Sin(Bf0)
                        - C * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 3)
                        - D * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 5)
                        - E * Math.Cos(Bf0) * Math.Pow(Math.Sin(Bf0), 7);
                Bf = (x - FBf) / AA;
                dBf = Bf - Bf0;
                Bf0 = Bf;
                k++;
            }

            double Mf = this.A * (1 - E1) * Math.Pow(1 - E1 * Math.Pow(Math.Sin(Bf), 2), -1.5);
            double Nf = this.A * Math.Pow(1 - E1 * Math.Pow(Math.Sin(Bf), 2), -0.5);
            double tf = Math.Tan(Bf);
            double it2f = E2 * Math.Pow(Math.Cos(Bf), 2);
            b = Bf - tf / (2 * Mf * Nf) * y * y + tf / (24 * Mf * Math.Pow(Nf, 3)) * (5 + 3 * tf * tf + it2f - 9 * it2f * tf * tf) * Math.Pow(y, 4)
                - tf / (720.0 * Mf * Math.Pow(Nf, 5)) * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(y, 6);
            l = L0 + 1.00 / (Nf * Math.Cos(Bf)) * y - 1.0 / (6 * Math.Pow(Nf, 3) * Math.Cos(Bf)) * (1 + 2 * tf * tf + it2f) * Math.Pow(y, 3) +
                1.0 / (120.0 * Math.Pow(Nf, 5) * Math.Cos(Bf)) * (5 + 28 * tf * tf + 24 * tf * tf * tf * tf + 6 * it2f + 8 * it2f * tf * tf) * Math.Pow(y, 5);
        }

        /// <summary>
        /// 从y坐标判断出投影信息
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public static bool GetPrjFromCoordinate(double y, Ellipsoid ell, out Projection prj)
        {
            prj = null;

            //// 坐标在7位到8位，有带号
            if (y > 1e7 && y < 1e9)
            {
                double band = Math.Floor(y / 1e6);

                double l0 = 0;

                //// 对于山西来说3度带带号为37或38
                if (band > 20)
                {
                    l0 = band * 3;
                }

                //// 6度带为18或19
                else
                {
                    l0 = band * 6 - 3;
                }

                prj = new Projection(ell, l0, 0, band * 1e6 + 500000d);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从y坐标判断出投影信息
        /// </summary>
        /// <param name="y"></param>
        /// <param name="l0"></param>
        /// <param name="falseEasting"></param>
        /// <returns></returns>
        public static bool GetPrjFromCoordinate(double y, out double l0, out double falseEasting)
        {
            l0 = 0d; falseEasting = 0d;

            //// 坐标在7位到8位，有带号
            if (y > 1e6 && y < 1e8)
            {
                double band = Math.Floor(y / 1e6);

                //// 对于山西来说3度带带号为37或38
                if (band > 20)
                {
                    l0 = band * 3;

                    falseEasting = band * 1e6 + 5e5;
                }

                //// 6度带为18或19
                else
                {
                    l0 = band * 6 - 3;
                    falseEasting = band * 1e6 + 500000d;
                }

                l0 *= Angle.D2R;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
