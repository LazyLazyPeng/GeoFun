using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.Tests
{
    [TestClass()]
    public class FourParaTests
    {
        [TestMethod()]
        public void GetInvTest()
        {
            double dx = 100;
            double dy = 100;
            double r = 1e-5;
            double s = 1;

            FourPara four1 = new FourPara
            {
                DX = dx,
                DY = dy,
                R = r,
                S = s,
            };

            FourPara four2 = FourPara.GetInv(four1);
            Console.WriteLine(four2.DX);
        }
    }
}