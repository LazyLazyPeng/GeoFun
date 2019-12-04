using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;

namespace GeoFun.MathGeo
{
    /// <summary>
    /// 球谐函数模型
    /// </summary>
    public class SphericalHamonicIonoModel
    {
        public uint Degree { get; set; } = 0;
        public uint Order { get; set; } = 0;

        /// <summary>
        /// 模型参数,完全归一化,一维，排列顺序为 A00 B00 A10 B10 ......
        /// </summary>
        public Vector<double> Factor = null;

        /// <summary>
        /// 根据提供的数据采用最小二乘拟合球谐函数模型
        /// </summary>
        /// <param name="degree">阶</param>
        /// <param name="order">次</param>
        /// <param name="lat">纬度(弧度)</param>
        /// <param name="lon">经度(弧度)</param>
        /// <param name="vtec">VTEC(TECU)</param>
        /// <returns></returns>
        public static SphericalHamonicIonoModel CalculateModel(uint degree, uint order,
            List<double> lat, List<double> lon, List<double> vtec)
        {
            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (lat is null || lon is null || vtec is null) return null;

            //// 检测观测值个数是否满足要求
            int minCount = Math.Min(Math.Min(lat.Count, lon.Count), vtec.Count);
            uint paraNum = (degree + 1) * (order + 1) * 2;
            if (minCount == 0) return null;
            if (minCount < paraNum)
            {
                throw new Exception("球谐函数,观测值个数无法满足建模要求");
            }

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(minCount, (int)paraNum);
            Vector<double> L = new DenseVector(minCount);
            for (int i = 0; i <= minCount; i++)
            {
                for (int n = 0; n <= degree; n++)
                {
                    int start = n * n;
                    for (int m = 0; m <= n; m++)
                    {
                        double Pnm = Legendre.lpmv(n, m, Math.PI / 2d - lat[i]);
                        B[i, start + m * 2] = Pnm * Math.Cos(m * lon[i]);
                        B[i, start + m * 2 + 1] = Pnm * Math.Sin(m * lat[i]);
                    }
                }

                L[i] = vtec[i];
            }

            //// 求解
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            SphericalHamonicIonoModel model = new SphericalHamonicIonoModel();
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

            Vector<double> B = new DenseVector(((int)Degree + 1) * ((int)Order + 1) * 2);
            for (int n = 0; n <= Degree; n++)
            {
                int start = n * n;
                for (int m = 0; m <= n; m++)
                {
                    double Pnm = Legendre.lpmv(n, m, Math.PI / 2d - lat);
                    B[start + m * 2] = Math.Cos(m * lon) * Pnm;
                    B[start + m * 2] = Math.Cos(m * lon) * Pnm;
                }
            }

            result = B.DotProduct(Factor);
            return result;
        }
    }
}
