using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;

namespace GeoFun.CoordinateSystem
{
    public interface ICoordinateSystem
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 坐标系类型
        /// </summary>
        enumCSType CSType { get; }

        /// <summary>
        /// 椭球
        /// </summary>
        Ellipsoid Ellipsoid { get; set; }

        /// <summary>
        /// 是否是ArcGIS内置的坐标系
        /// </summary>
        bool IsArcGIS { get; set; }
    }


}
