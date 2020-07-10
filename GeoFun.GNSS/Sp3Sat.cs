using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    ///一颗卫星的精密星历
    public struct SP3Sat
    {
        public string Prn;

        /// <summary>
        /// 数据类型
        /// </summary>
        public char Type;

        private double[] values;
        public double[] Pos
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }
        public double[] Vel
        {
            get
            {
                return values;
            }
            set
            {
                values = value;
            }
        }

        private double[] std;
        public double[] Std
        {
            get
            {
                return std;
            }
            set
            {
                std = value;
            }
        }
        private double[] cov;
        public double[] Cov
        {
            get
            {
                return cov;
            }
            set
            {
                cov = value;
            }
        }

        public double X
        {
            get
            {
                return values[0];
            }
            set
            {
                values[0] = value;
            }
        }
        public double Y
        {
            get
            {
                return values[1];
            }
            set
            {
                values[1] = value;
            }
        }
        public double Z
        {
            get
            {
                return values[2];
            }
            set
            {
                values[2] = value;
            }
        }
        public double C
        {
            get
            {
                return values[3];
            }
            set
            {
                values[3] = value;
            }
        }

        ///// <summary>
        ///// /satellite position/clock (ecef) (m|s) 卫星位置
        ///// </summary>
        //public double[] pos;
        ///// <summary>
        ///// satellite position/clock std (m|s)
        ///// </summary>
        //public double[] posStd;
        ///// <summary>
        ///// satellite position covariance (m^2)
        ///// </summary>
        //public double[] posCov;

        ///// <summary>
        ///// satellite velocity/clk-rate (m/s|s/s)
        ///// </summary>
        //public double[] vel;
        ///// <summary>
        ///// satellite velocity/clk-rate std (m/s|s/s)
        ///// </summary>
        //public double[] velStd;
        ///// <summary>
        /////  satellite velocity covariance (m^2)
        ///// </summary>
        //public double[] velCov;

        public static SP3Sat New()
        {
            SP3Sat sat = new SP3Sat();
            sat.values = new double[] { 0d, 0d, 0d, 0d };
            sat.std = new double[] { 0d, 0d, 0d, 0d };
            sat.cov = new double[] { 0d, 0d, 0d };
            return sat;
        }
    }
}