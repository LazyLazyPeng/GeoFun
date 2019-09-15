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
    public class OStationTests
    {
        [TestMethod()]
        public void ReadAllObsDirTest()
        {
            OStation sta = new OStation();
            sta.Name = "bjfs";
            sta.ReadAllObs(@"\\Mac\Home\Documents\Data\bjfs");
            sta.SortObs();
            Observation.DetectCycleSlip(sta.Epoches,"G01");
        }
    }
}