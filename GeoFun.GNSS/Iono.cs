using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

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
        public static double STEC2VTEC(double stec, double ele, double earthRadius = 6371100, double ionoHeight = 450000)
        {
            double sinz = Math.Sin(PI / 2d - ele);
            double sinzz = earthRadius / (earthRadius + ionoHeight) * sinz;
            double coszz = Math.Sqrt(1d - sinzz * sinzz);
            return stec * coszz;
        }

        /// <summary>
        /// 计算穿刺点坐标
        /// </summary>
        /// <param name="xSat"></param>
        /// <param name="ySat"></param>
        /// <param name="zSat"></param>
        /// <param name="xRec"></param>
        /// <param name="yRec"></param>
        /// <param name="zRec"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="earthR"></param>
        /// <param name="ionoH"></param>
        public static void CalIPP(double xSat, double ySat, double zSat,
            double xRec, double yRec, double zRec,
            out double x, out double y, out double z,
            double earthR = 63781000, double ionoH = 450000)
        {
            x = y = z = 0d;

            Vector<double> op1 = new DenseVector(new double[] { xRec, yRec, zRec });
            Vector<double> op2 = new DenseVector(new double[] { xSat, ySat, zSat });

            Vector<double> p1p2 = op1 - op2;
            p1p2 = p1p2 / p1p2.L2Norm();

            double a = p1p2.DotProduct(p1p2);
            double b = 2 * p1p2.DotProduct(op1);
            double c = op1.DotProduct(op1) - Math.Pow(earthR + ionoH, 2);

            double t1, t2;
            double delta = b * b - 4 * a * c;
            if (delta < 1e-14) return;

            else
            {
                t1 = (-b + Math.Sqrt(delta)) / 2d / a;
                t2 = (-b - Math.Sqrt(delta)) / 2d / a;

                var oi1 = op1 + t1 * p1p2;
                var oi2 = op1 + t2 * p1p2;

                var i1p2 = op2 - oi1;
                var i2p2 = op2 - oi2;

                double d1 = i1p2.DotProduct(i1p2);
                double d2 = i2p2.DotProduct(i2p2);

                if (d1 < d2)
                {
                    x = oi1[0];
                    y = oi1[1];
                    z = oi1[2];
                }
                else
                {
                    x = oi1[0];
                    y = oi1[1];
                    z = oi1[2];
                }
            }
        }
    }
}
