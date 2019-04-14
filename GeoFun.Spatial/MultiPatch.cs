using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class MultiPatch:IFeature
    {
        public bool HasM { get; set; } = false;

        public int Size
        {
            get
            {
                int W = 44 + 4 * NumParts;
                int X = W + 4 * NumParts;
                int Y = X + 16 * NumPoints;
                int Z = Y + 16 + 8 * NumPoints;
                if(HasM)
                {
                    return Z + 16 + 8 * NumPoints+8;
                }
                else
                {
                    return Z+8;
                }
            }
        }

        public Box BoundingBox = new Box();

        /// <summary>
        /// Number of Parts
        /// </summary>
        public int NumParts = 0;

        /// <summary>
        /// Part Type
        /// </summary>
        public List<enumPartType> PartTypes = new List<enumPartType>();

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

        public double[] ZArray;

        /// <summary>
        /// Bounding Z Range
        /// </summary>
        public double[] MRange = new double[2];

        public double[] MArray;
    }
}
