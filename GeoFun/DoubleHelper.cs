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
    }
}
