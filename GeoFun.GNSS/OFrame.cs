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
        public Dictionary<string, Dictionary<string, List<OArc>>> Arcs = new Dictionary<string, Dictionary<string, List<OArc>>>();

        public static OFrame FromStations(GPST startT, GPST endT, GPST CenterT,List<OStation> ostations)
        {
            OFrame frame = new OFrame();
            frame.StartTime = startT;
            frame.EndTime = endT;
            frame.CenterTime = CenterT;
            foreach(var station in ostations)
            {
                if (station.Epoches is null || station.Epoches.Count <= 0) continue;
                if (station.Epoches[0].Epoch > endT ||
                    station.Epoches[station.Epoches.Count - 1].Epoch < startT) continue;

                frame.Arcs.Add(station.Name, new Dictionary<string, List<OArc>>());
                foreach(var kv in station.Arcs)
                {
                    var prn = kv.Key;
                    var arcs = kv.Value;
                    foreach(var arc in arcs)
                    {
                        if (arc.StartTime > endT || arc.EndTime < startT) continue;
                        else
                        {
                            OArc arcFrame = new OArc(arc);
                            if(arc.StartTime<startT)
                            {
                                for(int i = 0; i < arc.Length; i++)
                                {
                                    if(arc[i].Epoch>startT)
                                    {
                                        arcFrame.StartIndex = i;
                                        break;
                                    }
                                }
                            }
                            if(arc.EndTime>endT)
                            {
                                for(int i = arc.Length-1; i >=0; i--)
                                {
                                    if(arc[i].Epoch<endT)
                                    {
                                        arcFrame.EndIndex = i;
                                        break;
                                    }
                                }
                            }

                            if(!frame.Arcs[station.Name].ContainsKey(prn))
                            {
                                frame.Arcs[station.Name].Add(prn, new List<OArc>());
                            }
                            frame.Arcs[station.Name][prn].Add(arcFrame);
                        }
                    }
                }
            }

            return frame;
        }
    }
}
