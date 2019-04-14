using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    public static class enumShapeCode
    {
        public static byte[] NullShape = BitConverter.GetBytes(1);
        public static byte[] Point = BitConverter.GetBytes(1);
        public static byte[] Polyline = BitConverter.GetBytes(3);
        public static byte[] Polygon = BitConverter.GetBytes(5);
        public static byte[] MultiPoint = BitConverter.GetBytes(8);

        public static byte[] PointZ = BitConverter.GetBytes(11);
        public static byte[] PolylineZ = BitConverter.GetBytes(13);
        public static byte[] PolygonZ = BitConverter.GetBytes(15);
        public static byte[] MultiPointZ = BitConverter.GetBytes(18);

        public static byte[] PointM = BitConverter.GetBytes(21);
        public static byte[] PolylineM = BitConverter.GetBytes(23);
        public static byte[] PolygonM = BitConverter.GetBytes(25);
        public static byte[] MultiPointM = BitConverter.GetBytes(28);

        public static byte[] MultiPatch = BitConverter.GetBytes(31);
    }
}
