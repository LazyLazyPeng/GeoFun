using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public interface IProjectionSystem : ICoordinateSystem
    {
        IGeographicSystem GeoCS { get; set; }

        /// <summary>
        /// 中央子午线
        /// </summary>
        Angle CenterMeridian { get; set; }

        /// <summary>
        /// 中央子午线(弧度)
        /// </summary>
        double L0 { get; set; }
        /// <summary>
        /// 投影抬高(米)
        /// </summary>
        double H0 { get; set; }

        enumBandType BandType { get; set; }
        int BandNum { get; set; }

        double XOff { get; set; }
        double YOff { get; set; }

        /// <summary>
        /// 底点纬度
        /// </summary>
        Angle OriginLat { get; set; }
    }
}
