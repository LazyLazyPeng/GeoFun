using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class DOY : IComparable<DOY>
    {
        public int Year { get; set; } = 1970;
        public int Day { get; set; } = 1;

        public DOY(int year, int day)
        {
            Year = year;
            Day = day;
        }

        /// <summary>
        /// 与其他日期进行比较，以便排序
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(DOY other)
        {
            if (other is null) return 1;
            else if (Year < other.Year || Year > other.Year)
            {
                return Year.CompareTo(other.Year);
            }
            else
            {
                return Day.CompareTo(other.Day);
            }
        }
    }
}
