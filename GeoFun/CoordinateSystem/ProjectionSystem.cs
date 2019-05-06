using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoFun.CoordinateSystem
{
    public class ProjectionSystem : IProjectionSystem
    {
        public static readonly Dictionary<string, Dictionary<enumBandType, string>> FME_NAME = new Dictionary<string, Dictionary<enumBandType, string>>
        {
            {"BJ54",new Dictionary<enumBandType, string>{
                { enumBandType.Band0,"Bjing54/a.GK3d/CM-{0}E" },
                { enumBandType.Band3," Beijing1954/a.GK3d-{0}" },
                { enumBandType.Band6,"Beijing1954/a.GK-{0}" }
             }},
            {"XIAN80",new Dictionary<enumBandType, string>{
                { enumBandType.Band0,"Xian80.GK3d/CM-{0}E" },
                { enumBandType.Band3,"Xian80.GK3d-{0}" },
                { enumBandType.Band6,"Xian80.GK-{0}" }
             } },
            {"CGCS2000",new Dictionary<enumBandType, string>{
                { enumBandType.Band0,"CGCS2000/GK3d-{0}E_FME" },
                { enumBandType.Band3,"CGCS2000/GK3d-{0}_FME" },
                { enumBandType.Band6,"CGCS2000/GK6d-{0}_FME" }
            } },
        };

        public static readonly string ESRI_STR = "PROJCS[\"{0}\",{1},PROJECTION[\"Gauss_Kruger\"],PARAMETER[\"False_Easting\",{2}],PARAMETER[\"False_Northing\",{3}],PARAMETER[\"Central_Meridian\",{4}],PARAMETER[\"Scale_Factor\",1.0],PARAMETER[\"Latitude_Of_Origin\",{5}],UNIT[\"Meter\",1.0]]";

        public static readonly string FME_STR = "\r\nCOORDINATE_SYSTEM_DEF {0} \\"
            + "\r\nDESC_NM  \"CS\"  \\"
            + "\r\nSOURCE  \"PowerMap\"   \\"
            + "\r\nPROJ GAUSSK            \\"
            + "\r\nDT_NAME {1}            \\"
            + "\r\nUNIT METER             \\"
            + "\r\nPARM1   {2:f12}            \\"
            + "\r\nORG_LAT {3}            \\"
            + "\r\nSCL_RED 1.0            \\"
            + "\r\nMAP_SCL 1.0            \\"
            + "\r\nX_OFF   {4}            \\"
            + "\r\nY_OFF   {5}";

        /// <summary>
        /// 坐标系名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 坐标系类型 地理坐标系/投影坐标系
        /// </summary>
        public enumCSType CSType { get; } = enumCSType.Projection;

        /// <summary>
        /// 对应的地理坐标系
        /// </summary>
        public IGeographicSystem GeoCS { get; set; } = new GeographicSystem();

        /// <summary>
        /// 基准
        /// </summary>
        public Datum Datum
        {
            get
            {
                return GeoCS.Datum;
            }
            set
            {
                GeoCS.Datum = value;
            }
        }

        /// <summary>
        /// 中央子午线(弧度)
        /// </summary>
        public double L0
        {
            get
            {
                return (double)CenterMeridian.ARC;
            }
            set
            {
                centerMeridian.ARC = (decimal)value;
            }
        }
        /// <summary>
        /// 投影抬高
        /// </summary>
        public double H0 { get; set; } = 0d;

        /// <summary>
        /// X偏移量(米)
        /// </summary>
        public double XOff { get; set; }
        /// <summary>
        /// Y偏移量(米)
        /// </summary>
        public double YOff { get; set; }

        /// <summary>
        /// 底点纬度
        /// </summary>
        public Angle OriginLat { get; set; } = new Angle();


        /// <summary>
        /// 是否是ArcGIS内置
        /// </summary>
        public bool IsArcGIS { get; set; }
        /// <summary>
        /// 是否是FME内置
        /// </summary>
        public bool IsFME { get; set; }


        /// <summary>
        /// ArcGIS坐标系名称
        /// </summary>
        public string ArcGISName { get; set; }
        /// <summary>
        /// arcgis python坐标系名称
        /// </summary>
        public string ArcGISPyName { get; set; }
        private string fmeName = "";
        /// <summary>
        /// FME坐标系名称
        /// </summary>
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

        private Angle centerMeridian = new Angle();
        public Angle CenterMeridian
        {
            get
            {
                return centerMeridian;
            }
            set
            {
                centerMeridian = value;
            }
        }

        /// <summary>
        /// 分带类型
        /// </summary>
        public enumBandType BandType { get; set; } = enumBandType.Band0;
        /// <summary>
        /// 带号
        /// </summary>
        public int BandNum { get; set; } = 0;

        public Ellipsoid Ellipsoid
        {
            get
            {
                if (GeoCS != null)
                {
                    return GeoCS.Ellipsoid;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (GeoCS != null)
                {
                    GeoCS.Ellipsoid = value;
                }
            }
        }

        /// <summary>
        /// 输出ESRI字符串
        /// </summary>
        /// <returns></returns>
        public string ToESRIString()
        {
            return string.Format(ESRI_STR,Name,GeoCS.ToESRIString(),XOff,YOff,centerMeridian.DD,OriginLat.DD);
        }
        /// <summary>
        /// 输出FME字符串
        /// </summary>
        /// <returns></returns>
        public string ToFMEString()
        {
            return string.Format(FME_STR, Name, GeoCS.Datum.FMEName, centerMeridian.DD, OriginLat.DD, XOff, YOff);
        }

        override
        public string ToString()
        {
            string pjstr = "|" + Name
            + "|" + Ellipsoid.Name
            + "|" + Ellipsoid.A.ToString()
            + "|" + Ellipsoid.F
            + "|" + centerMeridian.DD.ToString()
            + "|" + OriginLat.DD.ToString()
            + "|" + H0
            + "|" + (int)BandType
            + "|" + BandNum;

            return pjstr;
        }

        public void WritePrj(string path)
        {
            File.WriteAllText(path,ToESRIString());
        }

        /// <summary>
        /// 获取FME内置坐标系名称，如果不是内置，则返回默认名称
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public string GetFMENameOrDefault(string def = "UserDefine")
        {
            if (!FME_NAME.ContainsKey(Ellipsoid.Name)) return def;

            int l0 = (int)centerMeridian.DD;
            if (l0 % 3 != 0) return def;

            string csName = "";
            if(BandType == enumBandType.Band0)
            {
                csName = string.Format(FME_NAME[Ellipsoid.Name][BandType], (int)centerMeridian.DD);
            }
            else
            {
                csName = string.Format(FME_NAME[Ellipsoid.Name][BandType], BandNum);
            }

            return csName;
        }
    }
}
