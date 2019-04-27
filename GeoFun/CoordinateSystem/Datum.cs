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

        public string Name { get; set; } = "D_China_2000";
        public Ellipsoid Ellipsoid { get; set; } = new Ellipsoid();

        /// <summary>
        /// 是否为ArcGIS内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsArcGIS()
        {
            return Ellipsoid.IsArcGIS;
        }
        /// <summary>
        /// 是否为FME内置椭球
        /// </summary>
        /// <returns></returns>
        public bool IsFME()
        {
            return Ellipsoid.IsFME;
        }

        /// <summary>
        /// 在ArcGIS中显示的名称
        /// </summary>
        /// <returns></returns>
        public string ArcGISName()
        {
            return "";
        }
        /// <summary>
        /// 在FME中显示的名称
        /// </summary>
        /// <returns></returns>
        public string FMEName()
        {
            return "";
        }

        /// <summary>
        /// 转换为Arcgis字符串
        /// </summary>
        /// <returns></returns>
        public string ToESRIString()
        {
            return string.Format(ESRI_STR,Name,Ellipsoid.ToESRIString());
        }

        /// <summary>
        /// 转换为FME坐标系字符串
        /// </summary>
        /// <returns></returns>
        public string ToFMEString()
        {
            return string.Format(FME_STR,Name,Ellipsoid.Name);
        }
    }
}
