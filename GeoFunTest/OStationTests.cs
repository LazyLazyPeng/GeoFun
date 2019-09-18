using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.GNSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeoFun.GNSS.Tests
{
    [TestClass()]
    public class OStationTests
    {
        [TestMethod()]
        public void ReadAllObsDirTest()
        {
            string path = @"C:\Users\Administrator\Desktop\1";
            OStation sta = new OStation();
            sta.Name = "fjxp";
            sta.ReadAllObs(@"C:\Users\Administrator\Desktop\soulik");
            sta.SortObs();
            sta.Preprocess();
            foreach(var prn in sta.Arcs.Keys)
            {
                Console.WriteLine(prn+"*******************");
                for(int i = 0; i < sta.Arcs[prn].Count; i++)
                {
                    Console.WriteLine(string.Format("{0} {1} {2} {3}",i+1,sta.Arcs[prn][i].Length,sta.Arcs[prn][i].StartIndex,sta.Arcs[prn][i].EndIndex));

                    StringBuilder sb = new StringBuilder();
                    for(int k = 0; k < sta.Arcs[prn][i].Length;k++)
                    {
                        sb.AppendLine(string.Format("{0} {1}", sta.Arcs[prn][i].StartIndex + k, sta.Arcs[prn][i][k]["SP4"]));
                    }

                    File.WriteAllText(path+"\\"+prn+i.ToString()+".txt",sb.ToString());
                }
            }
            //Observation.DetectCycleSlip(sta.Epoches,"G01");
        }
    }
}