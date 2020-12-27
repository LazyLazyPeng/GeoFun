using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MathUtils
{
    public class PolynominalIonoModel
    {
        /// <summary>
        /// 次数
        /// </summary>
        public int Degree { get; set; } = 4;
        /// <summary>
        /// 阶数
        /// </summary>
        public int Order { get; set; } = 2;

        /// <summary>
        /// 系数
        /// </summary>
        public Dictionary<int, Dictionary<int, double>> Factor = new Dictionary<int, Dictionary<int, double>>();

        public PolynominalIonoModel(int order = 2, int degree = 4)
        {
            Order = order;
            Degree = degree;

            // 初始化系数
            for (int i = 0; i < order; i++)
            {
                Factor[i] = new Dictionary<int, double>();
                for (int j = 0; j < degree; j++)
                {
                    Factor[i].Add(j, 0);
                }
            }
        }

        /// <summary>
        /// 多项式模型拟合
        /// </summary>
        /// <param name="b">地理纬度(弧度)</param>
        /// <param name="s">太阳时角差=纬度差+时间差(弧度)</param>
        /// <param name="vtec">vtec</param>
        /// <returns></returns>
        public bool Fit(List<double> bb, List<double> s, List<double> vtec)
        {
            if (bb is null || s is null || vtec is null) return false;
            int obsNum = MathHelper.Min(bb.Count, s.Count, vtec.Count);
            int paraNum = (Degree + 1) * (Order + 1);
            if(obsNum<paraNum)
            {
                return false;
            }

            int paraInd = 0;
            DenseMatrix b = new DenseMatrix(obsNum, paraNum);
            DenseVector l = new DenseVector(obsNum);
            for (int i = 0; i < obsNum; i++)
            {
                paraInd = 0;
                for (int j = 0; j < Order + 1; j++)
                {
                    for (int k = 0; k < Order + 1; k++)
                    {
                        b[i, paraInd] = Math.Pow(bb[i], j) * Math.Pow(s[i], k);
                        paraInd++;
                    }
                }
            }

            Vector<double> r = (b.Transpose() * b).Inverse() * (b.Transpose() * l);
            for (int i = 0; i <= Order; i++)
            {
                for (int j = 0; j <= Degree; j++)
                {
                    Factor[i][j] = r[i * (Degree + 1) + j];
                }
            }
            return true;
        }

        public List<double> CalFit(List<double> b, List<double> s)
        {
            if (b is null || s is null) return null;
            List<double> tec = new List<double>();
            int n = Math.Min(b.Count, s.Count);

            double t = 0;
            for (int i = 0; i < n; i++)
            {
                t = CalFit(b[i], s[i]);
                tec.Add(t);
            }
            return tec;
        }

        public double CalFit(double b, double s)
        {
            double sum = 0d;
            for (int i = 0; i <= Order; i++)
            {
                for (int j = 0; j <= Order; j++)
                {
                    sum += Factor[i][j] * Math.Pow(b, i) * Math.Pow(s, j);
                }
            }
            return sum;
        }
    }
}
