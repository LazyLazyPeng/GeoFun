using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Common
    {
        /// <summary>
        /// 地球半径
        /// </summary>
        public static readonly double EARTH_RADIUS1 = 6378137d;

        /// <summary>
        /// 地球平均半径
        /// </summary>
        public static readonly double EARTH_RADIUS2 = 6371000d;

        /// <summary>
        /// 电离层单层模型高度
        /// </summary>
        public static readonly double IONO_HIGH = 450000d;

        /// <summary>
        /// 光速(m/s,来源于gLAB)
        /// </summary>
        public static readonly double SPEED_OF_LIGHT = 299792458.0;
        public static readonly double C0 = SPEED_OF_LIGHT;

        /// <summary>
        /// 磁北极纬度(弧度)
        /// </summary>
        public static readonly double GEOMAGNETIC_POLE_LAT = 78.3 * Angle.D2R;

        /// <summary>
        /// 磁北极经度(弧度)
        /// </summary>
        public static readonly double GEOMAGENTIC_POLE_LON = 291d * Angle.D2R;

        /// <summary>
        /// GPS基频
        /// </summary>
        public static readonly double GPS_F0 = 10.23e6;

        /// <summary>
        /// GPS倍频数
        /// </summary>
        public static readonly int GPS_MF1 = 154;
        public static readonly int GPS_MF2 = 120;
        public static readonly int GPS_MF5 = 120;
        public static readonly int GPS_MFw = GPS_MF1 - GPS_MF2;
        /// <summary>
        /// GAL倍频数
        /// </summary>
        public static readonly int GAL_MF1 = 154;
        public static readonly int GAL_MF5a = 115;
        public static readonly int GAL_MF5b = 118;
        public static readonly double GAL_MF5 = 116.5;
        public static readonly int GAL_MF6 = 125;

        /// <summary>
        /// GPS频率
        /// </summary>
        public static readonly double GPS_F1 = GPS_MF1 * GPS_F0;
        public static readonly double GPS_F2 = GPS_MF2 * GPS_F0;
        public static readonly double GPS_F5 = GPS_MF5 * GPS_F0;
        public static readonly double GPS_Fw = GPS_MFw * GPS_F0;
        /// <summary>
        /// GAL频率
        /// </summary>
        public static readonly double GAL_F1 = GAL_MF1 * GPS_F0;
        public static readonly double GAL_F5a = GAL_MF5a * GPS_F0;
        public static readonly double GAL_F5b = GAL_MF5b * GPS_F0;
        public static readonly double GAL_F5 = GAL_MF5 * GPS_F0;

        /// <summary>
        /// GPS波长
        /// </summary>
        public static readonly double GPS_L1 = SPEED_OF_LIGHT / GPS_F1;
        public static readonly double GPS_L2 = SPEED_OF_LIGHT / GPS_F2;
        public static readonly double GPS_L5 = SPEED_OF_LIGHT / GPS_F5;
        public static readonly double GPS_Lw = SPEED_OF_LIGHT / GPS_Fw;
        /// <summary>
        /// GAL波长
        /// </summary>
        public static readonly double GAL_L1 = SPEED_OF_LIGHT / GAL_F1;
        public static readonly double GAL_L5a = SPEED_OF_LIGHT / GAL_F5a;
        public static readonly double GAL_L5b = SPEED_OF_LIGHT / GAL_F5b;
        public static readonly double GAL_L5 = SPEED_OF_LIGHT / GAL_F5;
    }
}
