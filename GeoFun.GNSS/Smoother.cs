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
        /// 用L4平滑P4
        /// </summary>
        public static void SmoothP4ByL4(ref List<OEpoch> epoches, string prn)
        {

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
