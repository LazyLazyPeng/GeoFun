using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public interface IFMEEllipsoid
    {
        /// <summary>
        /// 名称
        /// </summary>
        string FMEName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        string Source { get; set; }
        /// <summary>
        /// 长半轴(米)
        /// </summary>
        double A { get; set; }
        /// <summary>
        /// 短半轴(米)
        /// </summary>
        double B { get; set; }
    }
}
