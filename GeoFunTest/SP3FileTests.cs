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
    public class SP3FileTests
    {
        [TestMethod()]
        public void TryReadTest()
        {
            SP3File s = new SP3File("Data/sp3/igs18554.sp3");
            s.TryRead();
            foreach (var epo in s.AllEpoch)
            {
                foreach (var prn in epo.AllSat.Keys)
                {
                    Console.WriteLine("{0} {1}", prn, epo.AllSat[prn].X, epo.AllSat[prn].Y, epo.AllSat[prn].Z);
                }
            }
        }

        [TestMethod()]
        public void GetSatPosTest()
        {
            OFile o = new OFile("Data/rinex/FLNM2111.15o");
            SP3File s = new SP3File("Data/sp3/igs18554.sp3");
            o.TryRead();
            s.TryRead();
            for (int i = 1; i < o.AllEpoch.Count; i++)
            {
                Console.WriteLine(string.Format("epoch:{0}",i));
                foreach (var prn in o.AllEpoch[i].PRNList)
                {
                    GPST t0 = o.AllEpoch[i].Epoch;
                    if(o.AllEpoch[i][prn].SatData.ContainsKey("P1"))
                    {
                        double p1 = o.AllEpoch[i][prn].SatData["P1"];
                        t0.AddSeconds(-p1/Common.C0);
                    }
                    else if(o.AllEpoch[i][prn].SatData.ContainsKey("P2"))
                    {
                        double p2 = o.AllEpoch[i][prn].SatData["P2"];
                        t0.AddSeconds(-p2/Common.C0);
                    }

                    double[] pos = s.GetSatPos(t0, prn);

                    Console.WriteLine("{0} {1} {2} {3}",prn,pos[0],pos[1],pos[2]);
                }
            }
        }
    }
}