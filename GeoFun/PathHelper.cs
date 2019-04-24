using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class PathHelper
    {
        /// <summary>
        /// 改变文件名加上指定后缀，例如c:\1.txt 后缀(_转换后) => c:\1_转换后.txt
        /// </summary>
        /// <param name="path"></param>
        /// <param name="append"></param>
        /// <returns></returns>
        public static string FileNameAppend(string path,string append)
        {
            if (string.IsNullOrWhiteSpace(path) || string.IsNullOrWhiteSpace(append)) return path;

            return string.Format("{0}\\{1}{2}{3}",Path.GetDirectoryName(path),Path.GetFileNameWithoutExtension(path),append,Path.GetExtension(path));
        }

        /// <summary>
        /// 改变文件夹，不改文件名
        /// </summary>
        /// <param name="pathSrc"></param>
        /// <param name="pathDst"></param>
        /// <returns></returns>
        public static string ChangeFolder(string pathSrc, string folder)
        {
            return Path.Combine(folder, Path.GetFileName(pathSrc));
        }

        public static bool CreateBaseFolder(string path)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(path))
                {
                    return true;
                }

                string baseFolder = Path.GetDirectoryName(path);
                if(!Directory.Exists(baseFolder))
                {
                    Directory.CreateDirectory(baseFolder);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
