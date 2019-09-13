using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 一个测站的所有观测数据
    /// </summary>
    public class OStation
    {
        public List<OFile> OFiles = new List<OFile>();

        public List<OEpoch> Epoches = new List<OEpoch>(2880*2);
    }
}
