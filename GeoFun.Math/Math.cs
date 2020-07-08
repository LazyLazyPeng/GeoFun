using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using GeoFun;
using System.Xml.Schema;
using System.Runtime.ExceptionServices;
using System.Net.Cache;

namespace GeoFun.Math
{
    public class Math
    {
        public static readonly double PI = 3.14159265358979323;

        public static double Sqrt(double num)
        {
            return System.Math.Sqrt(num);
        }

        public static double Pow(double x, double y)
        {
            return System.Math.Pow(x, y);
        }

        /// <summary>
        /// 正弦函数
        /// </summary>
        /// <param name="angle">弧度</param>
        /// <returns></returns>
        public static double Sin(double angle)
        {
            return System.Math.Sin(angle);
        }

        public static double Cos(double angle)
        {
            return System.Math.Cos(angle);
        }

        public static double Asin(double d)
        {
            return System.Math.Asin(d);
        }

        public static double Acos(double d)
        {
            return System.Math.Acos(d);
        }

        public static double Atan(double d)
        {
            return System.Math.Atan(d);
        }

        public static double atan2(double x, double y)
        {
            return System.Math.Atan2(x, y);
        }

        // 计算向量点乘
        public static double Dot(double[] a, double[] b)
        {
            if (a is null || b is null)
            {
                throw new ArgumentException("向量不能为空...");
            }
            if (a.Length != b.Length)
            {
                throw new ArgumentException("向量长度不一致，无法进行dot运算");
            }

            return a.Zip(b, (x, y) => x * y).Sum();
        }

        /// <summary>
        ///  ecef to local coordinate transfromation matrix
        /// </summary>
        /// <param name="pos">geodetic position {lat,lon} (rad)</param>
        /// <returns>ecef to local coord transformation matrix (3x3)</returns>
        public static double[] xyz2enu(double[] pos)
        {
            double[] E = new double[9];

            double sinp = System.Math.Sin(pos[0]);
            double cosp = System.Math.Cos(pos[0]);
            double sinl = System.Math.Sin(pos[1]);
            double cosl = System.Math.Cos(pos[1]);

            E[0] = -sinl; E[3] = cosl; E[6] = 0.0;
            E[1] = -sinp * cosl; E[4] = -sinp * sinl; E[7] = cosp;
            E[2] = cosp * cosl; E[5] = cosp * sinl; E[8] = sinp;

            return E;
        }

        /// <summary>
        /// 根据接收机和卫星位置计算高度角，方位角
        /// </summary>
        /// <param name="recPos">receiver position, x y z</param>
        /// <param name="satPos">satellite position, x y z</param>
        /// <param name="az">azimuth 方位角(弧度)</param>
        /// <param name="el">elevation 高度角(弧度)</param>
        public static void CalAzEl(double[] recPos, double[] satPos, out double az, out double el)
        {
            if (recPos is null)
            {
                throw new ArgumentNullException("recPos");
            }

            if (satPos is null)
            {
                throw new ArgumentNullException("satPos");
            }

            if (recPos.Length != satPos.Length)
            {
                throw new ArgumentException("satPos与recPos的维数不一致");
            }

            if (recPos.Length != 3)
            {
                throw new ArgumentException("satPos与recPos的维数不为3");
            }

            // 测站距地心距离
            double R = Sqrt(Dot(recPos,recPos));

            // 卫星经纬度
            double lsat = 0d, bsat = 0d, hsat = 0d;
            Coordinate.XYZ2BLH(satPos[0], satPos[1], satPos[2], out bsat, out lsat, out hsat, Ellipsoid.ELLIP_WGS84);
            double lrec = 0d, brec = 0d, hrec = 0d;
            Coordinate.XYZ2BLH(recPos[0], recPos[1], recPos[2], out brec, out lrec, out hrec, Ellipsoid.ELLIP_WGS84);

            // 站心坐标系坐标
            double x = Cos(brec) * Cos(lrec) * satPos[0] + Cos(brec) * Sin(lrec) * satPos[1] + Sin(brec)*satPos[2] - R;
            double y = -Sin(lrec) * satPos[0] + Cos(lrec) * satPos[1];
            double z = -Sin(brec) * Cos(lrec) * satPos[0] - Sin(brec) * Sin(lrec) * satPos[1] + Cos(brec) * satPos[2];

            double sum = Sqrt(x * x + y * y + z * z);
            x = x / sum;
            y = y / sum;
            z = z / sum;

            if (x * x + y * y + z * z < 1e-12)
            {
                az = 0;
                el = 0d;
            }
            else
            {
                az = Atan(y / z);
                el = Asin(x / Sqrt(x * x + y * y + z * z));
            }

            if (az < 0) az += 2 * PI;
        }
    }
}
