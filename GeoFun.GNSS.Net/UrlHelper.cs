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
    }
}
