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

        public OFile File { get; set; }

        public OSat this[int index]
        {
            get
            {
                return File.AllEpoch[StartIndex + index][PRN];
            }
        }

        public OArc() { }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <param name="arc"></param>
        public OArc(OArc arc)
        {
            PRN = arc.PRN;
            Station = arc.Station;
            StartIndex = arc.StartIndex;
            EndIndex = arc.EndIndex;
        }

        /// <summary>
        /// 将弧段以index为界分为2段,index为第2段的起始索引
        /// </summary>
        /// <param name="index">第二段开始的索引</param>
        /// <returns></returns>
        public OArc[] Split(int index)
        {
            if (index < 0 || index >= Length) return null;

            // 第一段
            OArc arc1 = new OArc();
            arc1.PRN = PRN;
            arc1.Station = Station;
            arc1.StartIndex = StartIndex;
            arc1.EndIndex = StartIndex+index - 1;

            // 第2段[index,end]
            OArc arc2 = new OArc();
            arc2.Station = Station;
            arc2.PRN = PRN;
            arc2.StartIndex = StartIndex + index;
            arc2.EndIndex = EndIndex;

            // EndIndex = StartIndex + (index - 1 < 0 ? 0 : index - 1);

            return new OArc[] {arc1,arc2 };
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public OArc Copy()
        {
            return new OArc(this);
        }
    }
}
