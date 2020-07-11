using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MathUtils
{
    public class Interp
    {
		/**
         * 一元全区间不等距插值
         * 
         * @param n - 结点的个数
         * @param x - 一维数组，长度为n，存放给定的n个结点的值x(i)，
         *            要求x(0)<x(1)<...<x(n-1)
         * @param y - 一维数组，长度为n，存放给定的n个结点的函数值y(i)，
         *            y(i) = f(x(i)), i=0,1,...,n-1
         * @param t - 存放指定的插值点的值
         * @return double 型，指定的查指点t的函数近似值f(t)
         */
		public static double GetValueLagrange(int n, double[] x, double[] y, double t)
		{
			int i, j, k, m;
			double z, s;

			// 初值
			z = 0.0;

			// 特例处理
			if (n < 1)
				return (z);
			if (n == 1)
			{
				z = y[0];
				return (z);
			}

			if (n == 2)
			{
				z = (y[0] * (t - x[1]) - y[1] * (t - x[0])) / (x[0] - x[1]);
				return (z);
			}

			// 开始插值
			i = 0;
			while ((i < n) && (x[i] < t))
				i = i + 1;

			k = i - 4;
			if (k < 0)
				k = 0;

			m = i + 3;
			if (m > n - 1)
				m = n - 1;
			for (i = k; i <= m; i++)
			{
				s = 1.0;
				for (j = k; j <= m; j++)
				{
					if (j != i)
						// 拉格朗日插值公式
						s = s * (t - x[j]) / (x[i] - x[j]);
				}

				z = z + s * y[i];
			}

			return (z);
		}
	}
}
