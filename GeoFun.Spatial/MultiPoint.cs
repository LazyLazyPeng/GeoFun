using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class MultiPoint : IFeature
    {
        public bool HasM { get; set; } = false;

        public Box BoundingBox = new Box();

        /// <summary>
        /// TotalNumber of Points
        /// </summary>
        public int NumPoints { get; set; } = 0;

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


        public Point GetPointAt(int index)
        {
            if (index < 0 || index >= NumPoints) throw new IndexOutOfRangeException("点索引超出范围");

            return Points[index];
        }

        public void GetPointAt(int index, out double x, out double y)
        {
            if (index < 0 || index >= NumPoints) throw new IndexOutOfRangeException("点索引超出范围");

            x = Points[index].X;
            y = Points[index].Y;
        }

        public void SetPointAt(int index, Point pt)
        {
            if (index < 0 || index >= NumPoints) throw new IndexOutOfRangeException("点索引超出范围");

            Points[index] = pt;
        }

        public void SetPointAt(int index, double x, double y)
        {
            if (index < 0 || index >= NumPoints) throw new IndexOutOfRangeException("点索引超出范围");

            Points[index].X = x;
            Points[index].Y = y;
        }
    }
}
