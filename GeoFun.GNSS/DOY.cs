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
        public int TotalDaysThisYear
        {
            get
            {
                return DateTime.IsLeapYear(Year) ? 366 : 365;
            }
        }

        public DOY(int year, int day)
        {
            Year = year;
            Day = day;
        }

        public void AddDays(int dayNum)
        {
            if (dayNum >= 0)
            {
                Day += dayNum;
                while (Day > TotalDaysThisYear)
                {
                    Day -= TotalDaysThisYear;
                    Year++;
                }
            }
            else
            {
                Day += dayNum;
                while (Day <= 0)
                {
                    Day += TotalDaysThisYear;
                    Year--;
                }
            }
        }


        public static bool operator ==(DOY doy1, DOY doy2)
        {
            return (doy1.Year == doy2.Year) && (doy1.Day == doy2.Day);
        }

        public static bool operator !=(DOY doy1, DOY doy2)
        {
            return (doy1.Year != doy2.Year) || (doy1.Day != doy2.Day);
        }

        public static bool operator <(DOY doy1, DOY doy2)
        {
            bool flag = true;
            if (doy1.Year > doy2.Year) flag = false;
            else if (doy1.Year == doy2.Year)
            {
                if (doy1.Day >= doy2.Day) flag = false;
            }
  
            return flag;
        }
        public static bool operator >(DOY doy1, DOY doy2)
        {
            bool flag = true;
            if (doy1.Year < doy2.Year) flag = false;
            else if (doy1.Year == doy2.Year)
            {
                if (doy1.Day <= doy2.Day) flag = false;
            }

            return flag;
        }

        public static bool operator <=(DOY doy1, DOY doy2)
        {
            return doy1 < doy2 || doy1 == doy2;
        }
        public static bool operator >=(DOY doy1, DOY doy2)
        {
            return doy1 > doy2 || doy1 == doy2;
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

        private int GetDaysPerYear()
        {
            return DateTime.IsLeapYear(Year) ? 366 : 365;
        }
    }
}
