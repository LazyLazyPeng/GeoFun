using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Iono
    {
        public static double PI = 3.1415926535897932;

        /// <summary>
        /// 将stec转换为vtec
        /// </summary>
        /// <param name="stec"></param>
        /// <param name="ele">高度角(弧度)</param>
        /// <param name="earthRadius">地球半径(默认6371000)</param>
        /// <param name="ionoHeight">电离层单层模型高度(默认450000)</param>
        /// <returns></returns>
        public static double STEC2VTEC(double stec, double ele,double earthRadius=6371100, double ionoHeight = 450000)
        {
            double sinz = Math.Sin(PI/2d-ele);
            double sinzz = earthRadius / (earthRadius + ionoHeight) * sinz;
            double coszz = Math.Sqrt(1d-sinzz * sinzz);
            return stec * coszz;
        }
    }
}
