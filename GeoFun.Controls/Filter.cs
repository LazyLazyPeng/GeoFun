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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterStr">
        /// 过滤字符串,可以是一个，也可以是多个,例如：
        ///   shp文件(*.shp)|*.shp
        ///   shp文件(*.shp)|*.shp|dwg文件(*.dwg)|*.dwg
        /// </param>
        /// <param name="isFolder">是否是一个文件夹(有的文件格式以文件夹的方式存储,例如gdb或coverage)</param>
        public Filter(string filterStr,bool isFolder = false)
        {
            FilterStr = filterStr;
            IsFolder = isFolder;
        }
    }
}
