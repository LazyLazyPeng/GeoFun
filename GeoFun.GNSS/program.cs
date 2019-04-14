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

        }

        public static void TestOFile()
        {
            string prn = "";
            string obsType = "";
            OFile ofile = new OFile(@"E:\Data\Typhoon\201307_Soulik\FLNM1930.13o");

            if (ofile.TryRead())
            {
                var enumerator = ofile.AllEpoch.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var epoch = enumerator.Current;

                    Console.Write(string.Format("{0}:{1}:{2:00}\n",epoch.Key.CommonT.Hour,epoch.Key.CommonT.Minute,epoch.Key.CommonT.Second));
                    for (int i = 0; i < ofile.AllEpoch[epoch.Key].PRNList.Count; i++)
                    {
                        prn = ofile.AllEpoch[epoch.Key].PRNList[i];
                        Console.Write(prn);

                        for(int j = 0; j < ofile.Header.obsTypeNum;j++)
                        {
                            obsType = ofile.Header.obsTypeList[j];
                            Console.Write(string.Format(" {0} {1,13:f3}",obsType,ofile.AllEpoch[epoch.Key].AllSat[prn].SatData[obsType]));
                        }

                        Console.Write("\n");
                    }
                }
            }

            Console.ReadKey();

        }
    }
}
