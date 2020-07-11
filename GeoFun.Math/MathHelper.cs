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

namespace GeoFun.MathUtils
{
    public class MathHelper
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

        public static double Abs(double d)
        {
            return System.Math.Abs(d);
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
        /// 根据接收机和卫星位置计算高度角/方位角
        /// </summary>
        /// <param name="recPos">receiver position 测站坐标(m), x y z</param>
        /// <param name="satPos">satellite position 卫星坐标(m), x y z</param>
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

            // 接收机经纬度
            double lrec = 0d, brec = 0d, hrec = 0d;
            Coordinate.XYZ2BLH(recPos[0], recPos[1], recPos[2], out brec, out lrec, out hrec, Ellipsoid.ELLIP_WGS84);

            // 站心坐标系坐标
            double[] rec = Coordinate.ECEF2ENU(brec, lrec, recPos, satPos);
            double x = rec[0];
            double y = rec[1];
            double z = rec[2];

            if (x * x + y * y + z * z < 1e-12)
            {
                az = 0;
                el = 0d;
            }
            else
            {
                az = Atan(y / x);
                el = Atan(z / (x * Cos(az) + y * Sin(az)));
            }

            // 方位角位于0-2π的周期内
            if (az < 0) az += 2 * PI;
        }

        /// <summary>
        /// 根据接收机和卫星位置计算高度角/方位角
        /// </summary>
        /// <param name="b">receiver latitude 接收机纬度(rad)</param>
        /// <param name="l">receiver longitude 接收机经度(rad)</param>
        /// <param name="recPos">receiver position 测站坐标(m), x y z</param>
        /// <param name="satPos">satellite position 卫星坐标(m), x y z</param>
        /// <param name="az">azimuth 方位角(rad)</param>
        /// <param name="el">elevation 高度角(rad)</param>
        public static void CalAzEl(double b, double l, double[] recPos, double[] satPos, out double az, out double el)
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

            // 站心坐标系坐标
            double[] rec = Coordinate.ECEF2ENU(b, l, recPos, satPos);
            double x = rec[0];
            double y = rec[1];
            double z = rec[2];

            if (x * x + y * y + z * z < 1e-12)
            {
                az = 0;
                el = 0d;
            }
            else
            {
                az = Atan(y / x);
                el = Atan(z / (x * Cos(az) + y * Sin(az)));
            }

            // 方位角位于0-2π的周期内
            if (az < 0) az += 2 * PI;
        }

        /// <summary>
        /// 计算穿刺点坐标
        /// </summary>
        /// <param name="b">receiver latitude 测站纬度(rad)</param>
        /// <param name="l">receiver longitude 测站经度(rad)</param>
        /// <param name="r0">distance to earth center of receiver 测站地心距(m)</param>
        /// <param name="re">major axis 地球半径(m)</param>
        /// <param name="h0">height of single layer model of ionosphere电离层单层模型高度(m)</param>
        /// <param name="az">azimuth 方位角(rad)</param>
        /// <param name="el">elevation 高度角(rad)</param>
        /// <param name="bb">latitude of ipp 穿刺点纬度(rad)</param>
        /// <param name="ll">longitude of ipp 穿刺点经度(rad)</param>
        public static void CalIPP(double b, double l, double r0, double re, double h0, double az, double el, out double bb, out double ll)
        {
            //穿刺点与测站在地心的张角
            double phi = PI / 2d - el - Asin(re / (re + h0) * Cos(el));

            //穿刺点位置
            bb = Asin(Sin(b) * Cos(phi) + Cos(b) * Sin(phi) * Cos(az));
            ll = l + Asin(Sin(phi) * Sin(az) / Cos(phi));
        }
    }
}
