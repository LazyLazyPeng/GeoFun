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
        public static List<string[]> ReadThenSplitLine(string path, char sp)
        {
            if (!File.Exists(path)) return null;

            List<string[]> lines = new List<string[]>();
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    if (sp == ' ')
                    {
                        string line = reader.ReadLine();
                        while (line != null)
                        {
                            line = StringHelper.RemoveRedundantBlank(line);
                            lines.Add(line.Split(new char[] { sp }));
                            line = reader.ReadLine();
                        }
                    }
                    else
                    {
                        string line = reader.ReadLine();
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
    }
}
