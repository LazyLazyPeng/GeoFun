using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class SP3Epoch
    {
        /// <summary>
        /// 历元时刻
        /// </summary>
        public GPST Epoch;

        /// <summary>
        /// 所有卫星的数值
        /// </summary>
        public Dictionary<string, SP3Sat> AllSat { get; set; } = new Dictionary<string, SP3Sat>();
    }
}