using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class OEpoch
    {
        /// <summary>
        /// 历元
        /// </summary>
        public GPST Epoch { get; set; }

        /// <summary>
        /// 历元标识
        /// </summary>
        /// <remarks>
        /// 0:OK
        /// 1:power failure between previous and current epoch
        /// >1:event flag
        /// 2:start moving antenna
        /// 3:new site occupation
        /// 4:header information follows
        /// 5:external event(epoch is significant,same time frame as observation time tags)
        /// 6:cycle slip records follow to optionally report detected and repaired cycle slip
        /// </remarks>
        public int Flag { get; set; } = 0;

        /// <summary>
        /// 卫星数
        /// </summary>
        public int SatNum
        {
            get
            {
                return AllSat.Count;
            }
        }

        /// <summary>
        /// 本历元所有的卫星编号
        /// </summary>
        public List<string> PRNList = new List<string>();

        /// <summary>
        /// 一个历元的所有观测值
        /// </summary>
        public Dictionary<string, OSat> AllSat = new Dictionary<string, OSat>();

        /// <summary>
        /// 接收机钟差
        /// </summary>
        public double ClockBias { get; set; } = 0d;

        public OSat this[string prn]
        {
            get
            {
                return AllSat[prn];
            }
            set
            {
                AllSat[prn] = value;
            }
        }

    }
}