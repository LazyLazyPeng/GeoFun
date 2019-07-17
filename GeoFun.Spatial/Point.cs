using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class Point : IFeature
    {
        public bool HasZ { get; set; } = false;
        public bool HasM { get; set; } = false;
        public int NumPoints { get; set; } = 1;

        public double X = 0d;
        public double Y = 0d;
        public double M = 0d;
        public double Z = 0d;

        public Point GetPointAt(int index)
        {
            if (index != 0) throw new IndexOutOfRangeException("点索引超出范围");

            return this;
        }

        public void GetPointAt(int index, out double x, out double y)
        {
            if (index != 0) throw new IndexOutOfRangeException("点索引超出范围");

            x = X;
            y = Y;
        }

        public void SetPointAt(int index, Point pt)
        {
            if (index != 0) throw new IndexOutOfRangeException("点索引超出范围");

            HasZ = pt.HasZ;
            HasM = pt.HasM;
            X = pt.X;
            Y = pt.Y;
            Z = pt.Z;
            M = pt.M;
        }

        public void SetPointAt(int index, double x, double y)
        {
            if (index != 0) throw new IndexOutOfRangeException("点索引超出范围");

            X = x;
            Y = y;
        }
    }
}
