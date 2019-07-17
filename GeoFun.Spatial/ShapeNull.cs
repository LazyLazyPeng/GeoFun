using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class ShapeNull:IFeature
    {
        public bool HasM { get; set; } = false;

        public int Data = 0;

        /// <summary>
        /// Size in bytes
        /// </summary>
        public int Size
        {
            get
            {
                return 4;
            }
        }

        public int NumPoints { get; set; } = 0;

        public Point GetPointAt(int index)
        {
            throw new IndexOutOfRangeException();
        }

        public void GetPointAt(int index, out double x, out double y)
        {
            throw new IndexOutOfRangeException();
        }

        public void SetPointAt(int index, Point pt)
        {
            throw new IndexOutOfRangeException();
        }

        public void SetPointAt(int index, double x, double y)
        {
            throw new IndexOutOfRangeException();
        }
    }
}
