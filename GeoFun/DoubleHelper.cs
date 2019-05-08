using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class DoubleHelper
    {
        private static readonly double DIFFERENCE = 1E-15;
        /// <summary>
        /// 很小的一个double，double的精度为15位
        /// </summary>
        public static readonly double LITTLE_DOUBLE = 1E-14;

        /// <summary>
        /// 判断两个double类型的数据是否相等
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool Equals(double d1, double d2)
        {
            if (Math.Abs(d1 - d2) < DIFFERENCE) return true;

            return false;
        }

        public static double Parse(string str, double defaultValue =0d)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }

            return double.Parse(str);
        }

        /// <summary>
        /// 将double分解成整数和小数部分
        /// </summary>
        /// <param name="value"></param>
        /// <param name="intPart"></param>
        /// <param name="doublePart"></param>
        public static void Separate(double value, out int intPart, out double doublePart)
        {
            intPart = (int)Math.Floor(value+1e-14);
            doublePart = value - intPart;
        }
        public static void Separate(double value, out int intPart, out ulong doublePart)
        {
            intPart = (int)Math.Floor(value);
            doublePart = (ulong)Math.Floor((value - intPart) * 1e12);
        }


        /// <summary>
        /// 将double分解成整数和小数部分
        /// </summary>
        /// <param name="value"></param>
        /// <param name="intPart"></param>
        /// <param name="doublePart"></param>
        public static void Separate(double value, out ulong intPart, out double doublePart)
        {
            intPart = (ulong)Math.Floor(value);
            doublePart = value - intPart;
        }

    }
}
