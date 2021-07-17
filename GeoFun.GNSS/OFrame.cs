using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 一个观测时段，例如0:00-02:00
    /// 包含观测时段内的所有观测值
    /// </summary>
    public class OFrame
    {
        public GPST StartTime;
        public GPST EndTime;
        public GPST CenterTime;

        /// <summary>
        /// 本时段所有的观测弧段
        /// </summary>
        public Dictionary<string, List<OArc>> Arcs = new Dictionary<string, List<OArc>>();
    }
}
