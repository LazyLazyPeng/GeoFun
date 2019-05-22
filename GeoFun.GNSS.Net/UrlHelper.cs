using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS.Net
{
    public class UrlHelper
    {
        public static string GetHost(string url)
        {
            int index = url.Replace("//", "__").IndexOf("/");
            if(index<0)
            {
                return "";
            }
            return url.Substring(0, index+1);
        }

        /// <summary>
        /// 获取ftp文件的相对路径
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetRelPath(string fullPath)
        {
            if (fullPath is null) return null;
            if (!fullPath.StartsWith("ftp://"))return null;

            int index = fullPath.Substring(6).IndexOf("/");
            if (index < 0) return null;
            return fullPath.Substring(index + 7);
        }
    }
}
