using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class Point:IFeature
    {
        public bool HasZ { get; set; } = false;
        public bool HasM { get; set; } = false;

        public double X;
        public double Y;
        public double M;
        public double Z;
    }
}
