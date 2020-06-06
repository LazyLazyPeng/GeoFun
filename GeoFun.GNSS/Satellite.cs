using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Satellite
    {
        /// <summary>
        /// prn编号(字符串),例如G01,E01
        /// </summary>
        public string PRN { get; set; } = "";

        /// <summary>
        /// PRN编号(数字),例如01
        /// </summary>
        public int PRNNum { get; set; } = 01;

        /// <summary>
        /// 卫星系统
        /// </summary>
        public enumGNSSSystem System = enumGNSSSystem.GPS;
    }
}
