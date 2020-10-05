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
    public class LegendreTests
    {
        [TestMethod()]
        public void lpmvTest()
        {
            for (int i = 0; i <= 5; i++)
            {
                double a=Legendre.lpmv(5, i, Math.PI / 3);
                Console.WriteLine(a);

                    }
        }
    }
}