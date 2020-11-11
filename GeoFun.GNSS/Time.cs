using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public static class Time
    {
        public static readonly int SecondsPerDay = 24 * 3600;
        public static readonly int SecondsPerWeek = 7 * 24 * 3600;

        public static readonly int[] DaysPerMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static readonly int[] DaysPerMonthLeapYear = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static int DayNumOfYear(int year)
        {
            if (DateTime.IsLeapYear(year)) return 366;
            else return 365;
        }

        /// <summary>
        /// Check whether the values of year/month/day are in right ranges 
        /// 检查年月日的格式是否正确
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void CheckYearMonthDay(int year, int month, int day)
        {
            if (month > 12 || month < 1)
            {
                throw new ArgumentException("The value of month exceed range 1-12, the value is " + month.ToString());
            }

            int[] daysPerMonth = DaysPerMonth;
            if (DateTime.IsLeapYear(year)) daysPerMonth = DaysPerMonthLeapYear;
            if (day < 0 || day > daysPerMonth[month - 1])
            {
                throw new ArgumentException("The day number of month exceed maxinum day number of month");
            }
        }

        /// <summary>
        /// Time 类的静态方法，通用时到儒略日
        /// </summary>
        /// <param name="pct">通用时</param>
        /// <returns>通用时一秒之内的小数部分，单位：皮秒</returns>
        public static Day Common2MJD(CommonT dt)
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

            mjd.Days = temp;

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
        public static void MJD2Common(int days, double sod, out int year, out int month, out int day, out int hour, out int minute, out decimal second)
        {
            int a, b, c, d, e;
            double seconds = 0d;

            double jd = days + sod / 86400 + 2400000.5;
            a = (int)(jd + 0.5);
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
        public static CommonT MJD2Common(Day mjd)
        {
            int year, month, day, hour, minute;
            decimal second;
            MJD2Common(mjd.Days, mjd.Seconds, out year, out month, out day, out hour, out minute, out second);
            return new CommonT(year, month, day, hour, minute, second);
        }

        public static void MJD2GPS(int days, double sod, out int weeks, out double sow)
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
        public static void MJD2GPS(Day mjd, out int weeks, out double sow)
        {
            MJD2GPS(mjd.Days, mjd.Seconds, out weeks, out sow);
        }
        public static Week MJD2GPS(Day mjd)
        {
            int weeks;
            double sow;

            MJD2GPS(mjd, out weeks, out sow);

            return new Week(weeks, sow);
        }

        /// <summary>
        /// 通用时转GPS时
        /// </summary>
        /// <param name="week"></param>
        /// <param name="sow"></param>
        /// <param name="dt"></param>
        /// <param name="picoSeconds"></param>
        public static void Common2GPS(out int week, out double sow, CommonT dt)
        {
            Day mjd = Common2MJD(dt);
            MJD2GPS(mjd, out week, out sow);
        }
        public static Week Common2GPS(CommonT dt)
        {
            int week;
            double sow;

            Common2GPS(out week, out sow, dt);

            return new Week(week, sow);
        }
        public static void Common2GPS(int year, int month, int day, int hour, int minute, decimal second, out int week, out double sow)
        {
            CommonT ct = new CommonT(year, month, day, hour, minute, second);
            Common2GPS(out week, out sow, ct);
        }

        /// <summary>
        /// GPS时转儒略日
        /// </summary>
        /// <param name="weeks"></param>
        /// <param name="sow"></param>
        /// <param name="days"></param>
        /// <param name="sod"></param>
        public static void GPS2MJD(int weeks, double sow, out int days, out double sod)
        {
            int dow;
            sod = sow;

            // 防止出现负数
            if (sow < 0)
            {
                sow += SecondsPerWeek;
                weeks--;
            }

            dow = (int)(sow / 86400);
            sod = sow - dow * 86400;
            days = weeks * 7 + 44244 + dow;
        }
        public static Day GPS2MJD(Week week)
        {
            int days;
            double sod;
            GPS2MJD(week.Weeks, week.Seconds, out days, out sod);

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
        public static void GPS2Common(int weeks, double sow, out int year, out int month, out int day, out int hour, out int minute, out decimal second)
        {
            int days;
            double sod;

            GPS2MJD(weeks, sow, out days, out sod);
            MJD2Common(days, sod, out year, out month, out day, out hour, out minute, out second);
        }
        public static CommonT GPS2Common(Week gpsWeek)
        {
            int year, month, day, hour, minute;
            decimal second;

            GPS2Common(gpsWeek.Weeks, gpsWeek.Seconds, out year, out month, out day, out hour, out minute, out second);

            return new CommonT(year, month, day, hour, minute, second);
        }

        public static CommonT DOY2Common(int year, int doy)
        {
            int[] dayNum = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(year))
            {
                dayNum[1] = 29;
            }

            int month = 0;
            int dayOfMonth = 0;
            for (int i = 0; i < 12; i++)
            {
                if (doy <= dayNum[i])
                {
                    month = i + 1;
                    dayOfMonth = doy;
                    break;
                }

                doy -= dayNum[i];
            }

            return new CommonT(year, month, dayOfMonth, 0, 0, 0m);
        }

        /// <summary>
        /// 年积日转换成年月日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="doy"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static void DOY2MonthDay(int year, int doy, out int month, out int day)
        {
            int[] dayNum = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(year))
            {
                dayNum[1] = 29;
            }

            month = 1;
            day = doy;
            while (day > dayNum[month - 1])
            {
                day -= dayNum[month - 1];
                month++;
            }
        }
        public static void DOY2MonthDay(int doy, out int year,out int month, out int dom)
        {
            year = doy / 1000;
            int day = doy % 1000;

            DOY2MonthDay(year, day, out month, out dom);
        }
        /// <summary>
        /// 年月日转换成年积日
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static int MonthDay2DOY(int year, int month, int day)
        {
            if (month == 1) return day;

            int[] daysPerMonth = DateTime.IsLeapYear(year) ? DaysPerMonthLeapYear : DaysPerMonth;

            int doy = 0;
            for (int i = 0; i < month - 1; i++)
            {
                doy += daysPerMonth[i];
            }
            doy += day;

            return doy;
        }

        /// <summary>
        /// GPS周转年月日
        /// </summary>
        /// <param name="week"></param>
        /// <param name="dayOfWeek"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dayOfMonth"></param>
        public static void GPS2YearMonthDay(int week, int dayOfWeek, out int year, out int month, out int dayOfMonth)
        {
            year = 0;
            month = 0;
            dayOfMonth = 0;

            // GPS所有天数
            int dayNum = week * 7 + dayOfWeek;

            // 计算实际日期
            DateTime dt = GPST.StartTime.AddDays(dayNum);

            year = dt.Year;
            month = dt.Month;
            dayOfMonth = dt.Day;
        }
        /// <summary>
        /// 年月日转GPS周
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="dayOfMonth"></param>
        /// <param name="week"></param>
        /// <param name="dayOfWeek"></param>
        public static void YearMonthDay2GPS(int year, int month, int dayOfMonth, out int week, out int dayOfWeek)
        {
            DateTime dt = new DateTime(year, month, dayOfMonth);

            // GPS时的所有天数
            int dayNum = (dt - GPST.StartTime).Days;

            week = (int)Math.Floor(dayNum / 7d);
            dayOfWeek = dayNum - week * 7;
        }

        public static void DOY2GPS(int year, int doy, out int week, out int dow)
        {
            DateTime dt = new DateTime(year, 1, 1);

            // GPS时的所有天数
            int dayNum = (dt - GPST.StartTime).Days + doy - 1;

            week = (int)Math.Floor(dayNum / 7d);
            dow = dayNum - week * 7;
        }
        public static void GPS2DOY(int week, int dow, out int year, out int doy)
        {
            DateTime dt = new DateTime(1980,1,6);

            dt=dt.AddDays(week*7+dow);
            year = dt.Year;
            doy = dt.DayOfYear;
        }

        public static int GetYear2(int year4)
        {
            if (year4 >= 2000) return year4 - 2000;
            else return year4 - 1900;
        }
    }
}
