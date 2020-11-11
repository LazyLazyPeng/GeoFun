using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Solvers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeoFun.MathUtils
{
    /// <summary>
    /// 球谐函数模型
    /// </summary>
    public class SphericalHarmonicIonoModel
    {
        public int Degree { get; set; } = 0;
        public int Order { get; set; } = 0;

        /// <summary>
        /// 模型参数,完全归一化,一维，排列顺序为 A00 B00 A10 B10 ......
        /// </summary>
        public Vector<double> Factor = null;

        /// <summary>
        /// 最小二乘拟合球谐函数模型(单站,不估DCB)
        /// </summary>
        /// <param name="degree">阶</param>
        /// <param name="order">次</param>
        /// <param name="lat">纬度(弧度)</param>
        /// <param name="lon">经度(弧度)</param>
        /// <param name="vtec">VTEC(TECU)</param>
        /// <returns></returns>
        public static SphericalHarmonicIonoModel CalculateModel(int degree, int order,
            List<double> lat, List<double> lon, List<double> vtec)
        {
            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (lat is null || lon is null || vtec is null) return null;

            //// 检测观测值个数是否满足要求
            int minCount = System.Math.Min(System.Math.Min(lat.Count, lon.Count), vtec.Count);
            int paraNum = (degree + 1) * (degree + 1);
            if (minCount == 0) return null;
            if (minCount < paraNum)
            {
                throw new Exception("球谐函数,观测值个数无法满足建模要求");
            }

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(minCount, (int)paraNum);
            Vector<double> L = new DenseVector(minCount);
            for (int i = 0; i < minCount; i++)
            {
                int col = 0;
                for (int n = 0; n <= degree; n++)
                {
                    for (int m = 0; m <= n; m++)
                    {
                        double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - lat[i]);
                        B[i, col] = Pnm * System.Math.Cos(m * lon[i]);
                        col++;
                        // Bn0不需要估计,因为sin(0)乘任何数都是0
                        if (m == 0) continue;
                        B[i, col] = Pnm * System.Math.Sin(m * lon[i]);
                        col++;
                    }
                }

                L[i] = vtec[i];
            }

            //// 求解
            var btb = B.Transpose() * B;
            var btbi = btb.Inverse();
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            SphericalHarmonicIonoModel model = new SphericalHarmonicIonoModel();
            model.Degree = degree;
            model.Order = order;
            model.Factor = x;

            return model;
        }

        /// <summary>
        /// 计算某一处的模型值
        /// </summary>
        /// <param name="lat">弧度</param>
        /// <param name="lon">弧度</param>
        /// <returns></returns>
        public double Calculate(double lat, double lon)
        {
            double result = 0d;

            int index = 0;
            Vector<double> B = new DenseVector((int)(Degree + 1) * (int)(Degree + 1));
            for (int n = 0; n <= Degree; n++)
            {
                for (int m = 0; m <= n; m++)
                {
                    double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - lat);
                    B[index] = System.Math.Cos(m * lon) * Pnm;
                    index++;
                    if (m == 0) B[index] = 0;
                    else
                    {
                        B[index] = System.Math.Sin(m * lon) * Pnm;
                        index++;
                    }
                }
            }

            result = B.DotProduct(Factor);
            return result;
        }

        public void SaveAs(string path)
        {
            string content = string.Format("{0} {1}\n",Degree,Order);

            double cnm, snm;
            int index = 0;
            for(int i = 0; i < Order; i++)
            {
                for (int j = 0; j < Order; j++)
                {
                    cnm = Factor[index];
                    index++;

                    snm = 0d;
                    if (j != 0)
                    {
                        snm = Factor[index];
                        index++;
                    }
                    content += string.Format("{0} {1} {2} {3}\n",i,j,cnm,snm);
                }
            }

            File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}
