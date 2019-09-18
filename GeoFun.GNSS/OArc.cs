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
        /// 弧段开始时间
        /// </summary>
        public GPST StartTime
        {
            get
            {
                return Station.Epoches[StartIndex].Epoch;
            }
        }

        /// <summary>
        /// 弧段结束时间
        /// </summary>
        public GPST EndTime
        {
            get
            {
                return Station.Epoches[EndIndex].Epoch;
            }
        }

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

        /// <summary>
        /// 将弧段以index为界分为2段,index为第2段的起始索引
        /// </summary>
        /// <param name="index">第二段开始的索引</param>
        /// <returns></returns>
        public OArc Split(int index)
        {
            if (index < 0 || index >= Length) return null;

            // 第2段[index,end]
            OArc arc = new OArc();
            arc.Station = Station;
            arc.PRN = PRN;
            arc.StartIndex = StartIndex+index;
            arc.EndIndex = EndIndex;

            EndIndex = StartIndex + (index - 1 < 0 ? 0 : index - 1);

            return arc;
        }
    }
}
