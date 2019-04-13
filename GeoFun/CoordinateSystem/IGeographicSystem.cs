using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;

namespace GeoFun.CoordinateSystem
{
    public interface IGeographicSystem : ICoordinateSystem
    {
    }

    public class GeographicSystem : IGeographicSystem
    {
        public Ellipsoid Ellipsoid { get; set; } = Ellipsoid.ELLIP_CGCS2000;
        public enumCSType CSType { get; } = enumCSType.Geographic;
        public string Name { get; set; }
        public bool IsArcGIS { get; set; } = false;
    }
}
