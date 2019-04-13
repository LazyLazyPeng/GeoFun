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
        /// 中央子午线(弧度)
        /// </summary>
        double L0 { get; set; }
        /// <summary>
        /// 投影抬高(米)
        /// </summary>
        double H0 { get; set; }

        double XOff { get; set; }
        double YOff { get; set; }
    }

    public class ProjectionSystem : IProjectionSystem
    {
        public string Name { get; set; }
        public bool IsArcGIS { get; set; }
        public IGeographicSystem GeoCS { get; set; } = new GeographicSystem();

        /// <summary>
        /// 中央子午线(弧度)
        /// </summary>
        public double L0 { get; set; }
        public double H0 { get; set; } = 0d;

        public double XOff { get; set; }
        public double YOff { get; set; }

        public Ellipsoid Ellipsoid
        {
            get
            {
                if (GeoCS != null)
                {
                    return GeoCS.Ellipsoid;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (GeoCS != null)
                {
                    GeoCS.Ellipsoid = value;
                }
            }
        }

        public enumCSType CSType { get; } = enumCSType.Projection;
    }
}
