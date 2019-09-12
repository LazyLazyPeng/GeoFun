using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GMath
{
    public class Legendre
    {
        /// <summary>
        /// 计算完全归一化的缔合(连带,伴随)勒让德多项式
        /// </summary>
        /// <param name="nn">阶</param>
        /// <param name="mm">次</param>
        /// <param name="theta">角度(弧度)</param>
        /// <returns></returns>
        public static double lpmv(int n, int m, double theta)
        {
            double nn = n;
            double mm = m;
            if (m < 0 || n < 0 || m > nn) return 0;
            else if (m == 0 && n == 0) return 1;
            else if (m == 1 && n == 0) return Math.Sqrt(3) * Math.Cos(theta);
            else if (m == 1 && n == 1) return Math.Sqrt(3) * Math.Sin(theta);
            else if (m == n)
            {
                double c = Math.Sqrt((2 * mm + 1) / (2 * mm));
                return c *Math.Sin(theta)* lpmv(n - 1, m - 1, theta);
            }
            else
            {
                double a = Math.Sqrt((2 * nn - 1) * (2 * nn + 1) / (nn - mm) / (nn + mm));
                double b = Math.Sqrt((2 * nn + 1) * (nn + mm - 1) * (nn - mm - 1) / (nn + mm) / (nn - mm) / (2 * nn - 3));
                return a * Math.Cos(theta) * lpmv(n - 1, m, theta) - b * lpmv(n - 2, m, theta);
            }
        }

        /// <summary>
        /// 计算完全归一化的勒让德多项式
        /// </summary>
        /// <param name="n">阶</param>
        /// <param name="m">次</param>
        /// <param name="theta">角度(弧度)</param>
        /// <returns></returns>
        public static List<double> lpmn(int n, int m, double theta)
        {
            return null;
        }
    }
}
