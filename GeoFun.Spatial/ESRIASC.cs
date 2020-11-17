using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    /// <summary>
    /// ESRI ASCII文件格式，只能存储二维数据，且格网分辨率在两个维度要一致
    /// </summary>
    public class ESRIASC : IRasterFile
    {
        /// <summary>
        /// 列数
        /// </summary>
        public uint ColNum { get; set; } = 0;
        /// <summary>
        /// 行数
        /// </summary>
        public uint RowNum { get; set; } = 0;

        /// <summary>
        /// 左下角(LowerLeft)格网中心点坐标X
        /// </summary>
        public double XLLCenter { get; set; } = 0;
        /// <summary>
        /// 左下角(LowerLeft)格网中心点坐标Y
        /// </summary>
        public double YLLCenter { get; set; } = 0;

        /// <summary>
        /// 格网分辨率
        /// </summary>
        public double CellSize { get; set; } = 0;

        /// <summary>
        /// NoData的值(默认为-9999)
        /// </summary>
        public double NodataValue { get; set; } = -9999;

        /// <summary>
        /// 保存路径
        /// </summary>
        public string Path { get; set; }

        private double[,] data = null;
        /// <summary>
        /// 数据
        /// </summary>
        public double[,] Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                if (data != null)
                {
                    RowNum = (uint)data.GetLength(0);
                    ColNum = (uint)data.GetLength(1);
                }
                else
                {
                    RowNum = 0;
                    ColNum = 0;
                }
            }
        }

        public ESRIASC() : this(0, 0)
        { }

        public ESRIASC(uint rowNum, uint colNum)
        {
            if (rowNum < 0 || colNum < 0)
            {
                throw new ArgumentException();
            }

            RowNum = rowNum;
            ColNum = colNum;

            if (rowNum == 0 || colNum == 0) return;

            Data = new double[rowNum, colNum];
        }

        public ESRIASC(int rowNum, int colNum) : this((uint)rowNum, (uint)colNum)
        {

        }

        public void Write()
        {
            WriteAs(Path);
        }

        public void WriteAs(string otherPath)
        {
            if (string.IsNullOrWhiteSpace(otherPath))
            {
                throw new ArgumentException("路径错误，无法写入:" + otherPath);
            }

            PathHelper.CreateBaseFolder(otherPath);
            using (FileStream fs = new FileStream(otherPath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(string.Format("NCOLS {0}", ColNum));
                    writer.WriteLine(string.Format("NROWS {0}", RowNum));
                    writer.WriteLine(string.Format("XLLCENTER {0}", XLLCenter));
                    writer.WriteLine(string.Format("YLLCENTER {0}", YLLCenter));
                    writer.WriteLine(string.Format("CELLSIZE {0}", CellSize));
                    writer.WriteLine(string.Format("NODATA_VALUE {0}", NodataValue));

                    for (int i = 0; i < RowNum; i++)
                    {
                        writer.Write(Data[i, 0]);
                        for (int j = 1; j < ColNum; j++)
                        {
                            writer.Write(' ');
                            writer.Write(Data[i, j]);
                        }
                        writer.Write("\r\n");
                    }
                    writer.Close();
                    fs.Close();
                }
            }

        }
    }
}
