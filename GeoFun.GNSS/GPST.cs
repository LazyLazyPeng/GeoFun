using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    /// <summary>
    /// GPS时间
    /// </summary>
    public sealed class GPST
    {
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
                return SecondsOfDay * 24m * 3600m + SecondsOfDay;
            }
        }

        /// <summary>
        /// 简化儒略日
        /// </summary>
        public Day MJD;

        public GPST(int year, int month, int day, int hour, int minute, int second,ulong picoSecond)
        {
            CommonT = new CommonT(year, month, day, hour, minute, second);

            MJD = Time.CommonToMJD(commonT, PicoSeconds);
            week = Time.MJDToGPS(MJD);
        }

        public GPST(int year, int month, int day, int hour, int minute, double second)
        {
            int secInt;double secDouble;
            DoubleHelper.Separate(second, out secInt, out secDouble);

            CommonT = new CommonT(year, month, day, hour, minute, secInt);

            MJD = Time.CommonToMJD(commonT,picoSeconds);
            week = Time.CommonToGPS(commonT);
        }

        public GPST(int weeks, double seconds)
        {
            week = new Week(weeks, seconds);


        }

        /// <summary>
        /// 解码时间字符串,以空格分割
        /// </summary>
        /// <param name="timeStr"></param>
        public static GPST Decode(string timeStr)
        {
            string[] segs = StringHelper.SplitFields(timeStr);

            ulong picoSecond;
            double seconds;
            int year, month, day, hour, minute,second;

            year = int.Parse(segs[0]);
            month = int.Parse(segs[1]);
            day = int.Parse(segs[2]);
            hour = int.Parse(segs[3]);
            minute = int.Parse(segs[4]);
            seconds = double.Parse(segs[5]);

            if (year < 50) year += 2000;
            else if (year < 100) year += 1900;

            DoubleHelper.Separate(seconds, out second, out picoSecond);

            return new GPST(year, month, day, hour, minute, second, picoSecond);
        }
    }
}
