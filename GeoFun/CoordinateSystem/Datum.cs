using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class Datum
    {
        public string Name { get; set; }
        public Ellipsoid Ellipsoid { get; set; }
    }
}
