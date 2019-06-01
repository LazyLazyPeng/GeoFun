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

        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_75E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_75E", ArcGISName = "Beijing_1954_Degree_GK_CM_75E", ArcGISPyName = "Beijing 1954 Deree GK CM 75E", FMEName = "Bjing54/a.GK3d/CM-75E", GeoCS = GCS_54, CenterMeridian = new Angle(75m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_78E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_78E", ArcGISName = "Beijing_1954_Degree_GK_CM_78E", ArcGISPyName = "Beijing 1954 Deree GK CM 78E", FMEName = "Bjing54/a.GK3d/CM-78E", GeoCS = GCS_54, CenterMeridian = new Angle(78m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_81E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_81E", ArcGISName = "Beijing_1954_Degree_GK_CM_81E", ArcGISPyName = "Beijing 1954 Deree GK CM 81E", FMEName = "Bjing54/a.GK3d/CM-81E", GeoCS = GCS_54, CenterMeridian = new Angle(81m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_84E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_84E", ArcGISName = "Beijing_1954_Degree_GK_CM_84E", ArcGISPyName = "Beijing 1954 Deree GK CM 84E", FMEName = "Bjing54/a.GK3d/CM-84E", GeoCS = GCS_54, CenterMeridian = new Angle(84m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_87E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_87E", ArcGISName = "Beijing_1954_Degree_GK_CM_87E", ArcGISPyName = "Beijing 1954 Deree GK CM 87E", FMEName = "Bjing54/a.GK3d/CM-87E", GeoCS = GCS_54, CenterMeridian = new Angle(87m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_90E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_90E", ArcGISName = "Beijing_1954_Degree_GK_CM_90E", ArcGISPyName = "Beijing 1954 Deree GK CM 90E", FMEName = "Bjing54/a.GK3d/CM-90E", GeoCS = GCS_54, CenterMeridian = new Angle(90m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_93E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_93E", ArcGISName = "Beijing_1954_Degree_GK_CM_93E", ArcGISPyName = "Beijing 1954 Deree GK CM 93E", FMEName = "Bjing54/a.GK3d/CM-93E", GeoCS = GCS_54, CenterMeridian = new Angle(93m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_96E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_96E", ArcGISName = "Beijing_1954_Degree_GK_CM_96E", ArcGISPyName = "Beijing 1954 Deree GK CM 96E", FMEName = "Bjing54/a.GK3d/CM-96E", GeoCS = GCS_54, CenterMeridian = new Angle(96m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_99E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_99E", ArcGISName = "Beijing_1954_Degree_GK_CM_99E", ArcGISPyName = "Beijing 1954 Deree GK CM 99E", FMEName = "Bjing54/a.GK3d/CM-99E", GeoCS = GCS_54, CenterMeridian = new Angle(99m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_102E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_102E", ArcGISName = "Beijing_1954_Degree_GK_CM_102E", ArcGISPyName = "Beijing 1954 Deree GK CM 102E", FMEName = "Bjing54/a.GK3d/CM-102E", GeoCS = GCS_54, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_105E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_105E", ArcGISName = "Beijing_1954_Degree_GK_CM_105E", ArcGISPyName = "Beijing 1954 Deree GK CM 105E", FMEName = "Bjing54/a.GK3d/CM-105E", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_108E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_108E", ArcGISName = "Beijing_1954_Degree_GK_CM_108E", ArcGISPyName = "Beijing 1954 Deree GK CM 108E", FMEName = "Bjing54/a.GK3d/CM-108E", GeoCS = GCS_54, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_111E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_111E", ArcGISName = "Beijing_1954_Degree_GK_CM_111E", ArcGISPyName = "Beijing 1954 Deree GK CM 111E", FMEName = "Bjing54/a.GK3d/CM-111E", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_114E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_114E", ArcGISName = "Beijing_1954_Degree_GK_CM_114E", ArcGISPyName = "Beijing 1954 Deree GK CM 114E", FMEName = "Bjing54/a.GK3d/CM-114E", GeoCS = GCS_54, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_117E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_117E", ArcGISName = "Beijing_1954_Degree_GK_CM_117E", ArcGISPyName = "Beijing 1954 Deree GK CM 117E", FMEName = "Bjing54/a.GK3d/CM-117E", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_120E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_120E", ArcGISName = "Beijing_1954_Degree_GK_CM_120E", ArcGISPyName = "Beijing 1954 Deree GK CM 120E", FMEName = "Bjing54/a.GK3d/CM-120E", GeoCS = GCS_54, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_123E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_123E", ArcGISName = "Beijing_1954_Degree_GK_CM_123E", ArcGISPyName = "Beijing 1954 Deree GK CM 123E", FMEName = "Bjing54/a.GK3d/CM-123E", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_126E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_126E", ArcGISName = "Beijing_1954_Degree_GK_CM_126E", ArcGISPyName = "Beijing 1954 Deree GK CM 126E", FMEName = "Bjing54/a.GK3d/CM-126E", GeoCS = GCS_54, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_129E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_129E", ArcGISName = "Beijing_1954_Degree_GK_CM_129E", ArcGISPyName = "Beijing 1954 Deree GK CM 129E", FMEName = "Bjing54/a.GK3d/CM-129E", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_132E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_132E", ArcGISName = "Beijing_1954_Degree_GK_CM_132E", ArcGISPyName = "Beijing 1954 Deree GK CM 132E", FMEName = "Bjing54/a.GK3d/CM-132E", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_Degree_GK_CM_135E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_Degree_GK_CM_135E", ArcGISName = "Beijing_1954_Degree_GK_CM_135E", ArcGISPyName = "Beijing 1954 Deree GK CM 135E", FMEName = "Bjing54/a.GK3d/CM-135E", GeoCS = GCS_54, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_25 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_25", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_25", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 25", FMEName = "Beijing1954/a.GK3d-25", GeoCS = GCS_54, CenterMeridian = new Angle(75m), XOff = 25500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_26 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_26", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_26", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 26", FMEName = "Beijing1954/a.GK3d-26", GeoCS = GCS_54, CenterMeridian = new Angle(78m), XOff = 26500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_27 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_27", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_27", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 27", FMEName = "Beijing1954/a.GK3d-27", GeoCS = GCS_54, CenterMeridian = new Angle(81m), XOff = 27500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_28 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_28", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_28", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 28", FMEName = "Beijing1954/a.GK3d-28", GeoCS = GCS_54, CenterMeridian = new Angle(84m), XOff = 28500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_29 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_29", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_29", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 29", FMEName = "Beijing1954/a.GK3d-29", GeoCS = GCS_54, CenterMeridian = new Angle(87m), XOff = 29500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_30 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_30", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_30", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 30", FMEName = "Beijing1954/a.GK3d-30", GeoCS = GCS_54, CenterMeridian = new Angle(90m), XOff = 30500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_31 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_31", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_31", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 31", FMEName = "Beijing1954/a.GK3d-31", GeoCS = GCS_54, CenterMeridian = new Angle(93m), XOff = 31500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_32 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_32", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_32", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 32", FMEName = "Beijing1954/a.GK3d-32", GeoCS = GCS_54, CenterMeridian = new Angle(96m), XOff = 32500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_33 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_33", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_33", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 33", FMEName = "Beijing1954/a.GK3d-33", GeoCS = GCS_54, CenterMeridian = new Angle(99m), XOff = 33500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_34 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_34", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_34", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 34", FMEName = "Beijing1954/a.GK3d-34", GeoCS = GCS_54, CenterMeridian = new Angle(102m), XOff = 34500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_35 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_35", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_35", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 35", FMEName = "Beijing1954/a.GK3d-35", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 35500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_36 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_36", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_36", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 36", FMEName = "Beijing1954/a.GK3d-36", GeoCS = GCS_54, CenterMeridian = new Angle(108m), XOff = 36500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_37 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_37", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_37", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 37", FMEName = "Beijing1954/a.GK3d-37", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 37500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_38 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_38", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_38", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 38", FMEName = "Beijing1954/a.GK3d-38", GeoCS = GCS_54, CenterMeridian = new Angle(114m), XOff = 38500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_39 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_39", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_39", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 39", FMEName = "Beijing1954/a.GK3d-39", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 39500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_40 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_40", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_40", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 40", FMEName = "Beijing1954/a.GK3d-40", GeoCS = GCS_54, CenterMeridian = new Angle(120m), XOff = 40500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_41 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_41", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_41", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 41", FMEName = "Beijing1954/a.GK3d-41", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 41500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_42 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_42", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_42", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 42", FMEName = "Beijing1954/a.GK3d-42", GeoCS = GCS_54, CenterMeridian = new Angle(126m), XOff = 42500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_43 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_43", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_43", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 43", FMEName = "Beijing1954/a.GK3d-43", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 43500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_44 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_44", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_44", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 44", FMEName = "Beijing1954/a.GK3d-44", GeoCS = GCS_54, CenterMeridian = new Angle(132m), XOff = 44500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_3_Degree_GK_Zone_45 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_3_Degree_GK_Zone_45", ArcGISName = "Beijing_1954_3_Degree_GK_Zone_45", ArcGISPyName = "Beijing 1954 3 Degree GK Zone 45", FMEName = "Beijing1954/a.GK3d-45", GeoCS = GCS_54, CenterMeridian = new Angle(135m), XOff = 45500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_13 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_13", ArcGISName = "Beijing_1954_GK_Zone_13", ArcGISPyName = "Beijing 1954 GK Zone 13", FMEName = "Beijing1954/a.GK-13", GeoCS = GCS_54, CenterMeridian = new Angle(75m), XOff = 13500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_14 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_14", ArcGISName = "Beijing_1954_GK_Zone_14", ArcGISPyName = "Beijing 1954 GK Zone 14", FMEName = "Beijing1954/a.GK-14", GeoCS = GCS_54, CenterMeridian = new Angle(81m), XOff = 14500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_15 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_15", ArcGISName = "Beijing_1954_GK_Zone_15", ArcGISPyName = "Beijing 1954 GK Zone 15", FMEName = "Beijing1954/a.GK-15", GeoCS = GCS_54, CenterMeridian = new Angle(87m), XOff = 15500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_16 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_16", ArcGISName = "Beijing_1954_GK_Zone_16", ArcGISPyName = "Beijing 1954 GK Zone 16", FMEName = "Beijing1954/a.GK-16", GeoCS = GCS_54, CenterMeridian = new Angle(93m), XOff = 16500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_17 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_17", ArcGISName = "Beijing_1954_GK_Zone_17", ArcGISPyName = "Beijing 1954 GK Zone 17", FMEName = "Beijing1954/a.GK-17", GeoCS = GCS_54, CenterMeridian = new Angle(99m), XOff = 17500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_18 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_18", ArcGISName = "Beijing_1954_GK_Zone_18", ArcGISPyName = "Beijing 1954 GK Zone 18", FMEName = "Beijing1954/a.GK-18", GeoCS = GCS_54, CenterMeridian = new Angle(105m), XOff = 18500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_19 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_19", ArcGISName = "Beijing_1954_GK_Zone_19", ArcGISPyName = "Beijing 1954 GK Zone 19", FMEName = "Beijing1954/a.GK-19", GeoCS = GCS_54, CenterMeridian = new Angle(111m), XOff = 19500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_20 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_20", ArcGISName = "Beijing_1954_GK_Zone_20", ArcGISPyName = "Beijing 1954 GK Zone 20", FMEName = "Beijing1954/a.GK-20", GeoCS = GCS_54, CenterMeridian = new Angle(117m), XOff = 20500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_21 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_21", ArcGISName = "Beijing_1954_GK_Zone_21", ArcGISPyName = "Beijing 1954 GK Zone 21", FMEName = "Beijing1954/a.GK-21", GeoCS = GCS_54, CenterMeridian = new Angle(123m), XOff = 21500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_22 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_22", ArcGISName = "Beijing_1954_GK_Zone_22", ArcGISPyName = "Beijing 1954 GK Zone 22", FMEName = "Beijing1954/a.GK-22", GeoCS = GCS_54, CenterMeridian = new Angle(129m), XOff = 22500000, YOff = 0, };
        public static readonly IProjectionSystem Beijing_1954_GK_Zone_23 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Beijing_1954_GK_Zone_23", ArcGISName = "Beijing_1954_GK_Zone_23", ArcGISPyName = "Beijing 1954 GK Zone 23", FMEName = "Beijing1954/a.GK-23", GeoCS = GCS_54, CenterMeridian = new Angle(135m), XOff = 23500000, YOff = 0, };

        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_75E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_75E", ArcGISName = "Xian_1980_Degree_GK_CM_75E", ArcGISPyName = "Xian 1980 Degree GK CM 75E", FMEName = "Xian80.GK3d/CM-75E", GeoCS = GCS_80, CenterMeridian = new Angle(75m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_78E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_78E", ArcGISName = "Xian_1980_Degree_GK_CM_78E", ArcGISPyName = "Xian 1980 Degree GK CM 78E", FMEName = "Xian80.GK3d/CM-78E", GeoCS = GCS_80, CenterMeridian = new Angle(78m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_81E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_81E", ArcGISName = "Xian_1980_Degree_GK_CM_81E", ArcGISPyName = "Xian 1980 Degree GK CM 81E", FMEName = "Xian80.GK3d/CM-81E", GeoCS = GCS_80, CenterMeridian = new Angle(81m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_84E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_84E", ArcGISName = "Xian_1980_Degree_GK_CM_84E", ArcGISPyName = "Xian 1980 Degree GK CM 84E", FMEName = "Xian80.GK3d/CM-84E", GeoCS = GCS_80, CenterMeridian = new Angle(84m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_87E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_87E", ArcGISName = "Xian_1980_Degree_GK_CM_87E", ArcGISPyName = "Xian 1980 Degree GK CM 87E", FMEName = "Xian80.GK3d/CM-87E", GeoCS = GCS_80, CenterMeridian = new Angle(87m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_90E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_90E", ArcGISName = "Xian_1980_Degree_GK_CM_90E", ArcGISPyName = "Xian 1980 Degree GK CM 90E", FMEName = "Xian80.GK3d/CM-90E", GeoCS = GCS_80, CenterMeridian = new Angle(90m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_93E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_93E", ArcGISName = "Xian_1980_Degree_GK_CM_93E", ArcGISPyName = "Xian 1980 Degree GK CM 93E", FMEName = "Xian80.GK3d/CM-93E", GeoCS = GCS_80, CenterMeridian = new Angle(93m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_96E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_96E", ArcGISName = "Xian_1980_Degree_GK_CM_96E", ArcGISPyName = "Xian 1980 Degree GK CM 96E", FMEName = "Xian80.GK3d/CM-96E", GeoCS = GCS_80, CenterMeridian = new Angle(96m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_99E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_99E", ArcGISName = "Xian_1980_Degree_GK_CM_99E", ArcGISPyName = "Xian 1980 Degree GK CM 99E", FMEName = "Xian80.GK3d/CM-99E", GeoCS = GCS_80, CenterMeridian = new Angle(99m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_102E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_102E", ArcGISName = "Xian_1980_Degree_GK_CM_102E", ArcGISPyName = "Xian 1980 Degree GK CM 102E", FMEName = "Xian80.GK3d/CM-102E", GeoCS = GCS_80, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_105E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_105E", ArcGISName = "Xian_1980_Degree_GK_CM_105E", ArcGISPyName = "Xian 1980 Degree GK CM 105E", FMEName = "Xian80.GK3d/CM-105E", GeoCS = GCS_80, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_108E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_108E", ArcGISName = "Xian_1980_Degree_GK_CM_108E", ArcGISPyName = "Xian 1980 Degree GK CM 108E", FMEName = "Xian80.GK3d/CM-108E", GeoCS = GCS_80, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_111E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_111E", ArcGISName = "Xian_1980_Degree_GK_CM_111E", ArcGISPyName = "Xian 1980 Degree GK CM 111E", FMEName = "Xian80.GK3d/CM-111E", GeoCS = GCS_80, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_114E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_114E", ArcGISName = "Xian_1980_Degree_GK_CM_114E", ArcGISPyName = "Xian 1980 Degree GK CM 114E", FMEName = "Xian80.GK3d/CM-114E", GeoCS = GCS_80, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_117E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_117E", ArcGISName = "Xian_1980_Degree_GK_CM_117E", ArcGISPyName = "Xian 1980 Degree GK CM 117E", FMEName = "Xian80.GK3d/CM-117E", GeoCS = GCS_80, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_120E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_120E", ArcGISName = "Xian_1980_Degree_GK_CM_120E", ArcGISPyName = "Xian 1980 Degree GK CM 120E", FMEName = "Xian80.GK3d/CM-120E", GeoCS = GCS_80, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_123E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_123E", ArcGISName = "Xian_1980_Degree_GK_CM_123E", ArcGISPyName = "Xian 1980 Degree GK CM 123E", FMEName = "Xian80.GK3d/CM-123E", GeoCS = GCS_80, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_126E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_126E", ArcGISName = "Xian_1980_Degree_GK_CM_126E", ArcGISPyName = "Xian 1980 Degree GK CM 126E", FMEName = "Xian80.GK3d/CM-126E", GeoCS = GCS_80, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_129E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_129E", ArcGISName = "Xian_1980_Degree_GK_CM_129E", ArcGISPyName = "Xian 1980 Degree GK CM 129E", FMEName = "Xian80.GK3d/CM-129E", GeoCS = GCS_80, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_132E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_132E", ArcGISName = "Xian_1980_Degree_GK_CM_132E", ArcGISPyName = "Xian 1980 Degree GK CM 132E", FMEName = "Xian80.GK3d/CM-132E", GeoCS = GCS_80, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_Degree_GK_CM_135E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_Degree_GK_CM_135E", ArcGISName = "Xian_1980_Degree_GK_CM_135E", ArcGISPyName = "Xian 1980 Degree GK CM 135E", FMEName = "Xian80.GK3d/CM-135E", GeoCS = GCS_80, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_25 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_25", ArcGISName = "Xian_1980_3_Degree_GK_Zone_25", ArcGISPyName = "Xian 1980 3 Degree GK Zone 25", FMEName = "Xian80.GK3d-25", GeoCS = GCS_80, CenterMeridian = new Angle(75m), XOff = 25500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_26 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_26", ArcGISName = "Xian_1980_3_Degree_GK_Zone_26", ArcGISPyName = "Xian 1980 3 Degree GK Zone 26", FMEName = "Xian80.GK3d-26", GeoCS = GCS_80, CenterMeridian = new Angle(78m), XOff = 26500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_27 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_27", ArcGISName = "Xian_1980_3_Degree_GK_Zone_27", ArcGISPyName = "Xian 1980 3 Degree GK Zone 27", FMEName = "Xian80.GK3d-27", GeoCS = GCS_80, CenterMeridian = new Angle(81m), XOff = 27500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_28 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_28", ArcGISName = "Xian_1980_3_Degree_GK_Zone_28", ArcGISPyName = "Xian 1980 3 Degree GK Zone 28", FMEName = "Xian80.GK3d-28", GeoCS = GCS_80, CenterMeridian = new Angle(84m), XOff = 28500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_29 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_29", ArcGISName = "Xian_1980_3_Degree_GK_Zone_29", ArcGISPyName = "Xian 1980 3 Degree GK Zone 29", FMEName = "Xian80.GK3d-29", GeoCS = GCS_80, CenterMeridian = new Angle(87m), XOff = 29500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_30 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_30", ArcGISName = "Xian_1980_3_Degree_GK_Zone_30", ArcGISPyName = "Xian 1980 3 Degree GK Zone 30", FMEName = "Xian80.GK3d-30", GeoCS = GCS_80, CenterMeridian = new Angle(90m), XOff = 30500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_31 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_31", ArcGISName = "Xian_1980_3_Degree_GK_Zone_31", ArcGISPyName = "Xian 1980 3 Degree GK Zone 31", FMEName = "Xian80.GK3d-31", GeoCS = GCS_80, CenterMeridian = new Angle(93m), XOff = 31500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_32 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_32", ArcGISName = "Xian_1980_3_Degree_GK_Zone_32", ArcGISPyName = "Xian 1980 3 Degree GK Zone 32", FMEName = "Xian80.GK3d-32", GeoCS = GCS_80, CenterMeridian = new Angle(96m), XOff = 32500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_33 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_33", ArcGISName = "Xian_1980_3_Degree_GK_Zone_33", ArcGISPyName = "Xian 1980 3 Degree GK Zone 33", FMEName = "Xian80.GK3d-33", GeoCS = GCS_80, CenterMeridian = new Angle(99m), XOff = 33500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_34 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_34", ArcGISName = "Xian_1980_3_Degree_GK_Zone_34", ArcGISPyName = "Xian 1980 3 Degree GK Zone 34", FMEName = "Xian80.GK3d-34", GeoCS = GCS_80, CenterMeridian = new Angle(102m), XOff = 34500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_35 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_35", ArcGISName = "Xian_1980_3_Degree_GK_Zone_35", ArcGISPyName = "Xian 1980 3 Degree GK Zone 35", FMEName = "Xian80.GK3d-35", GeoCS = GCS_80, CenterMeridian = new Angle(105m), XOff = 35500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_36 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_36", ArcGISName = "Xian_1980_3_Degree_GK_Zone_36", ArcGISPyName = "Xian 1980 3 Degree GK Zone 36", FMEName = "Xian80.GK3d-36", GeoCS = GCS_80, CenterMeridian = new Angle(108m), XOff = 36500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_37 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_37", ArcGISName = "Xian_1980_3_Degree_GK_Zone_37", ArcGISPyName = "Xian 1980 3 Degree GK Zone 37", FMEName = "Xian80.GK3d-37", GeoCS = GCS_80, CenterMeridian = new Angle(111m), XOff = 37500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_38 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_38", ArcGISName = "Xian_1980_3_Degree_GK_Zone_38", ArcGISPyName = "Xian 1980 3 Degree GK Zone 38", FMEName = "Xian80.GK3d-38", GeoCS = GCS_80, CenterMeridian = new Angle(114m), XOff = 38500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_39 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_39", ArcGISName = "Xian_1980_3_Degree_GK_Zone_39", ArcGISPyName = "Xian 1980 3 Degree GK Zone 39", FMEName = "Xian80.GK3d-39", GeoCS = GCS_80, CenterMeridian = new Angle(117m), XOff = 39500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_40 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_40", ArcGISName = "Xian_1980_3_Degree_GK_Zone_40", ArcGISPyName = "Xian 1980 3 Degree GK Zone 40", FMEName = "Xian80.GK3d-40", GeoCS = GCS_80, CenterMeridian = new Angle(120m), XOff = 40500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_41 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_41", ArcGISName = "Xian_1980_3_Degree_GK_Zone_41", ArcGISPyName = "Xian 1980 3 Degree GK Zone 41", FMEName = "Xian80.GK3d-41", GeoCS = GCS_80, CenterMeridian = new Angle(123m), XOff = 41500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_42 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_42", ArcGISName = "Xian_1980_3_Degree_GK_Zone_42", ArcGISPyName = "Xian 1980 3 Degree GK Zone 42", FMEName = "Xian80.GK3d-42", GeoCS = GCS_80, CenterMeridian = new Angle(126m), XOff = 42500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_43 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_43", ArcGISName = "Xian_1980_3_Degree_GK_Zone_43", ArcGISPyName = "Xian 1980 3 Degree GK Zone 43", FMEName = "Xian80.GK3d-43", GeoCS = GCS_80, CenterMeridian = new Angle(129m), XOff = 43500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_44 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_44", ArcGISName = "Xian_1980_3_Degree_GK_Zone_44", ArcGISPyName = "Xian 1980 3 Degree GK Zone 44", FMEName = "Xian80.GK3d-44", GeoCS = GCS_80, CenterMeridian = new Angle(132m), XOff = 44500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_3_Degree_GK_Zone_45 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_3_Degree_GK_Zone_45", ArcGISName = "Xian_1980_3_Degree_GK_Zone_45", ArcGISPyName = "Xian 1980 3 Degree GK Zone 45", FMEName = "Xian80.GK3d-45", GeoCS = GCS_80, CenterMeridian = new Angle(135m), XOff = 45500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_13 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_13", ArcGISName = "Xian_1980_GK_Zone_13", ArcGISPyName = "Xian 1980 GK Zone 13", FMEName = "Xian80.GK-13", GeoCS = GCS_80, CenterMeridian = new Angle(75m), XOff = 13500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_14 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_14", ArcGISName = "Xian_1980_GK_Zone_14", ArcGISPyName = "Xian 1980 GK Zone 14", FMEName = "Xian80.GK-14", GeoCS = GCS_80, CenterMeridian = new Angle(81m), XOff = 14500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_15 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_15", ArcGISName = "Xian_1980_GK_Zone_15", ArcGISPyName = "Xian 1980 GK Zone 15", FMEName = "Xian80.GK-15", GeoCS = GCS_80, CenterMeridian = new Angle(87m), XOff = 15500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_16 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_16", ArcGISName = "Xian_1980_GK_Zone_16", ArcGISPyName = "Xian 1980 GK Zone 16", FMEName = "Xian80.GK-16", GeoCS = GCS_80, CenterMeridian = new Angle(93m), XOff = 16500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_17 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_17", ArcGISName = "Xian_1980_GK_Zone_17", ArcGISPyName = "Xian 1980 GK Zone 17", FMEName = "Xian80.GK-17", GeoCS = GCS_80, CenterMeridian = new Angle(99m), XOff = 17500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_18 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_18", ArcGISName = "Xian_1980_GK_Zone_18", ArcGISPyName = "Xian 1980 GK Zone 18", FMEName = "Xian80.GK-18", GeoCS = GCS_80, CenterMeridian = new Angle(105m), XOff = 18500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_19 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_19", ArcGISName = "Xian_1980_GK_Zone_19", ArcGISPyName = "Xian 1980 GK Zone 19", FMEName = "Xian80.GK-19", GeoCS = GCS_80, CenterMeridian = new Angle(111m), XOff = 19500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_20 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_20", ArcGISName = "Xian_1980_GK_Zone_20", ArcGISPyName = "Xian 1980 GK Zone 20", FMEName = "Xian80.GK-20", GeoCS = GCS_80, CenterMeridian = new Angle(117m), XOff = 20500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_21 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_21", ArcGISName = "Xian_1980_GK_Zone_21", ArcGISPyName = "Xian 1980 GK Zone 21", FMEName = "Xian80.GK-21", GeoCS = GCS_80, CenterMeridian = new Angle(123m), XOff = 21500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_22 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_22", ArcGISName = "Xian_1980_GK_Zone_22", ArcGISPyName = "Xian 1980 GK Zone 22", FMEName = "Xian80.GK-22", GeoCS = GCS_80, CenterMeridian = new Angle(129m), XOff = 22500000, YOff = 0, };
        public static readonly IProjectionSystem Xian_1980_GK_Zone_23 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "Xian_1980_GK_Zone_23", ArcGISName = "Xian_1980_GK_Zone_23", ArcGISPyName = "Xian 1980 GK Zone 23", FMEName = "Xian80.GK-23", GeoCS = GCS_80, CenterMeridian = new Angle(135m), XOff = 23500000, YOff = 0, };

        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_75E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_75E", ArcGISName = "CGCS2000_3_Degree_GK_CM_75E", ArcGISPyName = "CGCS2000 3 Degree GK CM 75E", FMEName = "CGCS2000/GK3d-75E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(75m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_78E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_78E", ArcGISName = "CGCS2000_3_Degree_GK_CM_78E", ArcGISPyName = "CGCS2000 3 Degree GK CM 78E", FMEName = "CGCS2000/GK3d-78E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(78m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_81E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_81E", ArcGISName = "CGCS2000_3_Degree_GK_CM_81E", ArcGISPyName = "CGCS2000 3 Degree GK CM 81E", FMEName = "CGCS2000/GK3d-81E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(81m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_84E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_84E", ArcGISName = "CGCS2000_3_Degree_GK_CM_84E", ArcGISPyName = "CGCS2000 3 Degree GK CM 84E", FMEName = "CGCS2000/GK3d-84E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(84m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_87E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_87E", ArcGISName = "CGCS2000_3_Degree_GK_CM_87E", ArcGISPyName = "CGCS2000 3 Degree GK CM 87E", FMEName = "CGCS2000/GK3d-87E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(87m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_90E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_90E", ArcGISName = "CGCS2000_3_Degree_GK_CM_90E", ArcGISPyName = "CGCS2000 3 Degree GK CM 90E", FMEName = "CGCS2000/GK3d-90E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(90m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_93E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_93E", ArcGISName = "CGCS2000_3_Degree_GK_CM_93E", ArcGISPyName = "CGCS2000 3 Degree GK CM 93E", FMEName = "CGCS2000/GK3d-93E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(93m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_96E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_96E", ArcGISName = "CGCS2000_3_Degree_GK_CM_96E", ArcGISPyName = "CGCS2000 3 Degree GK CM 96E", FMEName = "CGCS2000/GK3d-96E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(96m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_99E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_99E", ArcGISName = "CGCS2000_3_Degree_GK_CM_99E", ArcGISPyName = "CGCS2000 3 Degree GK CM 99E", FMEName = "CGCS2000/GK3d-99E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(99m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_102E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_102E", ArcGISName = "CGCS2000_3_Degree_GK_CM_102E", ArcGISPyName = "CGCS2000 3 Degree GK CM 102E", FMEName = "CGCS2000/GK3d-102E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(102m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_105E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_105E", ArcGISName = "CGCS2000_3_Degree_GK_CM_105E", ArcGISPyName = "CGCS2000 3 Degree GK CM 105E", FMEName = "CGCS2000/GK3d-105E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(105m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_108E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_108E", ArcGISName = "CGCS2000_3_Degree_GK_CM_108E", ArcGISPyName = "CGCS2000 3 Degree GK CM 108E", FMEName = "CGCS2000/GK3d-108E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(108m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_111E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_111E", ArcGISName = "CGCS2000_3_Degree_GK_CM_111E", ArcGISPyName = "CGCS2000 3 Degree GK CM 111E", FMEName = "CGCS2000/GK3d-111E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(111m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_114E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_114E", ArcGISName = "CGCS2000_3_Degree_GK_CM_114E", ArcGISPyName = "CGCS2000 3 Degree GK CM 114E", FMEName = "CGCS2000/GK3d-114E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(114m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_117E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_117E", ArcGISName = "CGCS2000_3_Degree_GK_CM_117E", ArcGISPyName = "CGCS2000 3 Degree GK CM 117E", FMEName = "CGCS2000/GK3d-117E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(117m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_120E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_120E", ArcGISName = "CGCS2000_3_Degree_GK_CM_120E", ArcGISPyName = "CGCS2000 3 Degree GK CM 120E", FMEName = "CGCS2000/GK3d-120E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(120m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_123E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_123E", ArcGISName = "CGCS2000_3_Degree_GK_CM_123E", ArcGISPyName = "CGCS2000 3 Degree GK CM 123E", FMEName = "CGCS2000/GK3d-123E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(123m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_126E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_126E", ArcGISName = "CGCS2000_3_Degree_GK_CM_126E", ArcGISPyName = "CGCS2000 3 Degree GK CM 126E", FMEName = "CGCS2000/GK3d-126E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(126m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_129E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_129E", ArcGISName = "CGCS2000_3_Degree_GK_CM_129E", ArcGISPyName = "CGCS2000 3 Degree GK CM 129E", FMEName = "CGCS2000/GK3d-129E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(129m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_132E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_132E", ArcGISName = "CGCS2000_3_Degree_GK_CM_132E", ArcGISPyName = "CGCS2000 3 Degree GK CM 132E", FMEName = "CGCS2000/GK3d-132E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(132m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_CM_135E = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_CM_135E", ArcGISName = "CGCS2000_3_Degree_GK_CM_135E", ArcGISPyName = "CGCS2000 3 Degree GK CM 135E", FMEName = "CGCS2000/GK3d-135E_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(135m), XOff = 500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_25 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_25", ArcGISName = "CGCS2000_3_Degree_GK_Zone_25", ArcGISPyName = "CGCS2000 3 Degree GK Zone 25", FMEName = " CGCS2000/GK3d-25_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(75m), XOff = 25500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_26 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_26", ArcGISName = "CGCS2000_3_Degree_GK_Zone_26", ArcGISPyName = "CGCS2000 3 Degree GK Zone 26", FMEName = " CGCS2000/GK3d-26_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(78m), XOff = 26500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_27 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_27", ArcGISName = "CGCS2000_3_Degree_GK_Zone_27", ArcGISPyName = "CGCS2000 3 Degree GK Zone 27", FMEName = " CGCS2000/GK3d-27_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(81m), XOff = 27500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_28 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_28", ArcGISName = "CGCS2000_3_Degree_GK_Zone_28", ArcGISPyName = "CGCS2000 3 Degree GK Zone 28", FMEName = " CGCS2000/GK3d-28_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(84m), XOff = 28500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_29 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_29", ArcGISName = "CGCS2000_3_Degree_GK_Zone_29", ArcGISPyName = "CGCS2000 3 Degree GK Zone 29", FMEName = " CGCS2000/GK3d-29_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(87m), XOff = 29500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_30 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_30", ArcGISName = "CGCS2000_3_Degree_GK_Zone_30", ArcGISPyName = "CGCS2000 3 Degree GK Zone 30", FMEName = " CGCS2000/GK3d-30_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(90m), XOff = 30500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_31 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_31", ArcGISName = "CGCS2000_3_Degree_GK_Zone_31", ArcGISPyName = "CGCS2000 3 Degree GK Zone 31", FMEName = " CGCS2000/GK3d-31_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(93m), XOff = 31500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_32 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_32", ArcGISName = "CGCS2000_3_Degree_GK_Zone_32", ArcGISPyName = "CGCS2000 3 Degree GK Zone 32", FMEName = " CGCS2000/GK3d-32_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(96m), XOff = 32500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_33 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_33", ArcGISName = "CGCS2000_3_Degree_GK_Zone_33", ArcGISPyName = "CGCS2000 3 Degree GK Zone 33", FMEName = " CGCS2000/GK3d-33_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(99m), XOff = 33500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_34 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_34", ArcGISName = "CGCS2000_3_Degree_GK_Zone_34", ArcGISPyName = "CGCS2000 3 Degree GK Zone 34", FMEName = " CGCS2000/GK3d-34_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(102m), XOff = 34500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_35 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_35", ArcGISName = "CGCS2000_3_Degree_GK_Zone_35", ArcGISPyName = "CGCS2000 3 Degree GK Zone 35", FMEName = " CGCS2000/GK3d-35_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(105m), XOff = 35500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_36 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_36", ArcGISName = "CGCS2000_3_Degree_GK_Zone_36", ArcGISPyName = "CGCS2000 3 Degree GK Zone 36", FMEName = " CGCS2000/GK3d-36_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(108m), XOff = 36500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_37 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_37", ArcGISName = "CGCS2000_3_Degree_GK_Zone_37", ArcGISPyName = "CGCS2000 3 Degree GK Zone 37", FMEName = " CGCS2000/GK3d-37_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(111m), XOff = 37500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_38 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_38", ArcGISName = "CGCS2000_3_Degree_GK_Zone_38", ArcGISPyName = "CGCS2000 3 Degree GK Zone 38", FMEName = " CGCS2000/GK3d-38_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(114m), XOff = 38500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_39 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_39", ArcGISName = "CGCS2000_3_Degree_GK_Zone_39", ArcGISPyName = "CGCS2000 3 Degree GK Zone 39", FMEName = " CGCS2000/GK3d-39_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(117m), XOff = 39500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_40 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_40", ArcGISName = "CGCS2000_3_Degree_GK_Zone_40", ArcGISPyName = "CGCS2000 3 Degree GK Zone 40", FMEName = " CGCS2000/GK3d-40_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(120m), XOff = 40500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_41 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_41", ArcGISName = "CGCS2000_3_Degree_GK_Zone_41", ArcGISPyName = "CGCS2000 3 Degree GK Zone 41", FMEName = " CGCS2000/GK3d-41_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(123m), XOff = 41500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_42 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_42", ArcGISName = "CGCS2000_3_Degree_GK_Zone_42", ArcGISPyName = "CGCS2000 3 Degree GK Zone 42", FMEName = " CGCS2000/GK3d-42_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(126m), XOff = 42500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_43 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_43", ArcGISName = "CGCS2000_3_Degree_GK_Zone_43", ArcGISPyName = "CGCS2000 3 Degree GK Zone 43", FMEName = " CGCS2000/GK3d-43_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(129m), XOff = 43500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_44 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_44", ArcGISName = "CGCS2000_3_Degree_GK_Zone_44", ArcGISPyName = "CGCS2000 3 Degree GK Zone 44", FMEName = " CGCS2000/GK3d-44_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(132m), XOff = 44500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_3_Degree_GK_Zone_45 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_3_Degree_GK_Zone_45", ArcGISName = "CGCS2000_3_Degree_GK_Zone_45", ArcGISPyName = "CGCS2000 3 Degree GK Zone 45", FMEName = " CGCS2000/GK3d-45_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(135m), XOff = 45500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_13 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_13", ArcGISName = "CGCS2000_GK_Zone_13", ArcGISPyName = "CGCS2000 GK Zone 13", FMEName = "CGCS2000/GK6d-13_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(75m), XOff = 13500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_14 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_14", ArcGISName = "CGCS2000_GK_Zone_14", ArcGISPyName = "CGCS2000 GK Zone 14", FMEName = "CGCS2000/GK6d-14_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(81m), XOff = 14500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_15 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_15", ArcGISName = "CGCS2000_GK_Zone_15", ArcGISPyName = "CGCS2000 GK Zone 15", FMEName = "CGCS2000/GK6d-15_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(87m), XOff = 15500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_16 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_16", ArcGISName = "CGCS2000_GK_Zone_16", ArcGISPyName = "CGCS2000 GK Zone 16", FMEName = "CGCS2000/GK6d-16_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(93m), XOff = 16500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_17 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_17", ArcGISName = "CGCS2000_GK_Zone_17", ArcGISPyName = "CGCS2000 GK Zone 17", FMEName = "CGCS2000/GK6d-17_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(99m), XOff = 17500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_18 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_18", ArcGISName = "CGCS2000_GK_Zone_18", ArcGISPyName = "CGCS2000 GK Zone 18", FMEName = "CGCS2000/GK6d-18_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(105m), XOff = 18500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_19 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_19", ArcGISName = "CGCS2000_GK_Zone_19", ArcGISPyName = "CGCS2000 GK Zone 19", FMEName = "CGCS2000/GK6d-19_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(111m), XOff = 19500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_20 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_20", ArcGISName = "CGCS2000_GK_Zone_20", ArcGISPyName = "CGCS2000 GK Zone 20", FMEName = "CGCS2000/GK6d-20_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(117m), XOff = 20500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_21 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_21", ArcGISName = "CGCS2000_GK_Zone_21", ArcGISPyName = "CGCS2000 GK Zone 21", FMEName = "CGCS2000/GK6d-21_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(123m), XOff = 21500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_22 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_22", ArcGISName = "CGCS2000_GK_Zone_22", ArcGISPyName = "CGCS2000 GK Zone 22", FMEName = "CGCS2000/GK6d-22_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(129m), XOff = 22500000, YOff = 0, };
        public static readonly IProjectionSystem CGCS2000_GK_Zone_23 = new ProjectionSystem() { IsArcGIS = true, IsFME = true, Name = "CGCS2000_GK_Zone_23", ArcGISName = "CGCS2000_GK_Zone_23", ArcGISPyName = "CGCS2000 GK Zone 23", FMEName = "CGCS2000/GK6d-23_FME", GeoCS = GCS_2000, CenterMeridian = new Angle(135m), XOff = 23500000, YOff = 0, };

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

        public static ICoordinateSystem FromString(string prj)
        {
            if (prj is null) return null;

            try
            {
                string[] segs = prj.Split('|');
                enumCSType csType = (enumCSType)int.Parse(segs[1]);

                if (csType == enumCSType.Geographic)
                {
                    string csName = segs[2];
                    string csAGName = segs[3];
                    string csAGPyName = segs[4];
                    string csFMEName = segs[5];
                    bool csIsAG = bool.Parse(segs[6]);
                    bool csIsFME = bool.Parse(segs[7]);

                    string datName = segs[8];
                    string datAGName = segs[9];
                    string datFMEName = segs[10];
                    bool datIsAG = bool.Parse(segs[11]);
                    bool datIsFME = bool.Parse(segs[12]);

                    string ellName = segs[13];
                    string ellAGName = segs[14];
                    string ellFMEName = segs[15];
                    bool ellIsAG = bool.Parse(segs[16]);
                    bool ellIsFME = bool.Parse(segs[17]);
                    double a = double.Parse(segs[18]);
                    double f = double.Parse(segs[19]);

                    ICoordinateSystem cs = GetCSFromPyName(csAGPyName);
                    if (cs != null) return cs;

                    Ellipsoid ell = new Ellipsoid
                    {
                        Name = ellName,
                        ArcGISName = ellAGName,
                        FMEName = ellFMEName,
                        IsArcGIS = ellIsAG,
                        IsFME = ellIsFME,
                        A = a,
                        F = f,
                    };

                    Datum dat = new Datum
                    {
                        Name = datName,
                        ArcGISName = datAGName,
                        FMEName = datFMEName,
                        IsArcGIS = datIsAG,
                        IsFME = datIsFME,
                    };

                    GeographicSystem gcs = new GeographicSystem
                    {
                        Name = csName,
                        ArcGISName = csAGName,
                        FMEName = csFMEName,
                        Datum = dat,
                        IsArcGIS = csIsAG,
                        IsFME = csIsFME,
                    };

                    return gcs as IGeographicSystem;
                }
                else
                {
                    string csName = segs[2];
                    string csAGName = segs[3];
                    string csAGPyName = segs[4];
                    string csFMEName = segs[5];
                    bool csIsAG = bool.Parse(segs[6]);
                    bool csIsFME = bool.Parse(segs[7]);

                    string csGeoName = segs[8];
                    string csGeoAGName = segs[9];
                    string csGeoAGPyName = segs[10];
                    string csGeoFMEName = segs[11];
                    bool csGeoIsAG = bool.Parse(segs[12]);
                    bool csGeoIsFME = bool.Parse(segs[13]);

                    string datName = segs[14];
                    string datAGName = segs[15];
                    string datFMEName = segs[16];
                    bool datIsAG = bool.Parse(segs[17]);
                    bool datIsFME = bool.Parse(segs[18]);

                    string ellName = segs[19];
                    string ellAGName = segs[20];
                    string ellFMEName = segs[21];
                    bool ellIsAG = bool.Parse(segs[22]);
                    bool ellIsFME = bool.Parse(segs[23]);
                    double a = double.Parse(segs[24]);
                    double f = double.Parse(segs[25]);

                    double l0 = double.Parse(segs[26]);
                    double b0 = double.Parse(segs[27]);
                    double h0 = double.Parse(segs[28]);
                    double x0 = double.Parse(segs[29]);
                    double y0 = double.Parse(segs[30]);
                    enumBandType bandType = (enumBandType)int.Parse(segs[31]);
                    int bandNum = int.Parse(segs[32]);

                    ICoordinateSystem cs = GetCSFromPyName(csAGPyName);
                    if (cs != null) return cs;

                    Ellipsoid ell = new Ellipsoid
                    {
                        Name = ellName,
                        ArcGISName = ellAGName,
                        FMEName = ellFMEName,
                        IsArcGIS = ellIsAG,
                        IsFME = ellIsFME,
                        A = a,
                        F = f,
                    };

                    Datum dat = new Datum
                    {
                        Name = datName,
                        ArcGISName = datAGName,
                        FMEName = datFMEName,
                        IsArcGIS = datIsAG,
                        IsFME = datIsFME,
                    };

                    GeographicSystem gcs = null;
                    if (csGeoIsAG) gcs = GetCSFromPyName(csGeoAGName) as GeographicSystem;

                    if (gcs == null)
                    {
                        gcs = new GeographicSystem
                        {
                            Name = csGeoName,
                            ArcGISName = csGeoAGName,
                            FMEName = csGeoFMEName,
                            Datum = dat,
                            IsArcGIS = csGeoIsAG,
                            IsFME = csGeoIsFME,
                        };
                    }

                    ICoordinateSystem pcs = new ProjectionSystem
                    {
                        Name = csName,
                        ArcGISName = csAGName,
                        ArcGISPyName = csAGPyName,
                        FMEName = csFMEName,

                        GeoCS = gcs,

                        CenterMeridian = new Angle(l0),
                        OriginLat = new Angle(b0),
                        XOff = x0,
                        YOff = y0,
                        H0 = h0,

                        BandType = bandType,
                        BandNum = bandNum,
                    };

                    return pcs;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
