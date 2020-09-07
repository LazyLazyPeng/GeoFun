using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeoFun.MathUtils
{
    public class PolynomialModel
    {
        /// <summary>
        /// 阶数
        /// </summary>
        public int Order = 2;
        /// <summary>
        /// 系数
        /// </summary>
        public List<double> Factor = new List<double>();

        public void Fit(List<double> x, List<double> y)
        {
            if (x is null || y is null) return;

            int n = System.Math.Min(x.Count, y.Count);
            DenseMatrix b = new DenseMatrix(n, Order + 1);
            DenseVector l = new DenseVector(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < Order + 1; j++)
                {
                    b[i, j] = System.Math.Pow(x[i], j);
                    l[i] = y[i];
                }
            }

            Vector<double> r = (b.Transpose() * b).Inverse() * (b.Transpose() * l);
            Factor = r.ToList();
        }

        public List<double> CalFit(List<double> x)
        {
            if (x is null) return null;
            List<double> y = new List<double>();
            foreach (var xx in x)
            {
                y.Add(CalFit(xx));
            }
            return y;
        }

        public double CalFit(double x)
        {
            double sum = 0d;
            for (int i = 0; i < Factor.Count; i++)
            {
                sum += Factor[i] * Math.Pow(x, i);
            }
            return sum;
        }
    }
}
