using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class Polygon:IFeature
    {
        public bool HasM { get; set; } = false;

        public Box BoundingBox = new Box();

        /// <summary>
        /// Number of Parts
        /// </summary>
        public int NumParts = 0;

        /// <summary>
        /// TotalNumber of Points
        /// </summary>
        public int NumPoints = 0;

        /// <summary>
        /// Index to First Point in Part
        /// </summary>
        public List<int> Parts = new List<int>();

        /// <summary>
        /// Points of All Parts
        /// </summary>
        public Point[] Points;

        /// <summary>
        /// Bounding Z Range
        /// </summary>
        public double[] ZRange = new double[2];

        /// <summary>
        /// Bounding Z Range
        /// </summary>
        public double[] MRange = new double[2];
    }
}
