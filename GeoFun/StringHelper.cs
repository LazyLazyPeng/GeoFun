using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GeoFun
{
    public class StringHelper
    {
        /// <summary>
        /// 将多个空格/制表符替换成一个空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveRedundantBlank(string str)
        {
            string line = str.Replace('\r', ' ');
            line = line.Replace('\t', ' ');
            line = Regex.Replace(str.Trim(), "\\s+", " ");
            return line;
        }

        /// <summary>
        /// 将文本按行分割,换行符\n
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitLines(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return new string[] { };
            }

            return str.Split('\n');
        }

        public static string[] SplitFields(string line, char spliter = ' ')
        {
            line = RemoveRedundantBlank(line);

            return line.Split(spliter);
        }

        public static string CombineFields(string[] fields, char spliter = ' ')
        {
            if (fields == null || fields.Count() == 0) return "";

            StringBuilder sb = new StringBuilder();

            ////这一段代码与下面一段等效
            ////return fields.Aggregate((str1, str2) => str1+" "+str2);

            foreach (var field in fields)
            {
                sb.AppendFormat("{0}{1}", spliter, field);
            }

            string line = sb.ToString();

            if (line.Length > 0)
            {
                line = line.Substring(1);
            }

            return line;
        }
    }
}
