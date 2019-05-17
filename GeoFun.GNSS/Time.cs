using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public static class Time
    {
        public static readonly int SecondsPerDay = 24 * 3600;
        public static readonly int SecondsPerWeek = 7 * 3600;

        /// <summary>
        /// Time 类的静态方法，通用时到儒略日
        /// </summary>
        /// <param name="pct">通用时</param>
        /// <returns>通用时一秒之内的小数部分，单位：皮秒</returns>
        public static Day  CommonToMJD(CommonT dt)
        {
            int y, m;
            int temp;

            Day mjd = new Day();

            y = dt.Year;

            if (dt.Year < 1900)
            {
                if (dt.Year < 80) y = y + 2000;
                else y = y + 1900;
            }

            if (dt.Month <= 2)
            {
                y = y - 1;
                m = dt.Month + 12;
            }
            else
            {
                m = dt.Month;
            }

            temp = (int)(365.25 * y);
            temp += (int)(30.6001 * (m + 1));
            temp += dt.Day;
            temp += -679019;

            mjd.Days = (int)temp;

            mjd.Seconds = dt.Hour * 3600 + dt.Minute * 60 + (double)dt.Second;

            return mjd;
        }

        /// <summary>
        /// 新儒略日转通用时
        /// </summary>
        /// <param name="days"></param>
        /// <param name="sod"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        /// <param name="pico"></param>
        public static void MJDToCommon(int days, double sod, out int year, out int month, out int day, out int hour, out int minute, out decimal second)
        {
            int a, b, c, d, e;
            double seconds = 0d;
            double secondsDouble = 0d;

            a = (int)(days + sod / 86400 + 2400000.5 + 0.5);
            b = a + 1537;
            c = (int)((b - 122.1) / 365.25);
            d = (int)(365.25 * c);
            e = (int)((b - d) / 30.6001);

            day = b - d - (int)(30.6001 * e);
            month = e - 1 - 12 * (int)(e / 14);
            year = c - 4715 - (int)((7 + month) / 10);

            hour = (int)(sod / 3600);
            minute = (int)((sod - hour * 3600) / 60);
            seconds = sod - hour * 3600d - minute * 60d;
            second = (decimal)seconds;
        }
        /// <summary>
        /// 新儒略日转通用时
        /// </summary>
        /// <param name="mjd"></param>
        /// <param name="dt"></param>
        /// <param name="pico"></param>
        public static void MJDToCommon(Day mjd, out CommonT dt)
        {
            int year, month, day, hour, minute;
            decimal second;
            MJDToCommon(mjd.Days, mjd.Seconds, out year, out month, out day, out hour, out minute, out second);
            dt = new CommonT(year, month, day, hour, minute, second);
        }

        public static void MJDToGPS(int days, double sod, out int weeks, out double sow)
        {
            int RemainDay;
            weeks = (int)((days - 44244) / 7);

            RemainDay = (int)(days - weeks * 7 - 44244);

            sow = RemainDay * 86400 + sod;
        }
        /// <summary>
        /// Time 类的静态方法，新儒略日到GPS时
        /// </summary>
        /// <param name="mjd">新儒略日</param>
        /// <returns>GPS时间结构</returns>
        public static void MJDToGPS(Day mjd, out int weeks, out double sow)
        {
            MJDToGPS(mjd.Days, mjd.Seconds, out weeks, out sow);
        }
        public static Week MJDToGPS(Day mjd)
        {
            int weeks;
            double sow;

            MJDToGPS(mjd,out weeks, out sow);

            return new Week(weeks, sow);
        }

        /// <summary>
        /// 通用时转GPS时
        /// </summary>
        /// <param name="week"></param>
        /// <param name="sow"></param>
        /// <param name="dt"></param>
        /// <param name="picoSeconds"></param>
        public static void CommonToGPS(out int week, out double sow, CommonT dt)
        {
            Day mjd = CommonToMJD(dt);
            MJDToGPS(mjd, out week, out sow);
        }
        public static Week CommonToGPS(CommonT dt)
        {
            int week;
            double sow;

            CommonToGPS(out week, out sow, dt);

            return new Week(week,sow);
        }

        /// <summary>
        /// GPS时转儒略日
        /// </summary>
        /// <param name="weeks"></param>
        /// <param name="sow"></param>
        /// <param name="days"></param>
        /// <param name="sod"></param>
        public static void GPSToMJD(int weeks,double sow,out int days, out double sod)
        {
            int day;
            sod = sow;

            day = (int)(sow / 86400);
            sod = sow - day * 86400;
            days = weeks * 7 + 44244 + day;
        }
        public static Day GPSToMJD(Week week)
        {
            int days;
            double sod;
            GPSToMJD(week.Weeks, week.Seconds, out days, out sod);

            return new Day(days, sod);
        }

        /// <summary>
        /// GPS时转通用时
        /// </summary>
        /// <param name="weeks"></param>
        /// <param name="sow"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="second"></param>
        public static void GPSToCommon(int weeks,double sow, out int year,out int month, out int day, out int hour, out int minute,out decimal second)
        {
            int days;
            double sod;

            GPSToMJD(weeks,sow,out days,out sod);
            MJDToCommon(days, sod, out year, out month, out day, out hour, out minute, out second);
        }
        public static void GPSToCommon(Week gpsWeek,out CommonT dt)
        {
            int year, month, day, hour, minute;
            decimal second;

            GPSToCommon(gpsWeek.Weeks, gpsWeek.Seconds, out year, out month, out day, out hour, out minute, out second);

            dt = new CommonT(year, month, day, hour, minute, second);
        }
    }
}
