using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GeoFun;

namespace GeoFunTest
{
    [TestClass]
    public class UnitTest1
    {
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

            Assert.IsTrue(Math.Abs(x11 - x22)<1e-5,"正向:两个模型X方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y11 - y22)<1e-5,"正向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(x33 - x44)<1e-5,"反向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y33 - y44)<1e-5,"反向:两个模型Y方向转换结果不一致");
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

            Assert.IsTrue(Math.Abs(x11 - x22)<1e-5,"正向:两个模型X方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y11 - y22)<1e-5,"正向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(x33 - x44)<1e-5,"反向:两个模型Y方向转换结果不一致");
            Assert.IsTrue(Math.Abs(y33 - y44)<1e-5,"反向:两个模型Y方向转换结果不一致");

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
                outY = (1 + four.S * 1e-6) * (-Math.Sin(four.R) * inX + Math.Cos(four.R) *inY);

                outX += four.DX;
                outY += four.DY;
            }
        }
    }
}
