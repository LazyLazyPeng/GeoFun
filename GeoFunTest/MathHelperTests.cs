using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.MathUtils.Tests
{
    [TestClass()]
    public class MathHelperTests
    {
        [TestMethod()]
        public void CalIonoFactorTest()
        {
            for (int i = 15; i < 80; i++)
            {
                Console.WriteLine(MathHelper.CalIonoFactor(i / 180d * Math.PI));
            }
        }
    }
}