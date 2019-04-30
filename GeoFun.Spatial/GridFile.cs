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

        public bool Read()
        {
            if (!File.Exists(Path)) return false;

            try
            {
                string[] lines = File.ReadAllLines(Path);
                if (lines.Length < 6) return false;

                Header.NCols = int.Parse(lines[0].Split()[1]);
                Header.NRows = int.Parse(lines[1].Split()[1]);
                Header.XllCorner = double.Parse(lines[2].Split()[1]);
                Header.YllCorner = double.Parse(lines[3].Split()[1]);
                Header.CellSize = double.Parse(lines[4].Split()[1]);

                Header.NoData = double.Parse(lines[5].Split()[1]);

                //// 检查文件行数
                if (lines.Length < Header.NRows + 5) return false;

                Data = new double[Header.NRows, Header.NCols];
                string[] segs;
                int row = 0, col = 0;
                int count = 0;
                for (int i = 6; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;

                    segs = StringHelper.SplitFields(lines[i]);
                    for (int j = 0; j < segs.Length; j++)
                    {
                        
                        row = (int)count / Header.NCols;
                        col = count - Header.NCols * row;
                        Data[row, col] = Double.Parse(segs[j]);
                        count++;
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
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
            sb.AppendFormat("ncols {0}\n", Header.NCols);
            sb.AppendFormat("nrows {0}\n", Header.NRows);
            sb.AppendFormat("xllcorner {0}\n", Header.XllCorner);
            sb.AppendFormat("yllcorner {0}\n", Header.YllCorner);
            sb.AppendFormat("cellsize {0}\n", Header.CellSize);
            sb.AppendFormat("NODATA_value {0}\n", Header.NoData);

            for (int i = 0; i < Header.NRows; i++)
            {
                sb.AppendFormat("{0}", Data[i, 0]);
                for (int j = 1; j < Header.NCols; j++)
                {
                    sb.AppendFormat(" {0}", Data[i, j]);
                }
                sb.Append("\n");
            }

            File.WriteAllText(path, sb.ToString());
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

            for (int i = 0; i < file.Data.GetLength(0); i++)
            {
                for (int j = 0; j < file.Data.GetLength(1); j++)
                {
                    if(file.Data[i, j] != -99999)
                        file.Data[i, j] = file.Data[i, j] * hzoom;
                }
            }

            return file;
        }
    }
}
