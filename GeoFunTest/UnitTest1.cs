using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GeoFun;
using GeoFun.IO;
using System.Collections.Generic;

namespace GeoFunTest
{
    [TestClass]
    public class UnitTest1
    {
        [DllImport("GeoFunC.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Sum(int a, int b);
        [DllImport("GeoFunC.DLL", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FourCal(int num,
        [In,Out] double[] x1, [In,Out] double[] y1,[In,Out] double[] x2,[In,Out] double[] y2,
        [In,Out] double dx, [In,Out] double dy, [In,Out] double r, [In,Out] double s);

        [TestMethod]
        public void TestFourYao()
        {
            double[] x1 = { 0, 500, 0 };
            double[] y1 = { 0, 0, 500 };

            double[] x2 = { 0, 0, 0 };
            double[] y2 = { 0, 0, 0 };

            double dx1 = 35.389;
            double dy1 = 127.442;
            double r1 = 2.53 / 3600*Angle.D2R;
            double s1 = 1.53;

            double sin = Math.Sin(r1);
            double cos = Math.Cos(r1);
            for(int i = 0; i < 3; i++)
            {
                x2[i] = (1 + s1 * 1e-6)*(cos * (x1[i]+dx1) + sin * (y1[i]+dy1));
                y2[i] = (1 + s1 * 1e-6)*(-sin * (x1[i]+dx1) + cos * (y1[i]+dy1));
            }

            double dx2 = 0, dy2=0, r2=0, s2=0;
            FourCal(3, x1, y1, x2, y2, dx2, dy2, r2, s2);

            Assert.IsTrue(Math.Abs(dx1-dx2)<1e-3);
            Assert.IsTrue(Math.Abs(dy1-dy2)<1e-3);
            Assert.IsTrue(Math.Abs(r1-r2)<1e-4);
            Assert.IsTrue(Math.Abs(s1-s2)<1e-4);
        }

        [TestMethod]
        public void TestSum()
        {
            Assert.IsTrue(Sum(1, 2) == 3);
        }

        [TestMethod]
        public void TestFourPara1()
        {
            double x1 = 475000;
            double y1 = 3754000;
            double dx1 = 500;
            double dy1 = 300;
            double r1 = 10d / 3600d * 4d * Math.Atan(1d);
            double s1 = 5d;

            FourPara four1 = new FourPara { Mode = enumFourMode.ORS, DX = dx1, DY = dy1, R = r1, S = s1 };
            FourPara four2 = FourPara.ChangeMode(four1, enumFourMode.RSO);
            FourPara four3 = FourPara.GetInv(four1);
            FourPara four4 = FourPara.GetInv(four2);

            double x2 = 0d, y2 = 0d, x3 = 0d, y3 = 0d, x4 = 0d, y4 = 0d;
            double x11 = 0d, y11 = 0d, x22 = 0d, y22 = 0d, x33 = 0d, y33 = 0d, x44 = 0d, y44 = 0d;
            FourTrans(four1, x1, y1, ref x11, ref y11);
            FourTrans(four2, x1, y1, ref x22, ref y22);
            FourTrans(four3, x11, y11, ref x33, ref y33);
            FourTrans(four4, x22, y22, ref x44, ref y44);

            Assert.IsTrue(Math.Abs(x11 - x22) < 1e-5, "正向:两个模型X方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y11 - y22) < 1e-5, "正向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(x33 - x44) < 1e-5, "反向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y33 - y44) < 1e-5, "反向:两个模型Y方向转换结果不一致");
        }

        [TestMethod]
        public void TestFourPara2()
        {
            double x1 = 475000;
            double y1 = 3754000;
            double dx1 = 500;
            double dy1 = 300;
            double r1 = 10d / 3600d * 4d * Math.Atan(1d);
            double s1 = 5d;

            FourPara four1 = new FourPara { Mode = enumFourMode.RSO, DX = dx1, DY = dy1, R = r1, S = s1 };
            FourPara four2 = FourPara.ChangeMode(four1, enumFourMode.ORS);
            FourPara four3 = FourPara.GetInv(four1);
            FourPara four4 = FourPara.GetInv(four2);

            double x11 = 0d, y11 = 0d, x22 = 0d, y22 = 0d, x33 = 0d, y33 = 0d, x44 = 0d, y44 = 0d;
            FourTrans(four1, x1, y1, ref x11, ref y11);
            FourTrans(four2, x1, y1, ref x22, ref y22);
            FourTrans(four3, x11, y11, ref x33, ref y33);
            FourTrans(four4, x22, y22, ref x44, ref y44);

            Assert.IsTrue(Math.Abs(x11 - x22) < 1e-5, "正向:两个模型X方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y11 - y22) < 1e-5, "正向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(x33 - x44) < 1e-5, "反向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y33 - y44) < 1e-5, "反向:两个模型Y方向转换结果不一致");

        }

        [TestMethod]
        public void DD2DMS()
        {
        }

        /// <summary>
        /// 测试四参数计算的精度
        /// </summary>
        [TestMethod]
        public void FourCal()
        {
            double dx, dy, r, s = 0;
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                dx = rand.Next(1, 200) + rand.NextDouble();
                dy = rand.Next(50, 500) + rand.NextDouble();
                r = (rand.Next(0, 10) + rand.NextDouble()) / 3600d * Angle.D2R;
                s = rand.Next(0, 10) + rand.NextDouble();

                List<double> x1 = new List<double>();
                List<double> x2 = new List<double>();
                List<double> y1 = new List<double>();
                List<double> y2 = new List<double>();

                double xx1, yy1, xx2, yy2;
                for (int j = 0; j < 4; j++)
                {
                    xx1 = rand.Next(-300000, 300000) + rand.NextDouble();
                    yy1 = rand.Next(-3000000, 3000000) + rand.NextDouble();

                    xx2 = (1 + s * 1e-6) * (Math.Cos(r) * xx1 + Math.Sin(r) * yy1) + dx;
                    yy2 = (1 + s * 1e-6) * (-Math.Sin(r) * xx1 + Math.Cos(r) * yy1) + dy;

                    x1.Add(xx1);
                    y1.Add(yy1);
                    x2.Add(xx2);
                    y2.Add(yy2);
                }

                FourPara four = FourPara.CalPara(x1, y1, x2, y2);

                Assert.IsTrue(Math.Abs(four.DX - dx) < 1e-5);
                Assert.IsTrue(Math.Abs(four.DY - dy) < 1e-5);
                Assert.IsTrue(Math.Abs(four.R - r) < 1e-5);
                Assert.IsTrue(Math.Abs(four.S - s) < 1e-9, string.Format("{0} {1}", s, four.S));
            }
        }

        [TestMethod]
        public void TestFourPara3()
        {
            string path = @"C: \Users\niuni\Desktop\common802000.txt";
            List<string[]> lines = FileHelper.ReadThenSplitLine(path, ' ');

            List<double> x1 = new List<double>();
            List<double> y1 = new List<double>();
            List<double> x2 = new List<double>();
            List<double> y2 = new List<double>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == null || lines[i].Length < 5) continue;

                x1.Add(double.Parse(lines[i][1]));
                y1.Add(double.Parse(lines[i][2]));
                x2.Add(double.Parse(lines[i][3]));
                y2.Add(double.Parse(lines[i][4]));
            }

            FourPara four = FourPara.CalParaIter(x1, y1, x2, y2);
        }

        [TestMethod]
        public void SevenCal()
        {
            double dx = 0d, dy = 0d, dz = 0d;
            double rx = 0d, ry = 0d, rz = 0d;
            double s = 0;
            Trans trans = new Trans();

            Ellipsoid ell1 = Ellipsoid.ELLIP_XIAN80;
            Ellipsoid ell2 = Ellipsoid.ELLIP_CGCS2000;

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                dx = rand.Next(1, 200) + rand.NextDouble();
                dy = rand.Next(50, 500) + rand.NextDouble();
                dz = rand.Next(50, 500) + rand.NextDouble();
                rx = (rand.Next(0, 10) / 10d + rand.NextDouble()) / 3600d * Angle.D2R;
                ry = (rand.Next(0, 10) / 10d + rand.NextDouble()) / 3600d * Angle.D2R;
                rz = (rand.Next(0, 10) / 10d + rand.NextDouble()) / 3600d * Angle.D2R;
                s = rand.Next(0, 10) + rand.NextDouble();

                List<double> b1 = new List<double>();
                List<double> l1 = new List<double>();
                List<double> h1 = new List<double>();
                List<double> b2 = new List<double>();
                List<double> l2 = new List<double>();
                List<double> h2 = new List<double>();

                SevenPara sev1 = new SevenPara
                {
                    XOff = dx,
                    YOff = dy,
                    ZOff = dz,
                    XRot = rx,
                    YRot = ry,
                    ZRot = rz,
                    M = s,
                };

                double bb1, ll1, hh1;
                double db, dl, dh;
                for (int j = 0; j < 4; j++)
                {
                    bb1 = (rand.Next(23, 30) + rand.NextDouble()) * Angle.D2R;
                    ll1 = (rand.Next(123, 130) + rand.NextDouble()) * Angle.D2R;
                    hh1 = rand.Next(-100, 500) + rand.NextDouble();

                    trans.Seven3d(out db, out dl, out dh, bb1, ll1, hh1, sev1, ell1, ell2);

                    b1.Add(bb1);
                    l1.Add(ll1);
                    h1.Add(hh1);

                    b2.Add(bb1 + db);
                    l2.Add(ll1 + dl);
                    h2.Add(hh1 + dh);
                }

                SevenPara sev2 = SevenPara.CalPara(b1, l1, h1, b2, l2, h2, ell1, ell2);

                Assert.IsTrue(Math.Abs(sev2.XOff - sev1.XOff) < 1e-4);
                Assert.IsTrue(Math.Abs(sev2.YOff - sev1.YOff) < 1e-4);
                Assert.IsTrue(Math.Abs(sev2.ZOff - sev1.ZOff) < 1e-4);
                Assert.IsTrue(Math.Abs(sev2.XRot - sev1.XRot) < 1e-5);
                Assert.IsTrue(Math.Abs(sev2.YRot - sev1.YRot) < 1e-5);
                Assert.IsTrue(Math.Abs(sev2.ZRot - sev1.ZRot) < 1e-5);
                Assert.IsTrue(Math.Abs(sev2.M - sev1.M) < 1e-5);
            }
        }

        [TestMethod]
        public void SevenCal2()
        {
            string path = @"C:\Users\niuni\Desktop\3.txt";
            var lines = FileHelper.ReadThenSplitLine(path, ' ');
            List<double> b1 = new List<double>();
            List<double> l1 = new List<double>();
            List<double> h1 = new List<double>();
            List<double> b2 = new List<double>();
            List<double> l2 = new List<double>();
            List<double> h2 = new List<double>();

            double b = 0, l = 0, h = 0;
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == null || lines[i].Length < 7) continue;
                b = Angle.DMS2Arc(double.Parse(lines[i][1]));
                l = Angle.DMS2Arc(double.Parse(lines[i][2]));
                h = double.Parse(lines[i][3]);

                b1.Add(b);
                l1.Add(l);
                h1.Add(h);

                b = Angle.DMS2Arc(double.Parse(lines[i][4]));
                l = Angle.DMS2Arc(double.Parse(lines[i][5]));
                h = double.Parse(lines[i][6]);

                b2.Add(b);
                l2.Add(l);
                h2.Add(h);
            }

            SevenPara sev = SevenPara.CalPara(b1, l1, h1, b2, l2, h2, Ellipsoid.ELLIP_XIAN80, Ellipsoid.ELLIP_CGCS2000);

            Assert.IsTrue(Math.Abs(sev.XOff - 4) < 1e-3);
            Assert.IsTrue(Math.Abs(sev.YOff - 5) < 1e-3);
            Assert.IsTrue(Math.Abs(sev.ZOff - 6) < 1e-3);
            Assert.IsTrue(Math.Abs(sev.XRot - 1) < 1e-4);
            Assert.IsTrue(Math.Abs(sev.YRot - 2) < 1e-4);
            Assert.IsTrue(Math.Abs(sev.ZRot - 3) < 1e-4);
            Assert.IsTrue(Math.Abs(sev.M - 1) < 1e-4);
        }

        private void FourTrans(FourPara four, double inX, double inY, ref double outX, ref double outY)
        {
            if (four is null) return;

            if (four.Mode == enumFourMode.ORS || four.Mode == enumFourMode.OSR)
            {
                outX = (1 + four.S * 1e-6) * (Math.Cos(four.R) * (inX + four.DX) + Math.Sin(four.R) * (inY + four.DY));
                outY = (1 + four.S * 1e-6) * (-Math.Sin(four.R) * (inX + four.DX) + Math.Cos(four.R) * (inY + four.DY));
            }
            else
            {
                outX = (1 + four.S * 1e-6) * (Math.Cos(four.R) * inX + Math.Sin(four.R) * inY);
                outY = (1 + four.S * 1e-6) * (-Math.Sin(four.R) * inX + Math.Cos(four.R) * inY);

                outX += four.DX;
                outY += four.DY;
            }
        }
    }
}
