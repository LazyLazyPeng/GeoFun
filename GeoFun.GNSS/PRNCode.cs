using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class PRNCode
    {
        // 系统类型
        public enumGNSSSystem system { get; set; } = enumGNSSSystem.GPS;

        private int prnNum = 1;
        // 卫星编号数字
        public int PRNNum
        {
            get
            {
                return prnNum;
            }
            set
            {
                prnNum = value;
            }
        }

        private string prnString = "G01";
        // 卫星编号字符
        public string PRNString
        {
            get
            {
                return prnString;
            }
            set 
            {
                prnString = value;
            }
        }
    }
}
