using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.GNSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace GeoFun.GNSS.Tests
{
    [TestClass()]
    public class DOYTests
    {
        [TestMethod()]
        public void GetMonthTest()
        {
            DOY dd = new DOY(2015, 60);
            Assert.IsTrue(dd.GetMonth() == 3);
            dd = new DOY(2012, 366);
            Assert.IsTrue(dd.GetMonth() == 12);
        }

        [TestMethod()]
        public void AddDaysTest1()
        {
            DateTime dt1 = new DateTime(1700, 1, 1);
            DateTime dt2 = new DateTime(2500, 12, 31);
            var span = (dt2 - dt1);
            var doy = DOY.AddDays(1700001, span.Days);
            Assert.IsTrue(doy == 2500365);
        }

        [TestMethod()]
        public void AddDaysTest2()
        {
            DateTime dt1 = new DateTime(1700, 1, 1);
            DateTime dt2 = new DateTime(2500, 12, 31);
            var span = (dt2 - dt1);
            var doy = DOY.AddDays(2500365, -span.Days);
            Console.WriteLine(doy);
            Assert.IsTrue(doy == 1700001);
        }
    }
}