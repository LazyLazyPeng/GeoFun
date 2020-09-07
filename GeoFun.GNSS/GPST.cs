using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Math = System.Math;

namespace GeoFun.GNSS
{
    /// <summary>
    /// GPS时间
    /// </summary>
    public sealed class GPST : IComparable<GPST>
    {
        /// <summary>
        /// GPS时起始时刻
        /// </summary>
        public static readonly DateTime StartTime = new DateTime(1980, 1, 6);

        private Week week;
        /// <summary>
        /// GPS周
        /// </summary>
        public Week Week
        {
            get
            {
                return week;
            }
            set
            {
                week = value;
                commonT = Time.GPS2Common(week);
                mjd = Time.GPS2MJD(week);
            }
        }

        private CommonT commonT;
        /// <summary>
        /// 通用时,只精确到秒的整数部分
        /// </summary>
        public CommonT CommonT
        {
            get
            {
                return commonT;
            }
            set
            {
                commonT = value;

                mjd = Time.Common2MJD(commonT);
                week = Time.Common2GPS(commonT);
            }
        }

        /// <summary>
        /// 年积日
        /// </summary>
        public int DayOfYear
        {
            get
            {
                return CommonT.DayOfYear;
            }
        }
        /// <summary>
        /// 一天之内的小数部分，单位:秒
        /// </summary>
        public decimal SecondsOfDay
        {
            get
            {
                return CommonT.SecondOfDay;
            }
        }

        /// <summary>
        /// 一年之内的小数部分，单位：秒
        /// </summary>
        public decimal SecondsOfYear
        {
            get
            {
                return DayOfYear * 24m * 3600m + SecondsOfDay;
            }
        }

        /// <summary>
        /// 简化儒略日
        /// </summary>
        public Day mjd = new Day();
        public Day MJD
        {
            get
            {
                return mjd;
            }
            set
            {
                mjd = value;

                week = Time.MJD2GPS(mjd);
                commonT = Time.MJD2Common(mjd);
            }
        }

        public double TotalSeconds
        {
            get
            {
                return Week.TotalSeconds;
            }
        }


        public GPST(int year, int month, int day, int hour, int minute, double second)
        {
            CommonT = new CommonT(year, month, day, hour, minute, second);
        }

        public GPST(int year, int month, int day, int hour, int minute, decimal second)
        {
            CommonT = new CommonT(year, month, day, hour, minute, second);
        }

        public GPST(int weeks, double seconds)
        {
            Week = new Week(weeks, seconds);
        }

        public GPST(int year, int doy)
        {
            CommonT = Time.DOY2Common(year, doy);
        }

        public GPST(GPST gpst) : this(gpst.week.Weeks, gpst.week.Seconds)
        {
        }

        public void AddSeconds(double sec)
        {
            week.Seconds += sec;
            if (sec < 0)
            {
                int weekNum = (int)Math.Ceiling(sec / Time.SecondsPerWeek);
                week.Weeks -= weekNum;
                Week.Seconds += weekNum*Time.SecondsPerWeek;
                Week = Week;
            }
            else
            {
                int weekNum = (int)Math.Floor(sec / Time.SecondsPerWeek);
                week.Weeks += weekNum;
                Week.Seconds = sec - weekNum * Time.SecondsPerWeek;
                Week = Week;
            }
        }

        /// <summary>
        /// 解码时间字符串,以空格分割
        /// </summary>
        /// <param name="timeStr"></param>
        public static GPST Decode(string timeStr)
        {
            string[] segs = StringHelper.SplitFields(timeStr);
            if (segs.Length < 6) return null;

            double seconds;
            int year, month, day, hour, minute;

            year = int.Parse(segs[0]);
            month = int.Parse(segs[1]);
            day = int.Parse(segs[2]);
            hour = int.Parse(segs[3]);
            minute = int.Parse(segs[4]);
            seconds = double.Parse(segs[5]);

            if (year < 50) year += 2000;
            else if (year < 100) year += 1900;

            return new GPST(year, month, day, hour, minute, seconds);
        }

        public string ToRinexString(string version = "2.11")
        {
            if (version == "2.11")
            {
                int year = CommonT.Year > 2000 ? CommonT.Year - 2000 : CommonT.Year - 1900;
                return string.Format("{0} {1,2} {2,2} {3,2} {4,2} {5,11:f7}", year, CommonT.Month, CommonT.Day,
                    CommonT.Hour, CommonT.Minute, commonT.second);
            }
            return "";
        }

        public int CompareTo(GPST other)
        {
            if (other is null) throw new Exception("无法比较null的大小");
            if (Math.Abs(MJD.Seconds - other.MJD.Seconds) < 1e-12) return 0;
            else if (MJD.Seconds - other.MJD.Seconds > 0) return 1;
            else return -1;
        }

        /// <summary>
        /// 返回两个日期之间的秒数
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static double operator -(GPST t1, GPST t2)
        {
            if (t1 is null || t2 is null) return 0d;

            return t1.Week.TotalSeconds - t2.Week.TotalSeconds;
        }
    }
}
