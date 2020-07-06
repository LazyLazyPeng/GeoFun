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
    public class TimeTests
    {
        [TestMethod()]
        public void DOYToCommonTest()
        {

        }

        [TestMethod()]
        public void CommonToDOYTest()
        {
            int month = 0, day = 0, doy = 0;

            // 闰年
            for (int i = 1; i < 367; i++)
            {
                Time.DOY2MonthDay(2012, i, out month, out day);
                doy = Time.MonthDay2DOY(2012, month, day);

                //Console.WriteLine(string.Format("{0} {1} {2} {3}", month, day, i, doy));
                Assert.IsTrue(doy == i);
            }

            // 平年
            for (int i = 1; i < 366; i++)
            {
                Time.DOY2MonthDay(2013, i, out month, out day);
                doy = Time.MonthDay2DOY(2013, month, day);

                //Console.WriteLine(string.Format("{0} {1} {2} {3}", month, day, i, doy));
                Assert.IsTrue(doy == i);
            }
        }

        [TestMethod()]
        public void GPS2YearMonthDayTest()
        {
            int year, month, dom;
            Time.GPS2YearMonthDay(1855, 0, out year, out month, out dom);
            Assert.IsTrue(year == 2015);
            Assert.IsTrue(month == 7);
            Assert.IsTrue(dom == 26);
        }

        [TestMethod()]
        public void YearMonthDay2GPSTest()
        {
            int week, dow;
            Time.YearMonthDay2GPS(2015, 7, 26, out week, out dow);
            Assert.IsTrue(week == 1855);
            Assert.IsTrue(dow == 0);
        }
    }
}