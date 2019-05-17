using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun.CoordinateSystem;

namespace GeoFun
{
    /// <summary>
    /// 椭球结构体
    /// </summary>
    public class Ellipsoid : IFMEEllipsoid
    {
        public static readonly string ESRI_STR =
            "SPHEROID[\"{0}\",{1},{2}]";

        private static readonly string FME_STR =
            "ELLIPSOID_DEF {0}               \\"
            + "\r\nDESC_NM \"Test Ellipsoid\"  \\"
            + "\r\nSOURCE \"PowerMap\"         \\"
            + "\r\nE_RAD {1}                   \\"
            + "\r\nP_RAD {2}";

        /// <summary>
        /// 椭球名称
        /// </summary>)
        public string Name { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">长半轴(米)</param>
        /// <param name="f">扁率倒数</param>
        public Ellipsoid(double a = 6378137d, double f = 298.257222101, string name = "")
        {
            A = a;
            F = f;
            Name = name;
            Alpha = 1 / F;
        }

        public Ellipsoid(Ellipsoid ell) : this(ell.A, ell.F)
        {

        }

        public static readonly Ellipsoid ELLIP_CGCS2000 = new Ellipsoid(a: 6378137d, f: 298.257222101, name: "CGCS2000")
        {
            isArcGIS = true,
            arcgisName = "CGCS2000",
            Name =  "CGCS2000",
            isFME = true,
            fmeName = "CGCS2000",
        };
        public static readonly Ellipsoid ELLIP_XIAN80 = new Ellipsoid(a: 6378140d, f: 298.257, name: "XIAN80")
        {
            isArcGIS = true,
            arcgisName = "Xian_1980",
            Name = "Xian_1980",
            isFME = true,
            fmeName = "Xian80",
        };
        public static readonly Ellipsoid ELLIP_BJ54 = new Ellipsoid(a: 6378245d, f: 298.3, name: "BJ54")
        {
            isArcGIS = true,
            arcgisName = "Krasovsky_1940",
            Name = "Krasovsky_1940",
            isFME = true,
            fmeName = "KRASOV",
        };

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
            set
            {
                F = A / (A - value);
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

        private bool isArcGIS = false;
        /// <summary>
        /// 是否为ArcGIS内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsArcGIS
        {
            get
            {
                return isArcGIS;
            }
            set
            {
                isArcGIS = value;
            }
        }

        private bool isFME = false;
        /// <summary>
        /// 是否为FME内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsFME
        {
            get
            {
                return isFME;
            }
            set
            {
                isFME = value;
            }
        }

        private string arcgisName = "";
        /// <summary>
        /// 在ArcGIS中显示的名称
        /// </summary>
        /// <returns></returns>
        public string ArcGISName
        {
            get
            {
                return arcgisName;
            }
            set
            {
                arcgisName = value;
            }
        }

        private string fmeName = "";
        /// <summary>
        /// 在FME中显示的名称
        /// </summary>
        /// <returns></returns>
        public string FMEName
        {
            get
            {
                return fmeName;
            }
            set
            {
                fmeName = value;
            }
        }

        public string Description { get; set; } = "";
        public string Source { get; set; } = "";

        public string ToESRIString()
        {
            return string.Format(ESRI_STR, Name, A, F);
        }
        public string ToFMEString()
        {
            return string.Format(FME_STR, Name, A, B);
        }

        public static bool operator ==(Ellipsoid ell1, Ellipsoid ell2)
        {
            if (ell1 is null && ell2 is null) return true;
            if (ell1 is null && ell2 != null) return false;
            if (ell1 != null && ell2 is null) return false;

            if (ell1.Name == ell2.Name) return true;
            if (ell1.FMEName == ell2.FMEName) return true;
            if (ell1.ArcGISName == ell2.ArcGISName) return true;

            return Math.Abs(ell1.A - ell2.A) < 1e-14 && Math.Abs(ell1.F - ell2.F) < 1e-14;
        }

        public static bool operator !=(Ellipsoid ell1, Ellipsoid ell2)
        {
            if (ell1 is null && ell2 is null) return false;
            if (ell1 is null && ell2 != null) return true;
            if (ell1 != null && ell2 is null) return true;

            if (ell1.Name == ell2.Name) return false;
            if (ell1.FMEName == ell2.FMEName) return false;
            if (ell1.ArcGISName == ell2.ArcGISName) return false;

            return Math.Abs(ell1.A - ell2.A) > 1e-14 || Math.Abs(ell1.F - ell2.F) > 1e-14;
        }
    }
}
