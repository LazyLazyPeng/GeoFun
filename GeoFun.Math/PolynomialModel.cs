using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Statistics;
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

        public PolynomialModel() { }

        public PolynomialModel(int order)
        {
            Order = order;
        }


        public bool Fit(List<double> x, List<double> y)
        {
            if (x is null || y is null) return false;
            if (x.Count < 2 || y.Count < 2) return false;

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
            return true;
        }

        public static PolynomialModel Fit(List<double> x, List<double> y, int order)
        {
            if (x is null || y is null) return null;
            PolynomialModel pm = new PolynomialModel(2);
            pm.Order = order;

            int n = System.Math.Min(x.Count, y.Count);
            DenseMatrix b = new DenseMatrix(n, order + 1);
            DenseVector l = new DenseVector(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < order + 1; j++)
                {
                    b[i, j] = System.Math.Pow(x[i], j);
                    l[i] = y[i];
                }
            }

            Vector<double> r = (b.Transpose() * b).Inverse() * (b.Transpose() * l);
            pm.Factor = r.ToList();
            return pm;
        }

        public bool Fit(List<double> x, List<double> y, out List<double> residue, out double sigma)
        {
            sigma = 0d;
            residue = null;
            if (x is null || y is null) return false;
            int n = System.Math.Min(x.Count, y.Count);
            if (n < 2) return false;

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
            Vector<double> resi = b * r - l;
            residue = resi.ToList();
            sigma = resi.StandardDeviation();
            return true;
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
