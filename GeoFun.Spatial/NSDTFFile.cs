using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Spatial
{
    public class NSDTFFile
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件头
        /// </summary>
        public NSDTFHeader Header { get; set; } = new NSDTFHeader();

        /// <summary>
        /// 文件内容
        /// </summary>
        public double[,] Data { get; set; } = null;

        public NSDTFFile(string path = "")
        {
            Path = path;
        }

        public bool Read()
        {
            if (!File.Exists(Path)) return false;

            try
            {
                string[] lines = File.ReadAllLines(Path);
                if (lines.Length < 13) return false;

                Header.DataMark = lines[0];
                Header.Version = lines[1];
                Header.Unit = lines[2];
                Header.Alpha = lines[3];
                Header.Compress = lines[4];

                Header.X0 = double.Parse(lines[5]);
                Header.Y0 = double.Parse(lines[6]);
                Header.DX = double.Parse(lines[7]);
                Header.DY = double.Parse(lines[8]);

                Header.RowNum = int.Parse(lines[9]);
                Header.ColNum = int.Parse(lines[10]);

                Header.HZoom = double.Parse(lines[11]);
                if (Header.HZoom == 0) Header.HZoom = 1;
                //// 压缩方法为0表示不压缩
                if (Math.Abs(double.Parse(Header.Compress)) > 1e-10) return false;

                //// 检查文件行数
                if (lines.Length < Header.RowNum + 13) return false;

                Data = new double[Header.RowNum, Header.ColNum];
                string[] segs;
                int row = 0, col = 0;
                int count = 0;
                for(int i = 13; i<lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;

                    segs = StringHelper.SplitFields(lines[i]);
                    for(int j = 0; j < segs.Length; j++)
                    {
                        row = (int)count / Header.ColNum;
                        col = count - Header.ColNum * row;
                        Data[row, col] = Double.Parse(segs[j]);
                        if (Data[row, col] != -99999)
                        {
                            Data[row, col] = Data[row, col] / Header.HZoom;
                        }
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

        public bool Write(string path = null)
        {
            if (string.IsNullOrWhiteSpace(path)) path = Path;
            if (string.IsNullOrWhiteSpace(path)) return false;
            if (!PathHelper.CreateBaseFolder(path)) return false;

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(Header.DataMark);
                    writer.WriteLine(Header.Version);
                    writer.WriteLine(Header.Unit);
                    writer.WriteLine(Header.Alpha);
                    writer.WriteLine(Header.Compress);
                    writer.WriteLine(Header.X0);
                    writer.WriteLine(Header.Y0);
                    writer.WriteLine(Header.DX);
                    writer.WriteLine(Header.DY);
                    writer.WriteLine(Header.RowNum);
                    writer.WriteLine(Header.ColNum);
                    writer.WriteLine(Header.HZoom);
                    writer.WriteLine("");

                    for (int i = 0; i < Data.GetLength(0); i++)
                    {
                        writer.Write(Data[i, 0]);
                        for (int j = 1; j < Data.GetLength(1); j++)
                        {
                            writer.Write(" ");
                            writer.Write(Data[i, j]);
                        }
                        writer.Write("\n");
                    }

                    writer.Close();
                    fs.Close();
                }
            }

            return true;
        }

        public GridFile ToESRI()
        {
            GridHeader header;
            if (!Header.ToGridHeader(out header))
            {
                return null;
            }

            GridFile file = new GridFile();
            file.Header = header;
            //Array.Copy(Data, file.Data, Data.Length);
            file.Data = Data;
            return file;
        }
    }
}
