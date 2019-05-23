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
            for (int i = 0; i < epoches.Count; i++)
            {
                foreach (var prn in epoches[i].PRNList)
                {
                    p1 = p2 = 0d;
                    if (!epoches[i][prn].SatData.TryGetValue("P1", out p1))
                        if (!epoches[i][prn].SatData.TryGetValue("C1", out p1)) continue;

                    if (!epoches[i][prn].SatData.TryGetValue("P2", out p2)) continue;

                    if (!epoches[i][prn].SatData.ContainsKey("P4"))
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

        public static void CalL4(ref List<OEpoch> epoches)
        {
            if (epoches is null || epoches.Count == 0) return;

            double l1 = 0d;
            double l2 = 0d;
            for (int i = 0; i < epoches.Count; i++)
            {
                foreach (var prn in epoches[i].PRNList)
                {
                    l1 = l2 = 0d;
                    if (!epoches[i][prn].SatData.TryGetValue("L1", out l1)) continue;
                    if (!epoches[i][prn].SatData.TryGetValue("L2", out l2)) continue;

                    if (!epoches[i][prn].SatData.ContainsKey("L4"))
                    {
                        epoches[i][prn].SatData["L4"] = l1 * Common.GPS_L1 - l2 * Common.GPS_L2;
                    }
                    else
                    {
                        epoches[i][prn].SatData.Add("L4", l1 * Common.GPS_L1 - l2 * Common.GPS_L2);
                    }
                }
            }
        }

        /// <summary>
        /// 探测卫星的弧段
        /// </summary>
        /// <param name="epoches"></param>
        /// <param name="prn"></param>
        /// <param name="minArcLen"></param>
        /// <returns></returns>
        public static List<int[]> DetectArc(ref List<OEpoch> epoches, string prn, int interval = 30, int minArcLen = 80)
        {
            List<int[]> arcs = new List<int[]>();

            if (epoches is null || epoches.Count < minArcLen) return arcs;

            int start = -1;
            int end = -1;
            bool flag = true;
            for (int i = 0; i < epoches.Count; i++)
            {
                flag = true;

                // 间隔不对
                if ((i > 0 && (Math.Abs(epoches[i].Epoch - epoches[i - 1].Epoch - interval) > 1e-13))) flag = false;

                // 卫星缺失
                else if (!epoches[i].PRNList.Contains(prn)) flag = false;

                // 观测值缺失
                else if (!epoches[i][prn].SatData.ContainsKey("L1")) flag = false;
                else if (!epoches[i][prn].SatData.ContainsKey("L2")) flag = false;
                else if (!epoches[i][prn].SatData.ContainsKey("P2")) flag = false;
                else if (!epoches[i][prn].SatData.ContainsKey("P1")
                       &&!epoches[i][prn].SatData.ContainsKey("C1"))flag = false;
                else if (epoches[i].Flag > 0) flag = false;

                if (!flag)
                {
                    if (start >= 0)
                    {
                        end = i;
                        if (end - start + 1 >= minArcLen)
                        {
                            int[] arc = new int[] { start, end };
                            arcs.Add(arc);
                        }

                        start = -1;
                        end = -1;

                        continue;
                    }
                }
                else
                {
                    if (start < 0)
                    {
                        start = i;
                    }
                }
            }

            return arcs;
        }

        /// <summary>
        /// 探测所有卫星的弧段
        /// </summary>
        /// <param name="epoches"></param>
        /// <param name="prns">卫星prn号</param>
        /// <param name="minArcLen">最短的历元弧段</param>
        /// <returns></returns>
        public static Dictionary<string, List<int[]>> DetectArcs(ref List<OEpoch> epoches, List<string> prns, int interval = 30, int minArcLen = 80)
        {
            Dictionary<string, List<int[]>> arcs = new Dictionary<string, List<int[]>>();
            if (prns is null || prns.Count <= 0) return arcs;

            foreach (var prn in prns)
            {
                arcs.Add(prn, DetectArc(ref epoches, prn, interval, minArcLen));
            }

            return arcs;
        }
    }
}
