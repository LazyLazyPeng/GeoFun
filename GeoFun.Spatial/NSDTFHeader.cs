using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    /** 头文件示例
    NSDTF-DEM 
    1.0 
    M 
    0.000000 
    0.000000 
    39512435.000000
    2743120.000000
    5.000000 
    5.000000 
    985 
    1328 
    100
     */
    public class NSDTFHeader
    {
        /// <summary>
        /// 文件标识
        /// </summary>
        public string DataMark { get; set; } = "NSDTF-DEM";
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "1.0";
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 方向角
        /// </summary>
        public string Alpha { get; set; }
        /// <summary>
        /// 压缩方法 0-不压缩 1-游程编码
        /// </summary>
        public string Compress { get; set; } = "0.0";
        /// <summary>
        /// 左上角X
        /// </summary>
        public double X0 { get; set; }
        /// <summary>
        /// 左上角Y
        /// </summary>
        public double Y0 { get; set; }
        /// <summary>
        /// X分辨率
        /// </summary>
        public double DX { get; set; }
        /// <summary>
        /// Y分辨率
        /// </summary>
        public double DY { get; set; }
        /// <summary>
        /// 行数
        /// </summary>
        public int RowNum { get; set; }
        /// <summary>
        /// 列数
        /// </summary>
        public int ColNum { get; set; }
        /// <summary>
        /// 高程放大倍数
        /// </summary>
        public double HZoom { get; set; } = 1d;

        /// <summary>
        /// 转换为ArcGIS Grid文件头
        /// </summary>
        /// <param name="gheader"></param>
        /// <returns></returns>
        public bool ToGridHeader(out GridHeader gheader)
        {
            gheader = new GridHeader();

            //// x和y方向分辨率不一致
            if (Math.Abs(DX - DY) > 1e-10) return false;

            double x0 = X0;
            double y0 = Y0;

            //// 将单位转换为米/度
            double cellSize = DX;
            switch (Unit.Trim().ToUpper())
            {
                case "K":
                    x0 *= 1000d;
                    y0 *= 1000d;
                    break;
                case "S":
                    x0 = Angle.DMS2DD(x0);
                    y0 = Angle.DMS2DD(y0);
                    break;
                default:
                    break;
            }
            y0 = y0 - cellSize * RowNum;

            gheader.NCols = ColNum;
            gheader.NRows = RowNum;
            gheader.XllCorner = x0;
            gheader.YllCorner = y0;
            gheader.CellSize = DX;
            gheader.NoData = -99999;

            return true;
        }
    }
}
