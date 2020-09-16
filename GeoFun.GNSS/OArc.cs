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
                if (Station is null)
                {
                    return File.Epoches[StartIndex].Epoch;
                }
                else
                {
                    return Station.Epoches[StartIndex].Epoch;
                }
            }
        }

        /// <summary>
        /// 弧段结束时间
        /// </summary>
        public GPST EndTime
        {
            get
            {
                if (File is null)
                {
                    return Station.Epoches[EndIndex].Epoch;
                }
                else
                {
                    return File.Epoches[EndIndex].Epoch;
                }
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
                if (File is null)
                {
                    return Station.Epoches[StartIndex + index][PRN];
                }
                else
                {
                    return File.Epoches[StartIndex + index][PRN];
                }
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
            File = arc.File;
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
            arc1.File = File;
            arc1.Station = Station;
            arc1.StartIndex = StartIndex;
            arc1.EndIndex = StartIndex + index - 1;

            // 第2段[index,end]
            OArc arc2 = new OArc();
            arc2.Station = Station;
            arc2.File = File;
            arc2.PRN = PRN;
            arc2.StartIndex = StartIndex + index;
            arc2.EndIndex = EndIndex;

            // EndIndex = StartIndex + (index - 1 < 0 ? 0 : index - 1);

            return new OArc[] { arc1, arc2 };
        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public OArc Copy()
        {
            return new OArc(this);
        }

        public static List<OArc> Detect(OStation sta, string prn, int minArcLen = -1)
        {
            if (sta is null) return null;
            if (minArcLen < 0) minArcLen = Options.ARC_MIN_LENGTH;

            List<OArc> arcs = new List<OArc>();

            int start = -1;
            double l1, l2, p1, p2, c1;
            for (int i = 0; i < sta.EpochNum; i++)
            {
                bool flag = true;
                if (!sta.Epoches[i].AllSat.ContainsKey(prn)) flag = false;
                else
                {
                    l1 = sta.Epoches[i][prn]["L1"];
                    l2 = sta.Epoches[i][prn]["L2"];
                    p1 = sta.Epoches[i][prn]["P1"];
                    p2 = sta.Epoches[i][prn]["P2"];
                    c1 = sta.Epoches[i][prn]["C1"];

                    if (l1 == 0 || l2 == 0 || p2 == 0) flag = false;
                    else if (p1 == 0 && c1 == 0) flag = false;
                    else if (sta.Epoches[i][prn].Outlier) flag = false;
                    else if (sta.Epoches[i][prn].CycleSlip) flag = false;
                }

                if (flag)
                {
                    // 弧段继续
                    if (start > -1) continue;
                    // 弧段开始
                    else
                    {
                        start = i;
                    }
                }
                else
                {
                    // 弧段结束
                    if (start > -1)
                    {
                        if (i - start > minArcLen)
                        {
                            OArc arc = new OArc();
                            arc.StartIndex = start + 10;
                            arc.EndIndex = i - 1 - 10;
                            arc.Station = sta;
                            arc.PRN = prn;
                            arcs.Add(arc);
                        }
                        start = -1;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return arcs;
        }
    }
}
