using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    /// <summary>
    /// 椭球结构体
    /// </summary>
    public class Ellipsoid
    {
        /// <summary>
        /// 椭球名称
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">长半轴(米)</param>
        /// <param name="f">扁率倒数</param>
        public Ellipsoid(double a = 6378137d, double f = 298.257222101,string name="")
        {
            A = a;
            F = f;
            Name = name;
            Alpha = 1 / F;
        }

        public Ellipsoid(Ellipsoid ell) : this(ell.A, ell.F)
        {

        }

        public static readonly Ellipsoid ELLIP_CGCS2000 = new Ellipsoid(a: 6378137d, f: 298.257222101,name:"CGCS2000");
        public static readonly Ellipsoid ELLIP_XIAN80 = new Ellipsoid(a: 6378140d, f: 298.257,name:"XIAN80");
        public static readonly Ellipsoid ELLIP_BJ54 = new Ellipsoid(a: 6378245d, f: 298.3,name:"BJ54");

        public static readonly Ellipsoid ELLIP_WGS84 = new Ellipsoid(a: 6378137d, f: 298.257223563);

        /// <summary>
        /// 椭球长半轴
        /// </summary>
        public double A { get; set; }

        /// <summary>
        /// 椭球扁率
        /// </summary>
        public double Alpha { get; set; }

        /// <summary>
        /// 扁率倒数
        /// </summary>
        public double F
        {
            get
            {
                if (Alpha != 0d) return 1 / Alpha;
                else return 0d;
            }
            set
            {
                if (value == 0d) Alpha = 0d;
                else Alpha = 1 / value;
            }
        }

        /// <summary>
        /// 椭球短半轴
        /// </summary>
        public double B
        {
            get
            {
                return A - Alpha * A;
            }
        }

        /// <summary>
        /// 第一偏心率的平方
        /// </summary>
        public double E1
        {
            get
            {
                return 2 * Alpha - Alpha * Alpha;
            }
        }

        /// <summary>
        /// 第二偏心率的平方
        /// </summary>
        public double E2
        {
            get
            {
                return E1 / (1 - E1);
            }
        }

        public double c
        {
            get
            {
                return A * A / B;
            }
        }

        /// <summary>
        /// t
        /// </summary>
        /// <param name="b">纬度(弧度)</param>
        /// <returns></returns>
        public double t(double b)
        {
            return Math.Tan(b);
        }

        /// <summary>
        /// eta
        /// </summary>
        ///<param name="b">纬度(弧度)</param>
        public double eta(double b)
        {
            return E2 * Math.Pow(Math.Cos(b), 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="b">弧度</param>
        /// <returns></returns>
        public double W(double b)
        {
            return Math.Sqrt(1 - E1 * Math.Sin(b) * Math.Sin(b));
        }

        public double V(double b)
        {
            return Math.Sqrt(1 + E2 * Math.Cos(b) * Math.Cos(b));
        }

        /// <summary> 
        /// 子午圈半径(m)
        /// </summary>
        /// <param name="b">弧度</param>
        /// <returns></returns>
        public double M(double b)
        {
            return A * (1 - E1) / Math.Pow(W(b), 3);
        }

        /// <summary>
        /// 卯酉圈半径 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public double N(double b)
        {
            return A / W(b);
        }

        /// <summary>
        /// 根据纬度和投影抬高对纬度进行膨胀
        /// </summary>
        /// <param name="B">膨胀点所在的纬度(度)</param>
        /// <param name="dh">投影抬高</param>
        public void Expansion(double B, double dh)
        {
            A = A + Math.Sqrt(1 - E1 * Math.Pow(Math.Sin(B), 2)) * dh;
        }

        public static bool operator ==(Ellipsoid ell1, Ellipsoid ell2)
        {
            try
            {
                if (!DoubleHelper.Equals(ell1.A, ell2.A)) return false;
                if (!DoubleHelper.Equals(ell1.F, ell2.F)) return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool operator !=(Ellipsoid ell1, Ellipsoid ell2)
        {
            try
            {
                if (DoubleHelper.Equals(ell1.A, ell2.A) &&
                    DoubleHelper.Equals(ell1.F, ell2.F))
                    return false;

                return true;
            }
            catch
            {
                return true;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Ellipsoid)
            {
                Ellipsoid ell2 = obj as Ellipsoid;

                if (!DoubleHelper.Equals(A, ell2.A)) return false;
                if (!DoubleHelper.Equals(F, ell2.F)) return false;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
