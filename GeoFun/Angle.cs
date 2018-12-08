﻿using System;

namespace GeoFun
{
    public class Angle
    {
        public static double PI = 3.1415926535897932;
        public static decimal PIM = 3.1415926535897932m;

        /// <summary>
        /// 弧度转度
        /// </summary>
        public static double R2D = 180d / PI;
        public static decimal R2DM = 180m / PIM;

        /// <summary>
        /// 度转弧度
        /// </summary>
        public static double D2R = PI / 180d;
        public static decimal D2RM = PIM / 180m;

        /// <summary>
        /// 秒转弧度
        /// </summary>
        public static double S2R = PI / 180d / 3600d;

        /// <summary>
        /// 将度转换为度分秒格式的数
        /// </summary>
        /// <param name="dd">例如20.5</param>
        /// <returns>例如20.30</returns>
        public static double DD2DMS(double dd)
        {
            return (double)DD2DMS((decimal)dd);
        }
        public static decimal DD2DMS(decimal dd)
        {
            if (dd == 0)
            {
                return 0;
            }

            // 取绝对值
            decimal ddd = Math.Abs(dd);

            // 计算出度分秒
            decimal d = Math.Floor(ddd);
            decimal m = Math.Floor(ddd * 60m - d * 60m);

            decimal s = ddd * 3600m - d * 3600m - m * 60m;

            // 重新组合成”度d分分秒秒ddd”格式
            decimal dms = d + m / 100m + s / 10000m;

            // 注意符号
            if (dd < 0)
            {
                dms = -dms;
            }

            return dms;

        }

        /// <summary>
        /// 将度分秒表示的角度转换为度
        /// </summary>
        /// <param name="dms">例如20.30</param>
        /// <returns>例如20.5</returns>
        public static double DMS2DD(double dms)
        {
            return (double)DMS2DD((decimal)dms);
        }
        public static decimal DMS2DD(decimal dms)
        {
            try
            {
                if (dms == 0)
                {
                    return dms;
                }

                // 取绝对值
                decimal ddms = (decimal)Math.Abs(dms);

                // 计算出度分秒
                decimal d = (decimal)Math.Floor(ddms);
                decimal m = (decimal)Math.Floor(ddms * 100m - d * 100m);
                decimal s = ddms * 10000m - d * 10000m - m * 100m;

                while (s >= 60m)
                {
                    m += 1.0m;
                    s -= 60m;
                }

                while (m >= 60m)
                {
                    d += 1m;
                    m -= 60m;
                }

                while (d >= 360m)
                {
                    d -= 360m;
                }

                // 组合成度
                decimal dd = d + m / 60m + s / 3600m;

                // 注意符号
                if (dms < 0)
                {
                    dd = -dd;
                }

                return dd;
            }
            catch (Exception ex)
            {
                return dms;
            }

        }

        public static double Arc2DD(double arc)
        {
            return arc * 180d / PI;
        }
        public static decimal Arc2DD(decimal arc)
        {
            return arc * 180m / PIM;
        }

        public static double DD2Arc(double dd)
        {
            return dd * D2R;
        }
        public static decimal DD2Arc(decimal dd)
        {
            return dd * D2RM;
        }

        public static double Arc2DMS(double arc)
        {
            return DD2DMS(arc * R2D);
        }

        public static double DMS2Arc(double dms)
        {
            return DMS2DD(dms) * D2R;
        }
    }
}