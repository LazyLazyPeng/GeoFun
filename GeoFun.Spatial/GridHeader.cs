using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    public class GridHeader
    {
        /// <summary>
        /// 列数
        /// </summary>
        public int NCols { get; set; } = 0;

        /// <summary>
        /// 行数
        /// </summary>
        public int NRows { get; set; } = 0;

        /// <summary>
        /// 左上角X
        /// </summary>
        public double XllCorner { get; set; } = 0d;

        /// <summary>
        /// 左上角Y
        /// </summary>
        public double YllCorner { get; set; } = 0d;

        /// <summary>
        /// 格网大小
        /// </summary>
        public double CellSize { get; set; } = 1d;

        /// <summary>
        /// 无数值标识
        /// </summary>
        public double NoData { get; set; } = -99999d;

        public NSDTFHeader ToNSDTFHeader(string unit = "M",string alpha = "0.0",double hzoom=1d)
        {
            NSDTFHeader nheader = new NSDTFHeader();
            nheader.Unit = unit;
            nheader.Alpha = alpha;
            nheader.X0 = XllCorner;
            nheader.Y0 = YllCorner + CellSize * NRows;
            nheader.DX = CellSize;
            nheader.DY = CellSize;
            nheader.RowNum = NRows;
            nheader.ColNum = NCols;
            nheader.HZoom = 1d;
            return nheader;
        }
    }
}
