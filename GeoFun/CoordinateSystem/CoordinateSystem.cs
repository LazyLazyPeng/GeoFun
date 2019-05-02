using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GeoFun.CoordinateSystem
{
    public static class CoordinateSystem
    {
        public static readonly string ESRI_BAND0 = "{0}_3_Degree_GK_CM_{1}E";
        public static readonly string ESRI_BAND3 = "{0}_3_Degree_GK_Zone_{1}";
        public static readonly string ESRI_BAND6 = "{0}_GK_Zone_{1}";

        public static readonly string FME_BAND0 = "{0}.GK3d/CM-{1}E";
        public static readonly string FME_BAND3 = "{0}.GK3d-{1}";
        public static readonly string FME_BAND6 = "{0}.GK-{1}";

        public static Dictionary<string, string> ESRI_CS_HEADERS = new Dictionary<string, string>
        {
            {"Beijing","Beijing_1954" },
            {"Xian","Xian_1980" },
            {"CGCS2000","CGCS2000" }
        };

        public static Dictionary<string, string> FME_CS_HEADERS = new Dictionary<string, string>
        {
            {"Beijing","Bjing54/a." },
            {"Xian","Xian80" },
            {"CGCS2000","CGCS2000" },
         };

        public static readonly IGeographicSystem GCS_54 = new GeographicSystem
        {
            IsArcGIS = true,
            IsFME = true,
            Name = "Beijing1954/a.LL",
            ArcGISName = "GCS_Beijing_1954",
            ArcGISPyName = "Beijing 1954",
            FMEName = "Beijing1954/a.LL",
            Datum = Datum.BEIJING54
        };
        public static readonly IGeographicSystem GCS_80 = new GeographicSystem
        {
            IsArcGIS = true,
            IsFME = true,
            Name = "GCS_Xian_1980",
            ArcGISName = "GCS_Xian_1980",
            ArcGISPyName= "Xian 1980",
            FMEName = "Xian80.LL",
            Datum = Datum.XIAN80
        };
        public static readonly IGeographicSystem GCS_2000 = new GeographicSystem
        {
            IsArcGIS = true,
            IsFME = true,
            Name = "GCS_China_Geodetic_Coordinate_System_2000",
            ArcGISName = "GCS_China_Geodetic_Coordinate_System_2000",
            ArcGISPyName= "China Geodetic Coordinate System 2000",
            FMEName = "LL.China_2000_FME",
            Datum = Datum.CGCS2000,
        };

        public static string GetFMEName(string ellName,enumBandType bandType,int bandNum,int centerL)
        {
            if (ellName.StartsWith("Beijing"))
            {
                switch (bandType)
                {
                    case enumBandType.Band0:
                        return string.Format("Bjing54/a.GK3d/CM-{0}E", centerL);
                    case enumBandType.Band3:
                        return string.Format("Beijing1954/a.GK3d-{0}", bandNum);
                    case enumBandType.Band6:
                        return string.Format("Beijing1954/a.GK-{0}", bandNum);
                    default:
                        return null;
                }
            }
            else if(ellName.StartsWith("Xian"))
            {
                switch (bandType)
                {
                    case enumBandType.Band0:
                        return string.Format("Xian80.GK3d/CM-{0}E", centerL);
                    case enumBandType.Band3:
                        return string.Format("Xian80.GK3d-{0}",bandNum);
                    case enumBandType.Band6:
                        return string.Format("Xian80.GK-{0}",bandNum);
                    default:
                        return null;
                }

            }
            else if(ellName.StartsWith("CGCS2000"))
            {
                switch (bandType)
                {
                    case enumBandType.Band0:
                        return string.Format("CGCS2000/GK3d-{0}E_FME",centerL);
                    case enumBandType.Band3:
                        return string.Format("CGCS2000/GK3d-{0}_FME",bandNum);
                    case enumBandType.Band6:
                        return string.Format("CGCS2000/GK6d-{0}_FME",bandNum);
                    default:
                        return null;
                }

            }
            else
            {
                return null;
            }
        }

        public static ICoordinateSystem GetCSFromPyName(string name)
        {
            string[] csHeaders = { "Beijing 1954", "Xian 1980", "CGCS2000" };
            Dictionary<string, IGeographicSystem> geocs = new Dictionary<string, IGeographicSystem>
            {
                {"Beijing",GCS_54 },
                {"Xian",GCS_80 },
                {"CGCS2000",GCS_2000 },
            };

            if(name == "Beijing 1954")
            {
                return GCS_54;
            }
            else if(name == "Xian 1980")
            {
                return GCS_80;
            }
            else if(name == "China Geodetic Coordinate System 2000")
            {
                return GCS_2000;
            }
            else if (name.Contains("3 Degree GK CM"))
            {
                if (!name.StartsWith(csHeaders[0]) && !name.StartsWith(csHeaders[1]) && !name.StartsWith(csHeaders[2]))
                {
                    return null;
                }

                string[] segs = name.Split();
                string lastSeg = segs[segs.Length - 1];
                if (lastSeg is null || string.IsNullOrWhiteSpace(lastSeg)) return null;

                int centerL;
                try
                {
                    centerL = int.Parse(lastSeg.Substring(0, lastSeg.Length - 1));
                }
                catch
                {
                    return null;
                }

                string arcgisName = name.Replace(" ", "_");
                string fmeName = GetFMEName(segs[0], enumBandType.Band0, 0, centerL);

                return new ProjectionSystem
                {
                    IsArcGIS = true,
                    IsFME = true,
                    ArcGISName = arcgisName,
                    ArcGISPyName = name,
                    FMEName = fmeName,
                    L0 = centerL * Angle.D2R,
                    XOff = 500000d,
                    YOff = 0,
                    GeoCS = geocs[segs[0]],
                };
            }

            else if (name.Contains("3 Degree GK Zone"))
            {
                if (!name.StartsWith(csHeaders[0]) && !name.StartsWith(csHeaders[1]) && !name.StartsWith(csHeaders[2]))
                {
                    return null;
                }

                string[] segs = name.Split();
                string lastSeg = segs[segs.Length - 1];
                if (lastSeg is null || string.IsNullOrWhiteSpace(lastSeg)) return null;

                int bandNum;
                try
                {
                    bandNum = int.Parse(lastSeg);
                }
                catch
                {
                    return null;
                }

                string arcgisName = name.Replace(" ", "_");
                string fmeName = GetFMEName(segs[0], enumBandType.Band3, bandNum, 0);

                return new ProjectionSystem
                {
                    IsArcGIS = true,
                    IsFME = true,
                    ArcGISName = arcgisName,
                    ArcGISPyName = name,
                    FMEName = fmeName,
                    L0 = bandNum * 3 * Angle.D2R,
                    XOff = bandNum * 1e6 + 5e5,
                    YOff = 0,
                    GeoCS = geocs[segs[0]],
                };
            }

            else if (name.Contains("GK Zone"))
            {
                if (!name.StartsWith(csHeaders[0]) && !name.StartsWith(csHeaders[1]) && !name.StartsWith(csHeaders[2]))
                {
                    return null;
                }

                string[] segs = name.Split();
                string lastSeg = segs[segs.Length - 1];
                if (lastSeg is null || string.IsNullOrWhiteSpace(lastSeg)) return null;

                int bandNum;
                try
                {
                    bandNum = int.Parse(lastSeg);
                }
                catch
                {
                    return null;
                }

                string arcgisName = name.Replace(" ", "_");
                string fmeName = GetFMEName(segs[0], enumBandType.Band6, bandNum, 0);

                return new ProjectionSystem
                {
                    IsArcGIS = true,
                    IsFME = true,
                    ArcGISName = arcgisName,
                    ArcGISPyName = name,
                    FMEName = fmeName,
                    L0 = (bandNum * 6 - 3) * Angle.D2R,
                    XOff = bandNum * 1e6 + 5e5,
                    YOff = 0,
                    GeoCS = geocs[segs[0]],
                };

            }

            else
            {
                return null;
            }
        }

        public static ICoordinateSystem GetCSFromName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            name = name.Trim();

            if (name.StartsWith("GCS"))
            {
                if (name.Contains("1954")) return GCS_54;
                else if (name.Contains("1980")) return GCS_80;
                else if (name.Contains("2000")) return GCS_2000;
            }

            else
            {
                string[] segs = name.Split('_');
                string ellName = segs[0];

                if (name.Contains("3_Degree_GK_CM"))
                {
                    string centerL = segs[segs.Length - 1];

                    return GetProjCS(ellName, 0, 0, centerL);
                }
                else if (name.Contains("3_Degree_GK_Zone"))
                {
                    int bandNum = int.Parse(segs[segs.Length - 1]);
                    return GetProjCS(ellName, 3, bandNum, "");
                }
                else if (name.Contains("GK_Zone"))
                {
                    int bandNum = int.Parse(segs[segs.Length - 1]);
                    return GetProjCS(ellName, 6, bandNum, "");
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public static IProjectionSystem GetProjCS(string ellName, int bandType, int bandNum, string centerL)
        {
            Datum dat = null;
            switch (ellName)
            {
                case "Beijing":
                    dat = Datum.CGCS2000;
                    break;
                case "Xian":
                    dat = Datum.XIAN80;
                    break;
                case "CGCS2000":
                    dat = Datum.CGCS2000;
                    break;
                default:
                    break;
            }

            if (centerL.EndsWith("E")) centerL = centerL.Substring(centerL.Length - 1);

            if (dat is null) return null;

            string esriName = "";
            string fmeName = "";
            switch (bandType)
            {
                case 3:
                    esriName = string.Format(ESRI_BAND3, ESRI_CS_HEADERS[ellName], bandNum);
                    fmeName = string.Format(FME_BAND3, FME_CS_HEADERS[ellName], bandNum);
                    break;
                case 6:
                    esriName = string.Format(ESRI_BAND6, ESRI_CS_HEADERS[ellName], bandNum);
                    fmeName = string.Format(FME_BAND6, FME_CS_HEADERS[ellName], bandNum);
                    break;
                default:
                    esriName = string.Format(ESRI_BAND0, ESRI_CS_HEADERS[ellName], centerL);
                    fmeName = string.Format(FME_BAND0, FME_CS_HEADERS[ellName], centerL);
                    break;
            }

            IProjectionSystem pcs = new ProjectionSystem
            {
                IsArcGIS = true,
                IsFME = true,
                ArcGISName = esriName,
                FMEName = fmeName,
                Datum = dat,
                L0 = int.Parse(centerL) * Angle.D2R,
                XOff = bandNum * 1e6 + 5e6,
            };

            return pcs;
        }
    }
}
