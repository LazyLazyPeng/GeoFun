using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class PRNCode
    {
        public enumGNSSSystem System { get; set; } = enumGNSSSystem.GPS;

        public int Num { get; set; } = 1;

        public string PRN { get; set; }
    }
}
