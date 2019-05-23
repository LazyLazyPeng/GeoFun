using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 以周表示的时间系统
    /// </summary>
    public class Week
    {
        public Week(int weeks, double sow)
        {
            Weeks = weeks;
            Seconds = sow;
        }

        /// <summary>
        /// 周数
        /// </summary>
        public int Weeks { get; set; }
        /// <summary>
        /// 一周之内的小数秒
        /// </summary>
        public double Seconds { get; set; }

        /// <summary>
        /// 总的周数
        /// </summary>
        public double TotalWeeks
        {
            get
            {
                return Weeks + Seconds / Time.SecondsPerWeek;
            }
            set
            {
                int weeks;
                double sow;
                DoubleHelper.Separate(value, out weeks, out sow);
                Weeks = weeks;
                Seconds = sow;
            }
        }

        /// <summary>
        /// 总的秒数
        /// </summary>
        public double TotalSeconds
        {
            get
            {
                return Weeks * Time.SecondsPerWeek + Seconds;
            }
        }
    }
}
