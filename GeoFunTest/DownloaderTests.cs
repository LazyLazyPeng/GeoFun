using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.GNSS.Net;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.GNSS.Net.Tests
{
    [TestClass()]
    public class DownloaderTests
    {
        [TestMethod()]
        public void DownloadSp3Test()
        {
            if (File.Exists(@"Data\sp3\igs18554.sp3"))
            {
                File.Delete(@"Data\sp3\igs18554.sp3");
            }

            Downloader.DownloadSp3(1855, 4, @"Data\sp3\");
            Assert.IsTrue(File.Exists(@"Data\sp3\igs18554.sp3"));
        }
    }
}