using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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

        public DOY AddDays(int dayNum)
        {
            int year = Year;
            int day = Day;

            if (dayNum >= 0)
            {
                day += dayNum;
                while (day > Time.DayNumOfYear(year))
                {
                    day -= Time.DayNumOfYear(year);
                    year++;
                }
            }
            else
            {
                day += dayNum;
                while (day <= 0)
                {
                    day += Time.DayNumOfYear(year - 1);
                    year--;
                }
            }

            return new DOY(year, day);
        }

        public DOY Copy()
        {
            return new DOY(Year, Day);
        }

        public static bool operator ==(DOY doy1, DOY doy2)
        {
            if (doy1 is null && doy2 is null) return true;
            if (doy1 is null || doy2 is null) return false;
            return (doy1.Year == doy2.Year) && (doy1.Day == doy2.Day);
        }
        public static bool operator !=(DOY doy1, DOY doy2)
        {
            if (doy1 is null && doy2 is null) return false;
            if (doy1 is null || doy2 is null) return true;
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

        public static int operator -(DOY doy1, DOY doy2)
        {
            int month, dom;
            doy1.GetMonthDay(out month, out dom);
            DateTime dt1 = new DateTime(doy1.Year, month, dom);
            doy2.GetMonthDay(out month, out dom);
            DateTime dt2 = new DateTime(doy2.Year, month,dom);

            var span = dt1 - dt2;
            return (int)span.TotalDays;
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

        public int GetMonth()
        {
            int[] daysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(Year)) daysOfMonth[1] = 29;

            int month = 1;
            int dayNum = 0;
            while (dayNum + daysOfMonth[month - 1] < Day)
            {
                dayNum += daysOfMonth[month - 1];
                month++;
            }

            return month;
        }

        public void GetMonthDay(out int month, out int dom)
        {
            int[] daysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if (DateTime.IsLeapYear(Year)) daysOfMonth[1] = 29;

            month = 1;
            int dayNum = 0;
            while (dayNum + daysOfMonth[month - 1] < Day)
            {
                dayNum += daysOfMonth[month - 1];
                month++;
            }

            dom = Day - dayNum;
        }
    }
}
