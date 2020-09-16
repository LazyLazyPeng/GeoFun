using System;
using System.Collections.Generic;
using System.IO;
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
            //OFile ofile = new OFile(@"E:\Data\ZhaiCZ\2011070test\30900700.11o");
            OFile ofile = new OFile(@"E:\Data\Typhoon\obs\201307_Soulik\FLNM1930.13o");

            if (ofile.TryRead())
            {
                //List<string> prns = new List<string>();
                //for(int i = 0; i <32; i++)
                //{
                //    prns.Add("G" + (i+1).ToString("00"));
                //}
                //var arcs = Observation.DetectArcs(ref ofile.AllEpoch, prns);

                //foreach(var prn in prns)
                //{
                //    if (arcs[prn].Count < 1) continue;

                //    Console.WriteLine(prn);
                //    foreach(var arc in arcs[prn])
                //    {
                //        Console.Write(" {0},{1},{2}",arc[0],arc[1],arc[1]-arc[0]+1);
                //    }
                //    Console.Write("\r\n");
                //}
                //Console.ReadKey();
                //return;

                ObsHelper.CalP4(ref ofile.Epoches);
                ObsHelper.CalL4(ref ofile.Epoches);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < ofile.Epoches.Count; i++)
                {
                    var epoch = ofile.Epoches[i];
                    Console.WriteLine(string.Format(epoch.Epoch.ToRinexString()));

                    if (epoch.PRNList.Contains("G01"))
                    {
                        sb.AppendFormat("{0} {1}\n", epoch["G01"]["P4"], epoch["G01"]["L4"]);
                    }
                    else
                    {
                        sb.Append("0 0\n");
                    }


                    foreach (var prn in epoch.PRNList)
                    {
                        if (prn[0] != 'G') continue;
                        Console.Write(prn);
                        //foreach (var otype in new List<string> { "P1", "P2", "L1", "L2" })
                        //{
                        //    Console.Write(" {0}:{1,13:f3}", otype, epoch[prn][otype]);
                        //}
                        double c1, p1;
                        if (epoch[prn].SatData.TryGetValue("C1", out c1) &&
                           epoch[prn].SatData.TryGetValue("P1", out p1))
                        {
                            Console.Write(" P1-C1:{0,7:f3}", p1 - c1);
                        }
                        Console.Write(" P1-P2(P4):{0,7:f3}", epoch[prn]["P4"]);
                        Console.Write(" L1-L2(L4):{0,13:f3}", epoch[prn]["L4"]);
                        if (i > 0 &&
                            ofile.Epoches[i - 1].PRNList.Contains(prn) &&
                            ofile.Epoches[i - 1][prn].SatData.ContainsKey("L4"))
                        {
                            Console.Write(" L4(i)-L4(i-1):{0}", ofile.Epoches[i][prn]["L4"] - ofile.Epoches[i - 1][prn]["L4"]);
                        }
                        //Console.Write(" P1-C1(ns),{0,13:f3}", (epoch[prn]["P1"] - epoch[prn]["C1"])*1e9 / Common.SPEED_OF_LIGHT);

                        Console.Write("\n");
                    }
                    Console.WriteLine();
                }

                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "test\\test.txt", sb.ToString());
            }

            Console.ReadKey();

        }
    }
}
