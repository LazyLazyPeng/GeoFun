using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class Swaper
    {
        /// <summary>
        /// 交换两个数的值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref int a,ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        /// <summary>
        /// 交换两个数的值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref double a, ref double b)
        {
            double temp = a;
            a = b;
            b = temp;
        }
        /// <summary>
        /// 交换两个数的值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap(ref decimal a, ref decimal b)
        {
            decimal temp = a;
            a = b;
            b = temp;
        }
        /// <summary>
        /// 交换两个变量的值
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
