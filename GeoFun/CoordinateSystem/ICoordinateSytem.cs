using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;

namespace GeoFun.CoordinateSystem
{
    public interface ICoordinateSystem
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 坐标系类型
        /// </summary>
        enumCSType CSType { get; }

        /// <summary>
        /// 基准
        /// </summary>
        Datum Datum { get; set; }

        /// <summary>
        /// 椭球
        /// </summary>
        Ellipsoid Ellipsoid { get; set; }

        /// <summary>
        /// 是否是ArcGIS内置的坐标系
        /// </summary>
        bool IsArcGIS { get; set; }

        /// <summary>
        /// ArcGIS坐标系名称
        /// </summary>
        string ArcGISName { get; set; }

        /// <summary>
        /// arcgis python中坐标系名称
        /// </summary>
        string ArcGISPyName { get; set; }

        /// <summary>
        /// 是否是FME内置坐标系
        /// </summary>
        bool IsFME { get; set; }
        /// <summary>
        /// FME坐标系名称
        /// </summary>
        string FMEName { get; set; }

        /// <summary>
        /// 转换为ESRI字符串
        /// </summary>
        /// <returns></returns>
        string ToESRIString();
        /// <summary>
        /// 转换为FME字符串
        /// </summary>
        /// <returns></returns>
        string ToFMEString();

        string ToString();

        void WritePrj(string path);
    }


}
