using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    class program
    {
        static void Main(string[] args)
        {
            TestOFile();
        }

        public static void TestOFile()
        {
            OFile ofile = new OFile(@"E:\Data\Typhoon\obs\201307_Soulik\FLNM1930.13o");

            if (ofile.TryRead())
            {
                Observation.CalP4(ref ofile.AllEpoch);
                Observation.CalL4(ref ofile.AllEpoch);
                foreach (var epoch in ofile.AllEpoch)
                {
                    Console.WriteLine(string.Format(epoch.Epoch.ToRinexString()));

                    foreach (var prn in epoch.PRNList)
                    {
                        if (prn[0] != 'G') continue;
                        Console.Write(prn);
                        foreach (var otype in new List<string> { "P1", "P2", "L1", "L2" })
                        {
                            Console.Write(" {0}:{1,13:f3}", otype, epoch[prn][otype]);
                        }
                        Console.Write(" P1-P2(P4):{0,7:f3}", epoch[prn]["P4"]);
                        Console.Write(" L1-L2(L4):{0,13:f3}", epoch[prn]["L4"]);
                        //Console.Write(" P1-C1(ns),{0,13:f3}", (epoch[prn]["P1"] - epoch[prn]["C1"])*1e9 / Common.SPEED_OF_LIGHT);
                        Console.Write("\n");
                    }
                    Console.WriteLine();
                }
            }

            Console.ReadKey();

        }
    }
}
