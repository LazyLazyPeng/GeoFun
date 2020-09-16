using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.GNSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.GNSS.Tests
{
    [TestClass()]
    public class DOYTests
    {
        [TestMethod()]
        public void GetMonthTest()
        {
            DOY dd = new DOY(2015, 60);
            Assert.IsTrue(dd.GetMonth()==3);
            dd = new DOY(2012, 366);
            Assert.IsTrue(dd.GetMonth()==12);
        }
    }
}