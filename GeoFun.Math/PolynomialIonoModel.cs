using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace GeoFun.MathUtils
{
    public class PolynomialIonoModel
    {
        /// <summary>
        /// 次数
        /// </summary>
        public int XOrder { get; set; } = 2;
        /// <summary>
        /// 阶数
        /// </summary>
        public int YOrder { get; set; } = 4;

        /// <summary>
        /// 多项式系数
        /// </summary>
        public double[,] Factor= null;

        private void initFactor()
        {
            if (XOrder <= 0 || YOrder <= 0) return;
            Factor = new double[XOrder+1, YOrder+1];

            for(int i =0; i <=XOrder; i++)
            {
                for(int j =0; j<=YOrder; j++)
                {
                    Factor[i, j] = 0;
                }
            }
        }

        public PolynomialIonoModel(int xOrder = 2, int yOrder = 4)
        {
            XOrder = xOrder;
            YOrder = yOrder;
            initFactor();
        }

        /// <summary>
        /// 多项式模型拟合
        /// </summary>
        /// <param name="b">地理纬度(弧度)</param>
        /// <param name="s">太阳时角差=经度差+时间差(弧度)</param>
        /// <param name="vtec">vtec</param>
        /// <returns></returns>
        public bool Fit(List<double> bb, List<double> s, List<double> vtec)
        {
            if (bb is null || s is null || vtec is null) return false;
            int obsNum = MathHelper.Min(bb.Count, s.Count, vtec.Count);
            int paraNum = (XOrder + 1) * (YOrder + 1);
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
                for (int j = 0; j <= XOrder; j++)
                {
                    for (int k = 0; k <= YOrder; k++)
                    {
                        b[i, paraInd] = Math.Pow(bb[i], j) * Math.Pow(s[i], k);
                        paraInd++;
                    }
                }
            }

            Vector<double> r = (b.Transpose() * b).Inverse() * (b.Transpose() * l);
            for (int i = 0; i <= XOrder; i++)
            {
                for (int j = 0; j <= YOrder; j++)
                {
                    int index = i * (YOrder + 1) + j;
                    Factor[i, j] = r[index];
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
            for (int i = 0; i <= XOrder; i++)
            {
                for (int j = 0; j <= YOrder; j++)
                {
                    sum += Factor[i,j] * Math.Pow(b, i) * Math.Pow(s, j);
                }
            }
            return sum;
        }
    }
}
