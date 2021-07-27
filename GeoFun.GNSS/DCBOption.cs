using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class DCBOption
    {
        /// <summary>
        /// DCB处理方式
        /// </summary>
        public enumDCBOption Option { get; set; } = enumDCBOption.Regardless;

        /// <summary>
        /// DCB文件路径
        /// </summary>
        public string DCBFilePath { get; set; } = "";

        public static DCBOption Estimate = new DCBOption
        {
            Option = enumDCBOption.Estimate,
            DCBFilePath = "",
        };
    }
}
