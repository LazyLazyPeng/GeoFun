using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 按天计数的时间系统
    /// </summary>
    public class Day
    {
        public Day(double totalDay)
        {
            TotalDays = totalDay;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="day">天数</param>
        /// <param name="sod">一天内的小数部分</param>
        public Day(int day = 0, double sod=0d)
        {
            Days = day;
            Seconds = sod;
        }

        /// <summary>
        /// 全部的天数(包括一天之内的小数部分)
        /// </summary>
        public double TotalDays
        {
            get
            {
                return Days + Seconds;
            }
            set
            {
                int days;
                double sod;
                DoubleHelper.Separate(value, out days, out sod);
                Days = days;
                Seconds = sod;
            }
        }

        /// <summary>
        /// 天数
        /// </summary>
        public int Days { get; set; }
        /// <summary>
        /// 一天之内的秒数
        /// </summary>
        public double Seconds { get; set; }
    }

}
