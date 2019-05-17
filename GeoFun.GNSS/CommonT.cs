using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 通用时,年月日表示的
    /// </summary>
    public class CommonT
    {
        private DateTime dateT = DateTime.MinValue;

        /// <summary>
        /// 年
        /// </summary>
        public int Year
        {
            get
            {
                return dateT.Year;
            }
        }
        /// <summary>
        /// 月
        /// </summary>
        public int Month
        {
            get
            {
                return dateT.Month;
            }
        }
        /// <summary>
        /// 日
        /// </summary>
        public int Day
        {
            get
            {
                return dateT.Day;
            }
        }

        /// <summary>
        /// 时
        /// </summary>
        public int Hour
        {
            get
            {
                return dateT.Hour;
            }
        }
        /// <summary>
        /// 分
        /// </summary>
        public int Minute
        {
            get
            {
                return dateT.Minute;
            }
        }

        public decimal second = 0m;
        /// <summary>
        /// 秒
        /// </summary>
        public decimal Second
        {
            get
            {
                return second;
            }
            set
            {
                second = value;
            }
        }

        /// <summary>
        /// 一天内的秒数
        /// </summary>
        public decimal SecondOfDay
        {
            get
            {
                return Hour * 3600m + Minute * 60m + Second;
            }
        }

        /// <summary>
        /// 年积日
        /// </summary>
        public int DayOfYear
        {
            get
            {
                return dateT.DayOfYear;
            }
        }

        public CommonT(int year,int month,int day,int hour,int minute,decimal second)
        {
            dateT = new DateTime(year, month, day, hour, minute,0);
            Second = second;
        }

        public CommonT(int year,int month,int day,int hour,int minute,double second)
        {
            dateT = new DateTime(year, month, day, hour, minute,0);
            Second = (decimal)second;
        }
    }
}
