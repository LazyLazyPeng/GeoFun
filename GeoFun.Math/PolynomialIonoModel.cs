using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun.GNSS;

namespace GeoFun.Math
{
    public class PolynomialIonoModel
    {
        /// <summary>
        /// 多项式建模
        /// </summary>
        /// <param name="order">阶数</param>
        /// <param name="l0">建模区域中心点地理经度(弧度)</param>
        /// <param name="b0">建模区域中心点地理纬度(弧度)</param>
        /// <param name="lat">穿刺点纬度(弧度)</param>
        /// <param name="lon">穿刺点经度(弧度)</param>
        /// <param name="epoches">历元</param>
        /// <param name="vtec"></param>
        /// <returns></returns>
        public static PolynomialIonoModel CalculateModel(uint order,
            double l0, double b0,
            List<double> lat, List<double> lon, List<GPST> epoches, List<double> vtec)
        {
            return null;
        }

        public double Calculate(double lat, double lon)
        {
            return 0d;
        }
    }
}
