using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Observation
    {
        public static void CalP4(ref List<OEpoch> epoches)
        {
            if (epoches is null || epoches.Count == 0) return;

            double p1 = 0d;
            double p2 = 0d;
            for(int i = 0; i < epoches.Count; i++)
            {
                foreach(var prn in epoches[i].PRNList)
                {
                    p1 = p2 = 0d;
                    if (!epoches[i][prn].SatData.TryGetValue("P1", out p1))
                        if (!epoches[i][prn].SatData.TryGetValue("C1", out p1)) continue;

                    if (!epoches[i][prn].SatData.TryGetValue("P2", out p2)) continue;

                    if(!epoches[i][prn].SatData.ContainsKey("P4"))
                    {
                        epoches[i][prn].SatData["P4"] = p1 - p2;
                    }
                    else
                    {
                        epoches[i][prn].SatData.Add("P4", p1 - p2);
                    }
                }
            }
        }
    }
}
