using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class SHPFileHeader
    {
        /// <summary>
        /// File Code
        /// </summary>
        public int FileCode { get; set; } = 0;

        public int Unuse1 { get; set; } = 0;
        public int Unuse2 { get; set; } = 0;
        public int Unuse3 { get; set; } = 0;
        public int Unuse4 { get; set; } = 0;
        public int Unuse5 { get; set; } = 0;

        public int FileLength { get; set; } = 0;

        public int Version { get; set; } = 1000;

        /// <summary>
        /// 要素类型
        /// </summary>
        public enumShapeType ShapeType { get; set; } = 0;

        public double Xmax { get; set; } = 0d;
        public double Xmin { get; set; } = 0d;
        public double Ymax { get; set; } = 0d;
        public double Ymin { get; set; } = 0d;
        public double Zmax { get; set; } = 0d;
        public double Zmin { get; set; } = 0d;
        public double Mmax { get; set; } = 0d;
        public double Mmin { get; set; } = 0d;
    }
}
