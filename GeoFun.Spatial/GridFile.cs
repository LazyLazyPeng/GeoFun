using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    public class GridFile
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// 文件头
        /// </summary>
        public GridHeader Header { get; set; } = new GridHeader();

        /// <summary>
        /// 文件内容
        /// </summary>
        public double[,] Data { get; set; } = null;

        /// <summary>
        /// 格网大小
        /// </summary>
        public double CellSize
        {
            get
            {
                return Header.CellSize;
            }
            set
            {
                Header.CellSize = value;
            }
        }

        public GridFile(string path = "")
        {
            Path = path;
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <returns></returns>
        public bool Write(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path)) path = Path;

            if (!PathHelper.CreateBaseFolder(path))
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ncols {0}", Header.NCols);
            sb.AppendFormat("nrows {0}", Header.NRows);
            sb.AppendFormat("xllcorner {0}", Header.XllCorner);
            sb.AppendFormat("yllcorner {0}", Header.YllCorner);
            sb.AppendFormat("cellsize {0}", Header.CellSize);
            sb.AppendFormat("NODATA_value {0}", Header.NoData);

            for (int i = 0; i < Header.NRows; i++)
            {
                sb.AppendFormat("{0}", Data[i, 0]);
                for (int j = 1; j < Header.NCols; j++)
                {
                    sb.AppendFormat(" {0}", Data[i, j]);
                }
                sb.Append("\n");
            }

            File.WriteAllText(Path, sb.ToString());
            return true;
        }

        /// <summary>
        /// 转换为NSDTF文件
        /// </summary>
        /// <returns></returns>
        public NSDTFFile ToNSDTF(string unit = "M", string alpha = "0.0", double hzoom = 1d)
        {
            NSDTFFile file = new NSDTFFile();
            file.Header = Header.ToNSDTFHeader(unit, alpha, hzoom);

            file.Data = Data;
            if (Math.Abs(hzoom - 1d) < 1e-12)
            {
                for(int i = 0; i < file.Data.GetLength(0);i++)
                {
                    for(int j = 0;j<file.Data.GetLength(1);j++)
                    {
                        file.Data[i, j] = file.Data[i, j] / hzoom;
                    }
                }
            }

            return file;
        }
    }
}
