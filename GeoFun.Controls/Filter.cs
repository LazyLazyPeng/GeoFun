using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.Controls
{
    public class Filter
    {
        /// <summary>
        /// 过滤字符串
        /// </summary>
        public string FilterStr { get; set; } = "所有文件(*.*)|*.*";

        /// <summary>
        /// 是否文件夹
        /// </summary>
        public bool IsFolder { get; set; } = false;

        public Filter()
        {

        }

        public Filter(string filterStr,bool isFolder = false)
        {
            FilterStr = filterStr;

            IsFolder = isFolder;
        }
    }
}
