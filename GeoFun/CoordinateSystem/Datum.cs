using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class Datum
    {
        public static readonly string ESRI_STR =
            "DATUM[\"{0}\",{1}]";

        public static readonly string FME_STR =
            "DATUM_DEF {0}               \\"
            + "\r\nDESC_NM \"Datum\"        \\"
            + "\r\nSOURCE \"PowerMap\"      \\"
            + "\r\nELLIPSOID {1}            \\"
            + "\r\nUSE 7PARAMETER";

        public static readonly Datum CGCS2000 = new Datum()
        {
            Ellipsoid = Ellipsoid.ELLIP_CGCS2000,
            Name = "CGCS2000",
            IsArcGIS = true,
            IsFME = true,
            ArcGISName = "D_China_2000",
            FMEName = "China_2000_FME",
        };
        public static readonly Datum XIAN80 = new Datum()
        {
            Ellipsoid = Ellipsoid.ELLIP_XIAN80,
            Name = "D_Xian_1980",
            IsArcGIS = true,
            IsFME = true,
            ArcGISName = "D_Xian_1980",
            FMEName = "Xian80",
        };
        public static readonly Datum BEIJING54 = new Datum()
        {
            Ellipsoid = Ellipsoid.ELLIP_BJ54,
            Name = "D_Beijing_1954",
            IsArcGIS = true,
            IsFME = true,
            ArcGISName = "D_Beijing_1954",
            FMEName = "Beijing1954/a",
        };

        public string Name { get; set; } = "UserDefine";
        public Ellipsoid Ellipsoid { get; set; } = new Ellipsoid();

        /// <summary>
        /// 是否为ArcGIS内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsArcGIS { get; set; }
        /// <summary>
        /// 是否为FME内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsFME { get; set; }

        /// <summary>
        /// 在ArcGIS中显示的名称
        /// </summary>
        /// <returns></returns>
        public string ArcGISName { get; set; }

        /// <summary>
        /// 在FME中显示的名称
        /// </summary>
        /// <returns></returns>
        public string FMEName { get; set; }

        /// <summary>
        /// 转换为Arcgis字符串
        /// </summary>
        /// <returns></returns>
        public string ToESRIString()
        {
            return string.Format(ESRI_STR, ArcGISName, Ellipsoid.ToESRIString());
        }

        /// <summary>
        /// 转换为FME坐标系字符串
        /// </summary>
        /// <returns></returns>
        public string ToFMEString()
        {
            return string.Format(FME_STR, FMEName, Ellipsoid.Name);
        }

        public static bool operator ==(Datum dat1, Datum dat2)
        {
            if (dat1 is null && dat2 is null) return true;
            if (dat1 is null && dat2 != null) return false;
            if (dat1 != null && dat2 is null) return false;

            if (dat1.Name == dat2.Name) return true;
            if (dat1.FMEName == dat2.FMEName) return true;
            if (dat1.ArcGISName == dat2.ArcGISName) return true;

            return dat1.Ellipsoid == dat2.Ellipsoid;
        }

        public static bool operator !=(Datum dat1, Datum dat2)
        {
            if (dat1 is null && dat2 is null) return false;
            if (dat1 is null && dat2 != null) return true;
            if (dat1 != null && dat2 is null) return true;

            if (dat1.Name == dat2.Name) return false;
            if (dat1.FMEName == dat2.FMEName) return false;
            if (dat1.ArcGISName == dat2.ArcGISName) return false;

            return dat1.Ellipsoid != dat2.Ellipsoid;
        }
    }
}
