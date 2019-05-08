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
            ArcGISPyName = "Xian 1980",
            FMEName = "Xian80.LL",
            Datum = Datum.XIAN80
        };
        public static readonly IGeographicSystem GCS_2000 = new GeographicSystem
        {
            IsArcGIS = true,
            IsFME = true,
            Name = "GCS_China_Geodetic_Coordinate_System_2000",
            ArcGISName = "GCS_China_Geodetic_Coordinate_System_2000",
            ArcGISPyName = "China Geodetic Coordinate System 2000",
            FMEName = "LL.China_2000_FME",
            Datum = Datum.CGCS2000,
        };

        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_102E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_102E", ArcGISName = "CGCS2000_3_Degree_GK_CM_102E", FMEName = "CGCS2000/GK3d-102E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_105E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_105E", ArcGISName = "CGCS2000_3_Degree_GK_CM_105E", FMEName = "CGCS2000/GK3d-105E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_108E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_108E", ArcGISName = "CGCS2000_3_Degree_GK_CM_108E", FMEName = "CGCS2000/GK3d-108E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_111E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_111E", ArcGISName = "CGCS2000_3_Degree_GK_CM_111E", FMEName = "CGCS2000/GK3d-111E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_114E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_114E", ArcGISName = "CGCS2000_3_Degree_GK_CM_114E", FMEName = "CGCS2000/GK3d-114E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_117E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_117E", ArcGISName = "CGCS2000_3_Degree_GK_CM_117E", FMEName = "CGCS2000/GK3d-117E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_120E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_120E", ArcGISName = "CGCS2000_3_Degree_GK_CM_120E", FMEName = "CGCS2000/GK3d-120E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_123E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_123E", ArcGISName = "CGCS2000_3_Degree_GK_CM_123E", FMEName = "CGCS2000/GK3d-123E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_126E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_126E", ArcGISName = "CGCS2000_3_Degree_GK_CM_126E", FMEName = "CGCS2000/GK3d-126E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_129E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_129E", ArcGISName = "CGCS2000_3_Degree_GK_CM_129E", FMEName = "CGCS2000/GK3d-129E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_132E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_132E", ArcGISName = "CGCS2000_3_Degree_GK_CM_132E", FMEName = "CGCS2000/GK3d-132E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_135E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_135E", ArcGISName = "CGCS2000_3_Degree_GK_CM_135E", FMEName = "CGCS2000/GK3d-135E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };

        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_102E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_102E", ArcGISName = "Beijing_1954_Degree_GK_CM_102E", FMEName = "Bjing54/a.GK3d/CM-102E", GeoCS = GCS_54, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_105E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_105E", ArcGISName = "Beijing_1954_Degree_GK_CM_105E", FMEName = "Bjing54/a.GK3d/CM-105E", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_108E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_108E", ArcGISName = "Beijing_1954_Degree_GK_CM_108E", FMEName = "Bjing54/a.GK3d/CM-108E", GeoCS = GCS_54, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_111E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_111E", ArcGISName = "Beijing_1954_Degree_GK_CM_111E", FMEName = "Bjing54/a.GK3d/CM-111E", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_114E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_114E", ArcGISName = "Beijing_1954_Degree_GK_CM_114E", FMEName = "Bjing54/a.GK3d/CM-114E", GeoCS = GCS_54, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_117E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_117E", ArcGISName = "Beijing_1954_Degree_GK_CM_117E", FMEName = "Bjing54/a.GK3d/CM-117E", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_120E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_120E", ArcGISName = "Beijing_1954_Degree_GK_CM_120E", FMEName = "Bjing54/a.GK3d/CM-120E", GeoCS = GCS_54, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_123E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_123E", ArcGISName = "Beijing_1954_Degree_GK_CM_123E", FMEName = "Bjing54/a.GK3d/CM-123E", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_126E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_126E", ArcGISName = "Beijing_1954_Degree_GK_CM_126E", FMEName = "Bjing54/a.GK3d/CM-126E", GeoCS = GCS_54, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_129E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_129E", ArcGISName = "Beijing_1954_Degree_GK_CM_129E", FMEName = "Bjing54/a.GK3d/CM-129E", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_132E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_132E", ArcGISName = "Beijing_1954_Degree_GK_CM_132E", FMEName = "Bjing54/a.GK3d/CM-132E", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_135E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_135E", ArcGISName = "Beijing_1954_Degree_GK_CM_135E", FMEName = "Bjing54/a.GK3d/CM-135E", GeoCS = GCS_54, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };

        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_102E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_102E", ArcGISName = "Xian_1980_Degree_GK_CM_102E", FMEName = "Xian80.GK3d/CM-102E", GeoCS = GCS_80, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_105E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_105E", ArcGISName = "Xian_1980_Degree_GK_CM_105E", FMEName = "Xian80.GK3d/CM-105E", GeoCS = GCS_80, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_108E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_108E", ArcGISName = "Xian_1980_Degree_GK_CM_108E", FMEName = "Xian80.GK3d/CM-108E", GeoCS = GCS_80, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_111E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_111E", ArcGISName = "Xian_1980_Degree_GK_CM_111E", FMEName = "Xian80.GK3d/CM-111E", GeoCS = GCS_80, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_114E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_114E", ArcGISName = "Xian_1980_Degree_GK_CM_114E", FMEName = "Xian80.GK3d/CM-114E", GeoCS = GCS_80, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_117E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_117E", ArcGISName = "Xian_1980_Degree_GK_CM_117E", FMEName = "Xian80.GK3d/CM-117E", GeoCS = GCS_80, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_120E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_120E", ArcGISName = "Xian_1980_Degree_GK_CM_120E", FMEName = "Xian80.GK3d/CM-120E", GeoCS = GCS_80, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_123E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_123E", ArcGISName = "Xian_1980_Degree_GK_CM_123E", FMEName = "Xian80.GK3d/CM-123E", GeoCS = GCS_80, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_126E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_126E", ArcGISName = "Xian_1980_Degree_GK_CM_126E", FMEName = "Xian80.GK3d/CM-126E", GeoCS = GCS_80, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_129E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_129E", ArcGISName = "Xian_1980_Degree_GK_CM_129E", FMEName = "Xian80.GK3d/CM-129E", GeoCS = GCS_80, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_132E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_132E", ArcGISName = "Xian_1980_Degree_GK_CM_132E", FMEName = "Xian80.GK3d/CM-132E", GeoCS = GCS_80, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_135E = new ProjectionSystem { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_135E", ArcGISName = "Xian_1980_Degree_GK_CM_135E", FMEName = "Xian80.GK3d/CM-135E", GeoCS = GCS_80, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };

        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_25 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_25", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_25", FMEName = "Beijing1954/a.GK3d-25", GeoCS = GCS_54, CenterMeridian = new Angle(75m), XOff = 25500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_26 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_26", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_26", FMEName = "Beijing1954/a.GK3d-26", GeoCS = GCS_54, CenterMeridian = new Angle(78m), XOff = 26500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_27 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_27", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_27", FMEName = "Beijing1954/a.GK3d-27", GeoCS = GCS_54, CenterMeridian = new Angle(81m), XOff = 27500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_28 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_28", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_28", FMEName = "Beijing1954/a.GK3d-28", GeoCS = GCS_54, CenterMeridian = new Angle(84m), XOff = 28500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_29 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_29", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_29", FMEName = "Beijing1954/a.GK3d-29", GeoCS = GCS_54, CenterMeridian = new Angle(87m), XOff = 29500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_30 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_30", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_30", FMEName = "Beijing1954/a.GK3d-30", GeoCS = GCS_54, CenterMeridian = new Angle(90m), XOff = 30500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_31 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_31", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_31", FMEName = "Beijing1954/a.GK3d-31", GeoCS = GCS_54, CenterMeridian = new Angle(93m), XOff = 31500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_32 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_32", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_32", FMEName = "Beijing1954/a.GK3d-32", GeoCS = GCS_54, CenterMeridian = new Angle(96m), XOff = 32500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_33 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_33", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_33", FMEName = "Beijing1954/a.GK3d-33", GeoCS = GCS_54, CenterMeridian = new Angle(99m), XOff = 33500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_34 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_34", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_34", FMEName = "Beijing1954/a.GK3d-34", GeoCS = GCS_54, CenterMeridian = new Angle(102m), XOff = 34500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_35 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_35", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_35", FMEName = "Beijing1954/a.GK3d-35", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 35500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_36 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_36", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_36", FMEName = "Beijing1954/a.GK3d-36", GeoCS = GCS_54, CenterMeridian = new Angle(108m), XOff = 36500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_37 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_37", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_37", FMEName = "Beijing1954/a.GK3d-37", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 37500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_38 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_38", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_38", FMEName = "Beijing1954/a.GK3d-38", GeoCS = GCS_54, CenterMeridian = new Angle(114m), XOff = 38500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_39 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_39", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_39", FMEName = "Beijing1954/a.GK3d-39", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 39500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_40 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_40", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_40", FMEName = "Beijing1954/a.GK3d-40", GeoCS = GCS_54, CenterMeridian = new Angle(120m), XOff = 40500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_41 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_41", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_41", FMEName = "Beijing1954/a.GK3d-41", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 41500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_42 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_42", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_42", FMEName = "Beijing1954/a.GK3d-42", GeoCS = GCS_54, CenterMeridian = new Angle(126m), XOff = 42500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_43 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_43", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_43", FMEName = "Beijing1954/a.GK3d-43", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 43500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_44 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_44", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_44", FMEName = "Beijing1954/a.GK3d-44", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 44500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_45 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_45", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_45", FMEName = "Beijing1954/a.GK3d-45", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 45500000, YOff = 0, };

        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_25 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_25", ArcGISName = "Xian_1980_3_Degree_GK_Zone_25", FMEName = "Xian80.GK3d-25", GeoCS = GCS_54, CenterMeridian = new Angle(75m), XOff = 25500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_26 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_26", ArcGISName = "Xian_1980_3_Degree_GK_Zone_26", FMEName = "Xian80.GK3d-26", GeoCS = GCS_54, CenterMeridian = new Angle(78m), XOff = 26500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_27 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_27", ArcGISName = "Xian_1980_3_Degree_GK_Zone_27", FMEName = "Xian80.GK3d-27", GeoCS = GCS_54, CenterMeridian = new Angle(81m), XOff = 27500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_28 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_28", ArcGISName = "Xian_1980_3_Degree_GK_Zone_28", FMEName = "Xian80.GK3d-28", GeoCS = GCS_54, CenterMeridian = new Angle(84m), XOff = 28500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_29 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_29", ArcGISName = "Xian_1980_3_Degree_GK_Zone_29", FMEName = "Xian80.GK3d-29", GeoCS = GCS_54, CenterMeridian = new Angle(87m), XOff = 29500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_30 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_30", ArcGISName = "Xian_1980_3_Degree_GK_Zone_30", FMEName = "Xian80.GK3d-30", GeoCS = GCS_54, CenterMeridian = new Angle(90m), XOff = 30500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_31 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_31", ArcGISName = "Xian_1980_3_Degree_GK_Zone_31", FMEName = "Xian80.GK3d-31", GeoCS = GCS_54, CenterMeridian = new Angle(93m), XOff = 31500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_32 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_32", ArcGISName = "Xian_1980_3_Degree_GK_Zone_32", FMEName = "Xian80.GK3d-32", GeoCS = GCS_54, CenterMeridian = new Angle(96m), XOff = 32500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_33 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_33", ArcGISName = "Xian_1980_3_Degree_GK_Zone_33", FMEName = "Xian80.GK3d-33", GeoCS = GCS_54, CenterMeridian = new Angle(99m), XOff = 33500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_34 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_34", ArcGISName = "Xian_1980_3_Degree_GK_Zone_34", FMEName = "Xian80.GK3d-34", GeoCS = GCS_54, CenterMeridian = new Angle(102m), XOff = 34500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_35 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_35", ArcGISName = "Xian_1980_3_Degree_GK_Zone_35", FMEName = "Xian80.GK3d-35", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 35500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_36 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_36", ArcGISName = "Xian_1980_3_Degree_GK_Zone_36", FMEName = "Xian80.GK3d-36", GeoCS = GCS_54, CenterMeridian = new Angle(108m), XOff = 36500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_37 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_37", ArcGISName = "Xian_1980_3_Degree_GK_Zone_37", FMEName = "Xian80.GK3d-37", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 37500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_38 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_38", ArcGISName = "Xian_1980_3_Degree_GK_Zone_38", FMEName = "Xian80.GK3d-38", GeoCS = GCS_54, CenterMeridian = new Angle(114m), XOff = 38500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_39 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_39", ArcGISName = "Xian_1980_3_Degree_GK_Zone_39", FMEName = "Xian80.GK3d-39", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 39500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_40 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_40", ArcGISName = "Xian_1980_3_Degree_GK_Zone_40", FMEName = "Xian80.GK3d-40", GeoCS = GCS_54, CenterMeridian = new Angle(120m), XOff = 40500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_41 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_41", ArcGISName = "Xian_1980_3_Degree_GK_Zone_41", FMEName = "Xian80.GK3d-41", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 41500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_42 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_42", ArcGISName = "Xian_1980_3_Degree_GK_Zone_42", FMEName = "Xian80.GK3d-42", GeoCS = GCS_54, CenterMeridian = new Angle(126m), XOff = 42500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_43 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_43", ArcGISName = "Xian_1980_3_Degree_GK_Zone_43", FMEName = "Xian80.GK3d-43", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 43500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_44 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_44", ArcGISName = "Xian_1980_3_Degree_GK_Zone_44", FMEName = "Xian80.GK3d-44", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 44500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_45 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_45", ArcGISName = "Xian_1980_3_Degree_GK_Zone_45", FMEName = "Xian80.GK3d-45", GeoCS = GCS_54, CenterMeridian = new Angle(135m), XOff = 45500000, YOff = 0, };

        public static string GetFMEName(string ellName, enumBandType bandType, int bandNum, int centerL)
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
            else if (ellName.StartsWith("Xian"))
            {
                switch (bandType)
                {
                    case enumBandType.Band0:
                        return string.Format("Xian80.GK3d/CM-{0}E", centerL);
                    case enumBandType.Band3:
                        return string.Format("Xian80.GK3d-{0}", bandNum);
                    case enumBandType.Band6:
                        return string.Format("Xian80.GK-{0}", bandNum);
                    default:
                        return null;
                }

            }
            else if (ellName.StartsWith("CGCS2000"))
            {
                switch (bandType)
                {
                    case enumBandType.Band0:
                        return string.Format("CGCS2000/GK3d-{0}E_FME", centerL);
                    case enumBandType.Band3:
                        return string.Format("CGCS2000/GK3d-{0}_FME", bandNum);
                    case enumBandType.Band6:
                        return string.Format("CGCS2000/GK6d-{0}_FME", bandNum);
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

            if (name == "Beijing 1954")
            {
                return GCS_54;
            }
            else if (name == "Xian 1980")
            {
                return GCS_80;
            }
            else if (name == "China Geodetic Coordinate System 2000")
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
