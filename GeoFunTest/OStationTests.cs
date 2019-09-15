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
            sta.Preprocess();
            foreach(var prn in sta.Arcs.Keys)
            {
                Console.WriteLine(prn+"*******************");
                for(int i = 0; i < sta.Arcs[prn].Count; i++)
                {
                    Console.WriteLine(string.Format("{0} {1} {2} {3}",i+1,sta.Arcs[prn][i].Length,sta.Arcs[prn][i].StartIndex,sta.Arcs[prn][i].EndIndex));
                }
            }
            //Observation.DetectCycleSlip(sta.Epoches,"G01");
        }
    }
}