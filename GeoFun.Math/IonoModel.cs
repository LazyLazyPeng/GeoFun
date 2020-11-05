using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GeoFun.MathUtils
{
    public class IonoModel
    {
        public virtual void Calculate(double lon, double lat)
        {

        }

        public static bool CalSphericalHarmonicModel(
            int degree, int order,
            List<string> stations,
            List<LinkedList<int>> prn,
            List<LinkedList<double>> lat,
            List<LinkedList<double>> lon,
            List<LinkedList<double>> sp4,
            List<LinkedList<double>> ele,
            out SphericalHarmonicIonoModel spm,
            out Dictionary<string, double> receiverDCB,
            out Dictionary<string, double> satelliteDCB)
        {
            spm = null;
            receiverDCB = new Dictionary<string, double>();
            satelliteDCB = new Dictionary<string, double>();

            if (degree != order) throw new Exception("球谐函数阶数!=次数,暂时无法计算");
            if (sp4 is null || sp4.Count() <= 0) return false;

            // 获取到本次观测中的所有卫星编号，以估计卫星DCB
            // hashset是非重复集合，自动剔除重复元素
            HashSet<int> prns = new HashSet<int>();
            for (int i = 0; i < prn.Count(); i++)
            {
                foreach (var p in prn[i])
                {
                    prns.Add(p);
                }
            }

            // 测站数量
            int stationNum = stations.Count();
            // 卫星数量
            int satelliteNum = prns.Count();
            // 观测值数量
            int obsNum = (from item in sp4
                          select item.Count()).Sum();

            // 球谐系数参数个数
            int spmParaNum = (degree + 1) * (degree + 1) * 2 - (degree + 1);
            // 接收机DCB参数个数
            int recParaNum = stationNum;
            // 卫星DCB参数个数
            int satParaNum = satelliteNum;

            // 参数个数
            int paraNum = spmParaNum + stationNum + satelliteNum;
            if (obsNum < paraNum)
            {
                throw new Exception("观测值个数无法满足建模要求!");
            }

            // 球谐系数起始索引位置
            int spmStart = 0;
            // 接收机DCB系数起始位置
            int recStart = spmParaNum;
            // 卫星DCB系数起始位置
            int satStart = spmParaNum + recParaNum;

            //// 设计矩阵
            Matrix<double> B = new DenseMatrix(obsNum + 1, (int)paraNum);
            Vector<double> L = new DenseVector(obsNum + 1);
            int obsIndex = 0;
            int satIndex = 0;
            int recIndex = 0;
            for (int i = 0; i < sp4.Count(); i++)
            {
                for(int j =0; j < paraNum; j++)
                {
                    B[i, j] = 0d;
                }

                recIndex = i;

                var sp4i = sp4[i].First;
                var lati = lat[i].First;
                var loni = lon[i].First;
                var prni = prn[i].First;
                var elei = ele[i].First;
                for (int j = 0; j < sp4[i].Count; j++)
                {
                    satIndex = prni.Value;

                    double sp4j = sp4i.Value;
                    double latj = lati.Value;
                    double lonj = loni.Value;
                    double elej = elei.Value;
                    double fz = MathHelper.CalIonoFactor(elej);

                    // Anm，Bnm系数
                    int col = 0;
                    for (int n = 0; n <= degree; n++)
                    {
                        for (int m = 0; m <= n; m++)
                        {
                            double Pnm = Legendre.lpmv(n, m, System.Math.PI / 2d - latj);
                            B[obsIndex, col] = Pnm * System.Math.Cos(m * lonj) * fz;
                            col++;
                            // Bn0不需要估计,因为sin(0)乘任何数都是0
                            if (m == 0) continue;
                            B[obsIndex, col] = Pnm * System.Math.Sin(m * lonj) * fz;
                            col++;
                        }
                    }

                    // 接收机DCB系数
                    B[obsIndex, recStart + recIndex] = 9.52437;
                    // 卫星DCB系数
                    B[obsIndex, satStart + satIndex - 1] = -9.52437;

                    L[obsIndex] = sp4j * 9.52437;

                    obsIndex++;

                    sp4i = sp4i.Next;
                    lati = lati.Next;
                    loni = loni.Next;
                    prni = prni.Next;
                    elei = elei.Next;
                }
            }

            // 加入卫星DCB之和为0的约束
            for(int i =0; i < paraNum; i++)
            {
                B[obsNum, i] = 0d;
            }
            foreach (var p in prns)
            {
                B[obsNum, satStart + p - 1] = 1;
            }

            ////// 求解
            var btb = B.Transpose() * B;
            var btbi = btb.Inverse();
            Vector<double> x = (B.Transpose() * B).Inverse() * (B.Transpose() * L);

            // 提取球谐系数
            Vector<double> spmFactor = new DenseVector(spmParaNum);
            for (int i = 0; i < spmParaNum; i++)
            {
                spmFactor[i] = x[i];
            }
            SphericalHarmonicIonoModel model = new SphericalHarmonicIonoModel();
            model.Degree = degree;
            model.Order = order;
            model.Factor = spmFactor;

            // 提取接收机dcb
            for(int i =0; i < stationNum; i++)
            {
                receiverDCB.Add(stations[i], x[recStart + i]);
            }

            // 提取卫星DCB
            foreach (var p in prns)
            {
                satelliteDCB.Add(string.Format("G{0:00}", p), x[satStart + p - 1]);
            }

            return true;
        }
    }
}
