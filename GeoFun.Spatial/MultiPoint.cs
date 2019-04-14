using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class MultiPoint:IFeature
    {
        public bool HasM { get; set; } = false;

        public Box BoundingBox = new Box();

        /// <summary>
        /// TotalNumber of Points
        /// </summary>
        public int NumPoints = 0;

        /// <summary>
        /// All Points
        /// </summary>
        public Point[] Points;

        /// <summary>
        /// Bounding Z Range
        /// </summary>
        public double[] ZRange = new double[2];

        public double[] ZArray;

        /// <summary>
        /// Bounding Z Range
        /// </summary>
        public double[] MRange = new double[2];

        public double[] MArray;
    }
}
