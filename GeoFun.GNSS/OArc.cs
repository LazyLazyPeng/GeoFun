using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class OArc
    {
        /// <summary>
        /// 卫星编号
        /// </summary>
        public string PRN { get; set; } = "";

        /// <summary>
        /// 弧段的长度
        /// </summary>
        public int Length
        {
            get
            {
                if (StartIndex < 0 || EndIndex < 0) return 0;
                else if (StartIndex == EndIndex) return 0;
                else return EndIndex - StartIndex + 1;
            }
        }

        /// <summary>
        /// 开始索引
        /// </summary>
        public int StartIndex { get; set; } = -1;

        /// <summary>
        /// 结束索引
        /// </summary>
        public int EndIndex { get; set; } = -1;

        /// <summary>
        /// 该观测弧段对应的测站
        /// </summary>
        public OStation Station { get; set; }

        public OSat this[int index]
        {
            get
            {
                return Station.Epoches[StartIndex + index][PRN];
            }
        }
    }
}
