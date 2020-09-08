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
    public class FileNameTests
    {
        [TestMethod()]
        public void ParseSP3NameTest()
        {
            string center;
            int week, dow;
            FileName.ParseSP3Name("igs18013.sp3", out center, out week, out dow);
            Assert.IsTrue(center=="igs");
            Assert.IsTrue(week==1801);
            Assert.IsTrue(dow==3);

            FileName.ParseSP3Name("igs18013", out center, out week, out dow);
            Assert.IsTrue(center == "igs");
            Assert.IsTrue(week == 1801);
            Assert.IsTrue(dow == 3);
        }
    }
}