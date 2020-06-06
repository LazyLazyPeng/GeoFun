using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    /// <summary>
    /// 大地坐标系坐标
    /// </summary>
    public struct BLH
    {
        /// <summary>
        /// 纬度(弧度)
        /// </summary>
        public double B;
        /// <summary>
        /// 经度(弧度)
        /// </summary>
        public double L;
        /// <summary>
        /// 大地高(米)
        /// </summary>
        public double H;
    }
}
