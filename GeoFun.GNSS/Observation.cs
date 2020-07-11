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
                    if (!epoches[i][prn].SatData.TryGetValue("P1", out p1)
                        && !epoches[i][prn].SatData.TryGetValue("C1", out p1)) continue;

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

        public static void GetMeas(ref List<OEpoch> epoches, string measName)
        {
            if (measName == "P4")
            {
                CalP4(ref epoches);
            }
            else if (measName == "L4")
            {
                CalL4(ref epoches);
            }
            else { }
        }

        /// <summary>
        /// 探测粗差
        /// </summary>
        public static void DetectOutlier()
        {

        }

        /// <summary>
        /// 探测周跳
        /// </summary>
        /// <remarks>
        /// GPS周跳探测与修复的算法研究与实现.彭秀英.2004
        /// </remarks>
        public static bool DetectCycleSlip(ref OArc arc,out int index)
        {
            index = -1;

            // i-1历元宽巷模糊度估计值
            double NW1 = 0d;
            // i历元宽巷模糊度估计值
            double NW2 = 0d;
            // i+1历元宽巷模糊度估计值
            double NW3 = 0d;

            // i-1历元宽巷模糊度估计值精度
            double delta1 = 0d;
            // i历元宽巷模糊度估计值精度
            double delta2 = 0d;

            // GPS L1频率(Hz)
            double f1 = Common.GPS_F1;
            // GPS L2频率(Hz)
            double f2 = Common.GPS_F2;

            // GPS L1波长(m)
            double l1 = Common.GPS_L1;
            // GPS L2波长(m)
            double l2 = Common.GPS_L2;

            // L1精度(m)
            double dL1 = Common.DELTA_L1;
            // L2精度(m)
            double dL2 = Common.DELTA_L2;
            // P1精度(m)
            double dP1 = Common.DELTA_P1;
            // P2精度(m)
            double dP2 = Common.DELTA_P2;

            // 初始化NW(i-1)
            NW1 = (1 / (f1 - f2) *
                  (f1 * arc[0]["L1"] * l1 - f2 * arc[0]["L2"] * l2) -
                  1 / (f1 + f2) *
                  (f1 * arc[0]["P1"] + f2 * arc[0]["P2"])) / Common.GPS_Lw;
            // 初始化NW(i)
            NW2 = (1 / (f1 - f2) *
                  (f1 * arc[1]["L1"] * l1 - f2 * arc[1]["L2"] * l2) -
                  1 / (f1 + f2) *
                  (f1 * arc[1]["P1"] + f2 * arc[1]["P2"])) / Common.GPS_Lw;

            // 初始化δ(i-1)
            delta1 = Math.Sqrt(
                1 / Math.Pow(f1 - f2, 2) * (f1 * f1 * dL1 + f2 * f2 * dL2) +
                1 / Math.Pow(f1 + f2, 2) * (f1 * f1 * dP1 + f2 * f2 + dP2)
                );

            int arcLen = arc.Length - 1;
            for (int i = 1; i < arcLen; i++)
            {

                NW3 = (1 / (f1 - f2) *
                      (f1 * arc[i + 1]["L1"] * l1 - f2 * arc[i + 1]["L2"] * l2) -
                      1 / (f1 + f2) *
                      (f1 * arc[i + 1]["P1"] + f2 * arc[i + 1]["P2"])) / Common.GPS_Lw;

                delta2 = Math.Sqrt(delta1 * delta1 * i / (i + 1) + Math.Pow(NW2 - NW1, 2) / (i + 1));

                if (Math.Abs(NW2 - NW1) > 4 * delta1)
                {
                    if (Math.Abs(NW3 - NW2) < 1)
                    {
                        arc[i].CycleSlip = true;
                    }
                    else
                    {
                        arc[i].Outlier = true;
                    }

                    // 有周跳，分割成新弧段
                    //OArc newArc = arc.Split(i + 1);
                    //arc.Station.Arcs[arc.PRN].Add(newArc);
                    index = i + 1;
                    return true;
                }

                NW1 = NW1 * i / (i + 1) + NW2 / (i + 1);
                NW2 = NW3;
                delta1 = delta2;
            }
            return false;
        }

        /// <summary>
        /// 探测钟跳
        /// </summary>
        public static void DetectClockJumpAndRepair(ref List<OEpoch> epoches, int interval = 30)
        {
            if (epoches is null || epoches.Count < 1) return;

            // 测量噪声(m）
            double sigma = 5;
            double k1 = 1e-3 * Common.C0 - 3 * sigma;
            double k2 = 1e-4 * Common.C0;

            // 当前历元的观测值
            double curL1 = 0d, curL2 = 0d, curP1 = 0d, curP2 = 0d, curC1 = 0d;
            // 上一历元的观测值
            double lstL1 = 0d, lstL2 = 0d, lstP1 = 0d, lstP2 = 0d, lstC1 = 0d;

            // dP = P1(i) - P1(i-1)
            double dP = 0d;
            // dL = L1(i) - L1(i-1)
            double dL = 0d;

            // dP4 = P4(i)-P4(i-1)
            double dP4 = 0d;
            // dL4 = L4(i)-L4(i-1)
            double dL4 = 0d;

            // 判定为异常的卫星数(毫秒级)
            int jumpSatNumMs = 0;
            // 判定为异常的卫星数(毫秒级)
            int jumpSatNumUs = 0;
            // 可用的卫星数
            int validSatNum = 0;

            // 所有卫星毫秒级钟跳总和(米)
            double jumpAllSatMs = 0d;
            // 所有卫星微秒级钟跳总和(米)
            double jumpAllSatUs = 0d;

            // 平均每颗卫星的钟跳,小数值(s)
            double jumpPerSatDec = 0d;
            // 平均每颗卫星的钟跳,整数值(s)
            double jumpPerSatInt = 0d;

            for (int i = 1; i < epoches.Count; i++)
            {
                // 历元不连续
                if (epoches[i].Epoch - epoches[i - 1].Epoch > (interval + 1e-14)) continue;

                validSatNum = 0;
                jumpSatNumMs = 0;
                jumpSatNumUs = 0;
                jumpAllSatMs = 0d;
                jumpAllSatUs = 0d;
                foreach (var prn in epoches[i].PRNList)
                {
                    if (!prn.StartsWith("G")) continue;
                    if (!epoches[i - 1].PRNList.Contains(prn)) continue;

                    if (!epoches[i][prn].SatData.TryGetValue("L1", out curL1)) continue;
                    if (!epoches[i][prn].SatData.TryGetValue("L2", out curL2)) continue;
                    if (!epoches[i][prn].SatData.TryGetValue("P2", out curP2)) continue;
                    if (!epoches[i][prn].SatData.TryGetValue("P1", out curP1) &&
                        !epoches[i][prn].SatData.TryGetValue("C1", out curP1)) continue;

                    if (!epoches[i - 1][prn].SatData.TryGetValue("L1", out lstL1)) continue;
                    if (!epoches[i - 1][prn].SatData.TryGetValue("L2", out lstL2)) continue;
                    if (!epoches[i - 1][prn].SatData.TryGetValue("P2", out lstP2)) continue;
                    if (!epoches[i - 1][prn].SatData.TryGetValue("P1", out lstP1) &&
                        !epoches[i - 1][prn].SatData.TryGetValue("C1", out lstP1)) continue;

                    if (Math.Abs(curL1) < 1e-13
                     || Math.Abs(curL2) < 1e-13
                     || Math.Abs(curP1) < 1e-13
                     || Math.Abs(curP2) < 1e-13) continue;

                    dP = curP1 - lstP1;
                    dL = curL1 * Common.GPS_L1 - lstL1 * Common.GPS_L1;

                    dP4 = (curP1 - curP2) - (lstP1 - lstP2);
                    dL4 = (curL1 * Common.GPS_L1 - curL2 * Common.GPS_L2) - (lstL1 * Common.GPS_L1 - lstL2 * Common.GPS_L2);

                    // GF对周跳敏感，对钟跳不敏感，用来剔除周跳的情况
                    // 排除周跳，以免影响钟跳探测
                    if (dL4 > 0.15) continue;

                    validSatNum++;

                    // 毫秒级钟跳
                    if (Math.Abs(dP - dL) > k1)
                    {
                        jumpAllSatMs += dP - dL;

                        jumpSatNumMs++;
                    }

                    // 微妙级钟跳
                    else if (Math.Abs(dP - dL) > 0.000001 * Common.SPEED_OF_LIGHT &&
                        Math.Abs(dP - dL) < k1)
                    {
                        jumpAllSatUs += dP - dL;

                        jumpSatNumUs++;
                    }
                }

                if (validSatNum <= 0) continue;

                if (validSatNum != 0 && jumpSatNumMs == validSatNum)
                {
                    // 每颗卫星的钟跳(ms)
                    jumpPerSatDec = jumpAllSatMs / jumpSatNumMs / Common.C0 * 1000;
                    jumpPerSatInt = Math.Round(jumpPerSatDec);

                    if (Math.Abs(jumpPerSatDec - jumpPerSatInt) < k2)
                    {
                        epoches[i].ClockJump = true;
                        epoches[i].ClockJumpType = 2;
                        epoches[i].ClockJumpValue = (int)(jumpPerSatInt * 1e3);

                        Console.WriteLine("发生钟跳(毫秒级),历元:{0},大小:{1}", i, jumpPerSatInt);

                        // 单位转换成s
                        jumpPerSatInt *= 1e-3;
                    }
                }

                else if (validSatNum != 0 && jumpSatNumUs == validSatNum)
                {
                    epoches[i].ClockJump = true;
                    epoches[i].ClockJumpType = 2;

                    // 每颗卫星的钟跳(us)
                    jumpPerSatDec = jumpAllSatUs / jumpSatNumUs / Common.C0 * 1e6;
                    jumpPerSatInt = Math.Round(jumpPerSatDec);

                    Console.WriteLine("发生钟跳(微秒级),历元:{0},大小:{1}", i, jumpPerSatInt);

                    epoches[i].ClockJumpValue = (int)jumpPerSatInt;

                    // 转换成s
                    jumpPerSatInt *= 1e-6;
                }
                else
                {
                    continue;
                }

                // 修复当前弧段上所有卫星的观测值
                RepairClockJump(ref epoches, i, jumpPerSatInt, interval);
            }
        }

        /// <summary>
        /// 接收机钟跳修复
        /// Repair clock jump of a station
        /// </summary>
        /// <param name="epoches">观测历元 Observed epoches</param>
        /// <param name="startIndex">开始修复的历元索引 Index of first epoch to be repair</param>
        /// <param name="jumpSeconds">跳秒数(秒) Value to be repair(unit:s)</param>
        /// <param name="interval">采样频率(s) interval(unit:s)</param>
        private static void RepairClockJump(ref List<OEpoch> epoches, int startIndex, double jumpSeconds, int interval = 30)
        {
            if (epoches is null || epoches.Count < 2) return;
            if (startIndex < 1 || startIndex >= epoches.Count) return;
            if (epoches[startIndex] is null) return;
            if (epoches[startIndex].PRNList is null) return;

            //// 逐卫星修复
            foreach (var prn in epoches[startIndex].PRNList)
            {
                // 只修复GPS卫星
                if (!prn.StartsWith("G")) continue;

                for (int i = startIndex; i < epoches.Count; i++)
                {
                    // 该卫星当前弧段结束,后续的弧段不再修复
                    if (!epoches[i].PRNList.Contains(prn)) break;

                    if (epoches[i][prn].SatData.ContainsKey("L1"))
                    {
                        epoches[i][prn]["L1"] += jumpSeconds * Common.C0 / Common.GPS_L1;
                    }

                    if (epoches[i][prn].SatData.ContainsKey("L2"))
                    {
                        epoches[i][prn]["L2"] += jumpSeconds * Common.C0 / Common.GPS_L2;
                    }
                }
            }
        }

        /// <summary>
        /// 探测卫星的弧段
        /// </summary>
        /// <param name="epoches">观测历元</param>
        /// <param name="prn">要探测的卫星编号</param>
        /// <param name="minArcLen">探测出来的弧段最短的历元数</param>
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
                       && !epoches[i][prn].SatData.ContainsKey("C1")) flag = false;
                else if (epoches[i].Flag > 0) flag = false;

                // 发生周跳
                else if (epoches[i][prn].CycleSlip) flag = false;
                // 粗差
                else if (epoches[i][prn].Outlier) flag = false;

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

        public static void EliminateSatellites(ref List<OEpoch> epoches)
        {
            if (epoches is null || epoches.Count <= 0) return;
            foreach (var epoch in epoches)
            {
                for (int i = epoch.SatNum - 1; i > -1; i--)
                {
                    if (!epoch[i].SatPRN.StartsWith("G"))
                    {
                        epoch.AllSat.Remove(epoch[i].SatPRN);
                    }
                }
            }
        }
    }
}
