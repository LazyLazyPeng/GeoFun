using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.IO
{
    public class FileHelper
    {
        /// <summary>
        /// 按行读取文本文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns>行数组(List),行末没有'\n'</returns>
        public static List<string> ReadLines(string path)
        {
            if (!File.Exists(path)) return null;

            List<string> lines = new List<string>();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        lines.Add(line);
                        line = reader.ReadLine();
                    }

                    reader.Close();
                    fs.Close();
                }
            }

            return lines;
        }

        /// <summary>
        /// 按行读取文本文件，每一行根据字符分割成数组
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sp"></param>
        /// <returns>行数组(List)，每行为字符串数组(string[])</returns>
        /// <remarks>如果分隔符是空格，那么首先会移除多余的空格，及将多个空格替换为1个空格</remarks>
        public static List<string[]> ReadThenSplitLine(string path, char sp, int skipNum = 0)
        {
            if (!File.Exists(path)) return null;

            List<string[]> lines = new List<string[]>();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    string line;
                    for (int i = 0; i < skipNum; i++)
                    {
                        line = reader.ReadLine();
                        continue;
                    }

                    if (sp == ' ')
                    {
                        line = reader.ReadLine();
                        while (line != null)
                        {
                            line = StringHelper.RemoveRedundantBlank(line);
                            lines.Add(line.Split(new char[] { sp }));
                            line = reader.ReadLine();
                        }
                    }
                    else
                    {
                        line = reader.ReadLine();
                        while (line != null)
                        {
                            lines.Add(line.Split(new char[] { sp }));
                            line = reader.ReadLine();
                        }
                    }

                    reader.Close();
                    fs.Close();
                }
            }

            return lines;
        }

        /// <summary>
        /// 将行数组写入文本文件
        /// </summary>
        /// <param name="path">文件路径，文件夹不存在时会自动尝试创建</param>
        /// <param name="lines">行数组</param>
        /// <param name="sp">分隔符</param>
        /// <returns></returns>
        public static bool WriteLines(string path, List<string[]> lines, char sp)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i] == null)
                {
                    sb.Append("\r\n");
                    continue;
                }

                sb.Append(lines[i][0]);
                for (int j = 1; j < lines[i].Length; j++)
                {
                    sb.Append(sp);
                    sb.Append(lines[i][j]);
                }
                sb.Append("\r\n");
            }

            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());

                    writer.Close();
                    fs.Close();
                }
            }

            return true;
        }

        public static void WriteMatrix(string path,double[,] data,string[] format=null,char seperator=',')
        {
            using(FileStream fs = new FileStream(path,FileMode.Create,FileAccess.Write))
            {
                using(StreamWriter writer = new StreamWriter(fs,Encoding.ASCII))
                {
                    StringBuilder line = null;
                    int rowMax = data.GetLength(0);
                    int colMax = data.GetLength(1);

                    if (format != null && format.Length >= colMax)
                    {
                        for (int i = 0; i < rowMax; i++)
                        {
                            line = new StringBuilder();
                            for (int j = 0; j < colMax; j++)
                            {
                                line.AppendFormat(format[j],data[i,j]); ;
                            }
                            line.Append("\n");

                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 复制文件，输出文件夹不存在时尝试创建
        /// </summary>
        /// <param name="pathSrc">源文件路径</param>
        /// <param name="pathDst">目标文件路径</param>
        public static void CopyTo(string pathSrc, string pathDst)
        {
            if (!File.Exists(pathSrc))
            {
                throw new FileNotFoundException("找不到源文件:" + pathSrc);
            }

            // 创建父文件夹(递归)
            if (!Directory.Exists(Path.GetDirectoryName(pathDst)))
            {
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(pathDst));
                }
                catch (Exception ex)
                {
                    throw new DirectoryNotFoundException("输出文件夹不存在,尝试创建时失败!路径为:" + pathDst, ex);
                }
            }

            // 复制模板文件到输出路径
            try
            {
                File.Copy(pathSrc, pathDst, true);
            }
            catch (Exception ex)
            {
                throw new IOException(string.Format("无法复制文件到路径,源文件为:{0},目标文件为:{1}", pathSrc, pathDst), ex);
            }

            if (!File.Exists(pathDst))
            {
                throw new IOException(string.Format("无法复制文件到路径,源文件为:{0},目标文件为:{1}", pathSrc, pathDst));
            }
        }

        /// <summary>
        /// 扫描文件夹下所有指定后缀名的文件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="exts">后缀列表</param>
        /// <param name="option">搜索选项，默认会搜索子文件夹</param>
        /// <returns></returns>
        public static List<FileInfo> ScanFilesOfExtension(string folder, List<string> exts, SearchOption option = SearchOption.AllDirectories)
        {
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder)) return new List<FileInfo>();
            if (exts is null || exts.Count() <= 0) return new List<FileInfo>();

            List<FileInfo> files = new List<FileInfo>();
            try
            {
                // 转成小写
                List<string> extensions = new List<string>();
                foreach (var ext in exts)
                {
                    if (ext is null || ext.Trim() is null)
                    {
                        extensions.Add("");
                    }

                    if (!ext.StartsWith("."))
                    {
                        extensions.Add("." + ext.ToLower());
                    }
                    else
                    {
                        extensions.Add(ext.ToLower());
                    }
                }


                DirectoryInfo dir = new DirectoryInfo(folder);
                foreach (var file in dir.GetFiles("*", option))
                {
                    string ext = file.Extension;
                    if (!extensions.Contains(ext)) continue;

                    files.Add(file);
                }
            }
            catch (Exception ex)
            {
            }

            return files;
        }

        /// <summary>
        /// 扫描文件夹及子文件夹下所有指定后缀名的文件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <param name="ext">后缀列表</param>
        /// <param name="option">搜索选项，默认会搜索子文件夹</param>
        /// <returns></returns>
        public static List<FileInfo> ScanFilesOfExtension(string folder, string ext, SearchOption option = SearchOption.AllDirectories)
        {
            if (string.IsNullOrWhiteSpace(folder) || !Directory.Exists(folder)) return new List<FileInfo>();

            if (ext is null) ext = "";
            else if (string.IsNullOrWhiteSpace(ext.Trim())) ext = "";
            else ext = ext.ToLower();

            List<FileInfo> files = new List<FileInfo>();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(folder);
                foreach (var file in dir.GetFiles("*", option))
                {
                    string fileExt = file.Extension;
                    if (!string.IsNullOrWhiteSpace(fileExt))
                    {
                        fileExt = fileExt.ToLower();
                    }

                    if (fileExt == ext) continue;

                    files.Add(file);
                }
            }
            catch (Exception ex)
            {
            }

            return files;
        }
    }
}
