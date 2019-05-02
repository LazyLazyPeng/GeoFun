using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class GeographicSystem : IGeographicSystem
    {
        public static readonly string PRJ_STR = "GEOGCS[\"{0}\",DATUM[\"{1}\",SPHEROID[\"{2}\",{3},{4}]],PRIMEM[\"Greenwich\",0.0],UNIT[\"Degree\",0.0174532925199433]]";

        public static readonly string FME_STR = "\r\nCOORDINATE_SYSTEM_DEF {0} \\"
            + "\r\nDESC_NM  \"Test Coordinate System\"  \\"
            + "\r\nDT_NAME {1}                \\"
            + "\r\nSOURCE  \"PowerMap\"       \\"
            + "\r\nGROUP: LL   \\"
            + "\r\nMAP_SCL: 1  \\"
            + "\r\nMAX_LAT: 55.583333333333336 \\"
            + "\r\nMAX_LNG: 136.85  \\"
            + "\r\nMIN_LAT: 16.033333333333335 \\"
            + "\r\nMIN_LNG: 71.666666666666667 \\"
            + "\r\nPROJ: LL \\"
            + "\r\nQUAD: 1  \\"
            + "\r\nSCL_RED: 1    \\"
            + "\r\nUNIT: Degree  \\"
            + "\r\nZERO_X: 1e-12 \\"
            + "\r\nZERO_Y: 1e-12";

        public static readonly Dictionary<string, string> FME_NAME = new Dictionary<string, string>
        {
            {"BJ54","Beijing1954/a.LL" },
            {"XIAN80","Xian80.LL" },
            {"CGCS2000","LL.China_2000_FME"}
        };

        /// <summary>
        /// 基准
        /// </summary>
        public Datum Datum { get; set; } = new Datum();

        /// <summary>
        /// 参考椭球
        /// </summary>
        public Ellipsoid Ellipsoid
        {
            get
            {
                return Datum.Ellipsoid;
            }
            set
            {
                Datum.Ellipsoid = value;
            }
        }

        /// <summary>
        /// 坐标系类型
        /// </summary>
        public enumCSType CSType { get; } = enumCSType.Geographic;

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 是否是ArcGIS内置
        /// </summary>
        public bool IsArcGIS { get; set; } = false;

        /// <summary>
        /// ArcGIS内置名称
        /// </summary>
        public string ArcGISName { get; set; } = "";
        /// <summary>
        /// ArcGIS python 坐标系名称
        /// </summary>
        public string ArcGISPyName { get; set; } = "";

        /// <summary>
        /// 是否是FME内置
        /// </summary>
        public bool IsFME { get; set; } = false;

        private string fmeName = "";
        /// <summary>
        /// FME内置名称
        /// </summary>
        public string FMEName
        {
            get
            {
                if (FME_NAME.ContainsKey(Ellipsoid.Name))
                {
                    return FME_NAME[Ellipsoid.Name];
                }
                else
                {
                    return "UserDefine";
                }
            }
            set
            {
                fmeName = value;
            }
        }

        /// <summary>
        /// 转换到ESRI字符串
        /// </summary>
        /// <returns></returns>
        public string ToESRIString()
        {
            return "";
        }

        /// <summary>
        /// 转换到FME自定义坐标系
        /// </summary>
        /// <returns></returns>
        public string ToFMEString()
        {
            return string.Format(FME_STR,Name,Datum.Name);
        }

        override
        public string ToString()
        {
            string pjstr = "|" + Name
            + "|" + Ellipsoid.Name
            + "|" + Ellipsoid.A.ToString()
            + "|" + Ellipsoid.F
            + "|0|0|0|0|0";

            return pjstr;
        }

        /// <summary>
        /// 将坐标系输出到prj文件
        /// </summary>
        /// <param name="path">输出路径,以.prj结尾</param>
        /// <returns></returns>
        public void WritePrj(string path)
        {
        }
    }
}
