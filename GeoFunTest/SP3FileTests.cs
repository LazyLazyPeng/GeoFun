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
            foreach(var epo in s.AllEpoch.Values)
            {
                foreach(var prn in epo.AllSat.Keys)
                {
                    Console.WriteLine("{0} {1} {2} {3}",prn,epo.AllSat[prn].X,epo.AllSat[prn].Y,epo.AllSat[prn].Z);
                }
            }
        }
    }
}