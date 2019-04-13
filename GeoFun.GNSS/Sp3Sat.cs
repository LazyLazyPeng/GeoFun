using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    ///一颗卫星的精密星历
    public class SP3Sat
    {
        public string Prn;

        /// <summary>
        /// P位置 V速度
        /// </summary>
        public char Type { get; set; } = 'P';

        public double X { get; set; } = 0d;
        public double Y { get; set; } = 0d;
        public double Z { get; set; } = 0d;

        public double XBias { get; set; } = 0d;
        public double YBias { get; set; } = 0d;
        public double ZBias { get; set; } = 0d;

        ////卫星钟(Clock)
        public double C;
        /// <summary>
        /// 钟漂(Clock Bias)
        /// </summary>
        public double CBias;
    }
}