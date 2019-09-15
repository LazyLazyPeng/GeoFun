using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Smoother
    {
        /// <summary>
        /// 权重的最小值
        /// </summary>
        public static readonly double MIN_POWER = 0.01;

        /// <summary>
        /// 权重的减小的速率
        /// </summary>
        public static readonly double DEC_POWER = 0.01;

        public static void Smooth()
        {

        }

        /// <summary>
        /// 用另一个观测序列来平滑本观测序列
        /// </summary>
        public static void SmoothByAnother()
        {

        }

        /// <summary>
        /// 用L4观测值平滑P4观测值
        /// </summary>
        /// <param name="epoches"></param>
        /// <param name="prn"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void SmoothP4ByL4(ref List<OEpoch> epoches, string prn,int start = 0,int end = -1)
        {
            if (epoches is null) return;
            if (prn is null) return;
            if (start >= epoches.Count) return;
            if (start < 0) return;
            if (end < start) return;
            if (end >= epoches.Count) end = epoches.Count;

            double power = 1;
            // P4预报值
            double p4_est = 0d;
            for (int i = start+1;i<end; i++)
            {
                power = power - DEC_POWER;
                p4_est = epoches[i - 1][prn]["P4"] + epoches[i][prn]["L4"] - epoches[i - 1][prn]["L4"];

                epoches[i][prn]["P4"] =epoches[i][prn]["P4"] * power + (1 - power) * p4_est;
            }

        }

        /// <summary>
        /// 平滑一个弧段
        /// </summary>
        /// <param name="arc"></param>
        public static void SmoothP4ByL4(ref OArc arc)
        {
            double power = 1;

            // P4预报值
            double p4_est = 0d;
            for (int i = 1;i<arc.Length; i++)
            {
                power = power - DEC_POWER;
                p4_est = arc[i - 1]["P4"] + arc[i]["L4"] - arc[i - 1]["L4"];

                arc[i]["P4"] = arc[i]["P4"] * power + (1 - power) * p4_est;
            }
        }
    }
}
