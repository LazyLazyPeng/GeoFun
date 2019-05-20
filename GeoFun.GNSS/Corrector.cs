using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Corrector
    {
        /// <summary>
        /// 将C1观测值改正到P1
        /// </summary>
        /// <param name="epoches"></param>
        /// <param name="dcbs"></param>
        public static void CorrectC1P1(ref List<OEpoch> epoches, Dictionary<string, double> dcbs)
        {
            if (epoches is null || epoches.Count == 0) return;

            double p1 = 0d;
            double c1 = 0d;
            double dcb = 0d;
            for (int i = 0; i < epoches.Count; i++)
            {
                foreach (string prn in epoches[i].PRNList)
                {
                    c1 = p1 = 0d;

                    // 没有该卫星的dcb
                    if (!dcbs.TryGetValue(prn, out dcb)) continue;

                    // 已经有P1观测值,不用改C1观测值
                    if (epoches[i][prn].SatData.TryGetValue("P1", out p1)) continue;

                    // 没有C1观测值,无法改正
                    if (!epoches[i][prn].SatData.TryGetValue("C1", out c1)) continue;

                    // 将C1改正到P1 P1 = C1-DCB_P1C1
                    epoches[i][prn]["P1"] = c1 + dcb * 1e-9 * Common.SPEED_OF_LIGHT;
                }
            }
        }

        /// <summary>
        /// 改正P1P2间的硬件延迟，卫星端
        /// </summary>
        /// <param name="epoches"></param>
        /// <param name="dcbs"></param>
        public static void CorrectP1P2(ref List<OEpoch> epoches, Dictionary<string, double> dcbs)
        {
            if (epoches is null || epoches.Count == 0) return;

            double p2 = 0d;
            double p1p2 = 0d;
            for (int i = 0; i < epoches.Count; i++)
            {
                foreach (string prn in epoches[i].PRNList)
                {
                    p2 = p1p2 = 0d;

                    // 没有该卫星的dcb
                    if (!dcbs.TryGetValue(prn, out p1p2)) continue;

                    // 没有P2观测值,无法改正
                    if (!epoches[i][prn].SatData.TryGetValue("P2", out p2)) continue;

                    // 改正硬件延迟P1P2
                    epoches[i][prn]["P2"] = p2 - dcbs[prn] * 1e9 * Common.SPEED_OF_LIGHT;
                }
            }
        }
    }
}
