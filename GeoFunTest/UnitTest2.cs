using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using GeoFun;
using GeoFun.GNSS;

namespace GeoFunTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            DateTime dt1, dt2;
            ulong pico1, pico2;
            Week week1, week2;

            dt1 = new DateTime(2002, 1, 1, 3, 59, 59);
            pico1 = (ulong)(5 * 1e11);
            week1 = Time.CommonToGPS(dt1, pico1);

            Time.GPSToCommon(week1, out dt2, out pico2);

            Assert.IsTrue(dt1 == dt2, "dt1不等于dt2");
            Assert.IsTrue(pico1==pico2, "dt1不等于dt2");
        }

        [TestMethod]
        public void TestSP3File()
        {
            SP3File sp3 = new SP3File(@"E:\Data\Typhoon\201307_Soulik\cod17484.sp3");
            sp3.TryRead();
        }

        [TestMethod]
        public void TestOFile()
        {
            OrderedDictionary od = new OrderedDictionary();
            OFile ofile = new OFile(AppDomain.CurrentDomain.BaseDirectory+"\\Data\\fjpt1930.13o");
            //ofile.TryRead();
            Console.WriteLine(".........");
        }

    }
}
