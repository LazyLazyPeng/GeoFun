using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class ProjectionCoorSystem
    {
        /// <summary>
        /// 中央子午线(弧度)
        /// </summary>
        public double L0 { get; set; }

        /// <summary>
        /// 投影抬高(米)
        /// </summary>
        public double H0 { get; set; }

        /// <summary>
        /// 地理坐标系
        /// </summary>
        public GeographicCoorSystem GeographicCoorSystem { get; set; }
    }
}
