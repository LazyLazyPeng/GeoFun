using System;

namespace GeoFun
{
    public class Angle
    {
        public static double PI = 3.1415926535897932;
        public static decimal PIM = 3.1415926535897932m;

        private decimal ddd = 0m;
        private decimal dms = 0m;
        private decimal arc = 0m;

        public decimal DD
        {
            get
            {
                return ddd;
            }
            set
            {
                ddd = value;
                dms = DD2DMS(ddd);
                arc = ddd * Angle.D2RM;
            }
        }
        public decimal DMS
        {
            get
            {
                return dms;
            }
            set
            {
                dms = value;
                ddd = DMS2DD(dms);
                arc = ddd * Angle.R2DM;
            }
        }
        public decimal ARC
        {
            get
            {
                return arc;
            }
            set
            {
                arc = value;
                ddd = arc * Angle.R2DM;
                dms = DD2DMS(ddd);
            }
        }

        public int D
        {
            get
            {
                int d = 0;
                decimal ddms = dms < 0m ? -dms : dms;

                if (dms < 0) d = (int)Math.Floor(ddms);
                else
                {
                    d = (int)Math.Floor(ddms);
                }

                if (dms < 0m) d = -d;

                return d;
            }
        }

        public int M
        {
            get
            {
                int m = 0;
                decimal ddms = dms < 0m ? -dms : dms;

                if (dms < 0) m = (int)Math.Floor(-dms * 100 - D * 100);
                else
                {
                    m = (int)Math.Floor(dms * 100 - D * 100);
                }

                if (dms < 0) m = -m;

                return m;
            }
        }

        public decimal S
        {
            get
            {
                return dms*1e4m - D*1e4m - M*100m;
            }
        }

        public decimal AllSeconds
        {
            get
            {
                return DD * 3600m;
            }
            set
            {
                DD = value / 3600m;
            }
        }

        public decimal AllMinutes
        {
            get
            {
                return DD * 60m;
            }
            set
            {
                DD = value / 60m;
            }
        }

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

        public Angle(decimal ang = 0m, enumAngleFormat format = enumAngleFormat.DD)
        {
            switch (format)
            {
                case enumAngleFormat.DMS:
                    DMS = ang;
                    break;
                case enumAngleFormat.Arc:
                    ARC = ang;
                    break;
                default:
                    DD = ang;
                    break;
            }
        }
        public Angle(double angle, enumAngleFormat format = enumAngleFormat.DD)
        {
            decimal ang = (decimal)angle;
            switch (format)
            {
                case enumAngleFormat.DMS:
                    DMS = ang;
                    break;
                case enumAngleFormat.Arc:
                    ARC = ang;
                    break;
                default:
                    DD = ang;
                    break;
            }
        }


        public static double gd(double g)
        {
            int g1, g2, i;
            i = Math.Sign(g);
            g = Math.Abs(g);
            g1 = (int)Math.Floor(g + 1e-12);
            g2 = (int)Math.Floor((g - g1) * 100.0 + 1e-12);
            g = (g - g1 - g2 / 100.0) * 10000.0 / 3600.0;
            g += g1 + g2 / 60.0;
            g *= PI / 180.0;
            return (i * g);
        }

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