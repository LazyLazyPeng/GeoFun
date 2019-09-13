using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Inter
{
    public class Interpolation
    {
        /// <summary>
        /// 无效值
        /// </summary>
        public static decimal NoData = 9999;

        /// <summary>
        /// 双线性插值
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="minb"></param>
        /// <param name="maxb"></param>
        /// <param name="minl"></param>
        /// <param name="maxl"></param>
        /// <param name="lb">左下角</param>
        /// <param name="rb">右下角</param>
        /// <param name="rt">右上角</param>
        /// <param name="lt">左上角</param>
        /// <returns></returns>
        public static double Bilinear(double b, double l,
            double minb, double maxb, double minl, double maxl,
            double lb, double rb, double rt, double lt)
        {
            return (double)Bilinear(b, l, minb, maxb, minl, maxl, lb, rb, rt, lt);
        }

        /// <summary>
        /// 双线性插值
        /// </summary>
        /// <param name="b"></param>
        /// <param name="l"></param>
        /// <param name="minb"></param>
        /// <param name="maxb"></param>
        /// <param name="minl"></param>
        /// <param name="maxl"></param>
        /// <param name="lb">左下角</param>
        /// <param name="rb">右下角</param>
        /// <param name="rt">右上角</param>
        /// <param name="lt">左上角</param>
        /// <returns></returns>
        public static decimal Bilinear(decimal b, decimal l,
            decimal minb, decimal maxb, decimal minl, decimal maxl,
            decimal lb, decimal rb, decimal rt, decimal lt)
        {
            #region 点是否落在角点上
            if (b == minb)
            {
                if (l == minl)//左下角
                {
                    return lb;
                }

                if (l == maxl)//右下角
                {
                    return rb;
                }
            }
            if (b == maxb)
            {
                if (l == minl)//左上角
                {
                    return lt;
                }

                if (l == maxl)//右上角
                {
                    return rt;
                }
            }
            #endregion

            //点落在左边界或者右边界
            if (l.ToString("f10") == minl.ToString("f10"))
            {
                if (lb > 9998 || lt > 9998) return NoData;
                return lt * (b - minb) / (maxb - minb) + lb * (maxb - b) / (maxb - minb);
            }
            else if (l.ToString("f10") == maxl.ToString("f10"))
            {
                if (rb > 9998 || rt > 9998) return NoData;

                return rt * (b - minb) / (maxb - minb) + rb * (maxb - b) / (maxb - minb);
            }

            //点落在上边界或者下边界
            if (b.ToString("f10") == minb.ToString("f10"))
            {
                if (lb > 9998 || rb > 9998) return NoData;
                return rb * (l - minl) / (maxl - minl) + lb * (maxl - l) / (maxl - minl);
            }
            else if (b.ToString("f10") == maxb.ToString("f10"))
            {
                if (lt > 9998 || rt > 9998) return NoData;
                return rt * (l - minl) / (maxl - minl) + lt * (maxl - l) / (maxl - minl);
            }

            if (lb > 9998 || rb > 9998 || lt > 9998 || rt > 9998) return NoData;

            return (lb * (maxb - b) * (maxl - l) + rb * (maxb - b) * (l - minl) +
                    lt * (b - minb) * (maxl - l) + rt * (b - minb) * (l - minl))
                    / (maxb - minb) / (maxl - minl);
        }
        
        public static decimal Lagrange(int degree,List<double> x, List<double> y,double x0)
        {
            return 0m;
        }
    }
}
