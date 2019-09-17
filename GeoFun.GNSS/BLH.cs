using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class BLH
    {
        /// <summary>
        /// 纬度(弧度)
        /// </summary>
        /// <value></value>
        public double B{get;set;} = 0d;

        /// <summary>
        /// 经度(弧度)
        /// </summary>
        /// <value></value>
        public double L{get;set;} = 0d;

        /// <summary>
        /// 大地高(米)
        /// </summary>
        /// <value></value>
        public double H{get;set;} = 0d;
    }
}