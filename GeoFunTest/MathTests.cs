using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun;

namespace GeoFun.Math.Tests
{
    [TestClass()]
    public class MathTests
    {
        [TestMethod()]
        public void CalAzElTest()
        {
            double[] recPos = { -2859998.4403, 5000168.3570, 2729404.0045 };
            double[] satPos = { -21819.759014*1000, 8048.597117*1000, 13259.994053*1000 };
            double b, l, h;
            Coordinate.XYZ2BLH(recPos[0], recPos[1], recPos[2], out b, out l, out h, Ellipsoid.ELLIP_WGS84);
            Console.WriteLine("{0} {1} {2}",b,l,h);
            double az, el;
            Math.CalAzEl(recPos, satPos, out az, out el);
            Console.WriteLine(string.Format("{0} {1}",az*Angle.R2D,el*Angle.R2D));
        }
    }
}