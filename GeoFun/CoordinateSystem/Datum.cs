using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class Datum
    {
        public static readonly string FME_STR = 
            "DATUM_DEF {0}               \\"
            + "\r\nDESC_NM \"Datum\"        \\" 
            + "\r\nSOURCE \"PowerMap\"      \\"
            + "\r\nELLIPSOID {1}            \\"
            + "\r\nUSE 7PARAMETER";

        public string Name { get; set; }
        public Ellipsoid Ellipsoid { get; set; }

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
