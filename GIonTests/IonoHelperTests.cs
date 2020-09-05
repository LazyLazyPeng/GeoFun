using Microsoft.VisualStudio.TestTools.UnitTesting;
using GIon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.GNSS;
using System.IO;
using GeoFun;

namespace GIon.Tests
{
    [TestClass()]
    public class IonoHelperTests
    {
        [TestMethod()]
        public void CalculateTest()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "rinex", "30900700.11o");

            IonoHelper.Calculate(path);
        }
    }
}