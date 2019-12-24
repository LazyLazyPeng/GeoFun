using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 一个测站的所有观测数据
    /// </summary>
    public class OStation
    {
        /// <summary>
        /// 采样率(s)
        /// </summary>
        public double Interval { get; set; } = 30;

        /// <summary>
        /// 观测数据开始时间
        /// </summary>
        public GPST StartTime { get; set; }

        /// <summary>
        /// 测站名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 概略位置
        /// </summary>
        public Coor3 ApproxPos { get; set; } = new Coor3();

        /// <summary>
        /// 测站法向量
        /// </summary>
        public double[] ApproxPosNEU { get; set; } = new double[3];

        public List<OFile> OFiles = new List<OFile>();
        public List<OEpoch> Epoches = new List<OEpoch>(2880 * 2);

        /// <summary>
        /// 测站上所有卫星的所有观测弧段
        /// </summary>
        public Dictionary<string, List<OArc>> Arcs { get; set; } = new Dictionary<string, List<OArc>>();

        public OStation(string name = "")
        {
            Name = name;
        }

        /// <summary>
        /// 将读取到的O文件按照时间顺序排列
        /// </summary>
        public void SortObs()
        {
            if (OFiles.Count <= 0) return;

            /// 初步检查数据时间是否一致
            //else if (OFiles.Min(o => o.Year) != OFiles.Max(o => o.Year))
            //{
            //    throw new Exception("测站数据跨年，暂时无法处理，测站为:" + OFiles[0].Header.markName);
            //}

            //// 检查数据采样率是否一致
            else if (Math.Abs(OFiles.Min(o => o.Interval) - OFiles.Max(o => o.Interval)) > 0.001)
            {
                throw new Exception("测站数据采样率不一致，测站为:" + Name);
            }

            //// 将Ｏ文件按照观测起始时间排序(未检测文件重复的情况，会出错)
            OFiles = (from o in OFiles
                      orderby o.StartTime.mjd.Seconds
                      select o).ToList();

            //// 将观测历元合并到一个大的数组
            Epoches = new List<OEpoch>(OFiles.Sum(o => o.AllEpoch.Count));
            foreach (var o in OFiles)
            {
                o.StartIndex = Epoches.Count;
                Epoches.AddRange(o.AllEpoch);
            }
        }

        /// <summary>
        /// 读取文件夹下该测站的所有观测文件(rinex2版本)
        /// </summary>
        /// <param name="dir"></param>
        public void ReadAllObs(DirectoryInfo dir)
        {
            var files = dir.GetFiles(string.Format("{0}???*.??o", Name));
            foreach (var file in files)
            {
                try
                {
                    OFile oFile = new OFile(file.FullName);
                    if (oFile.TryRead())
                    {
                        OFiles.Add(oFile);
                    }
                    else
                    {
                        Console.WriteLine("文件读取失败,路径为:" + file.FullName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("文件读取失败！\n路径为:{0}\n 原因是:{1}",
                        file.FullName, ex.ToString()));
                    //// todo:读取失败
                }
            }
        }

        public void ReadAllObs(string dir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            ReadAllObs(dirInfo);
        }

        public void DetectArcs()
        {
            if (Epoches is null || Epoches.Count <= 0) return;

            string prn = "";
            double p1, p2, l1, l2;

            //// 弧段已经开始但尚未结束的卫星编号
            List<OArc> startedArc = new List<OArc>();
            List<OArc> endedArc = new List<OArc>();

            bool flag = true;
            for (int i = 0; i < Epoches.Count; i++)
            {
                if (Epoches[i].Epoch.SecondsOfYear < Options.START_TIME.SecondsOfYear || Epoches[i].Epoch.SecondsOfYear > Options.END_TIME.SecondsOfYear) continue;

                foreach (var arc in startedArc)
                {
                    flag = true;

                    //// Some MEAS missed
                    if (!Epoches[i].PRNList.Contains(arc.PRN)) flag = false;

                    else
                    {
                        //// Cycle slip
                        if (Epoches[i][arc.PRN].CycleSlip) flag = false;

                        //// Outlier
                        else if (Epoches[i][arc.PRN].Outlier) flag = false;

                        if (!Epoches[i][arc.PRN].SatData.TryGetValue("P2", out p2)) flag = false;
                        if (!Epoches[i][arc.PRN].SatData.TryGetValue("L1", out l1)) flag = false;
                        if (!Epoches[i][arc.PRN].SatData.TryGetValue("L2", out l2)) flag = false;

                        if (!Epoches[i][arc.PRN].SatData.TryGetValue("P1", out p1)) flag = false;
                        else if (Math.Abs(p1) < 0.001)
                        {
                            if (!Epoches[i][arc.PRN].SatData.TryGetValue("C1", out p1)) flag = false;
                        }

                        if (Math.Abs(p1) < 1e-3) flag = false;
                        if (Math.Abs(p2) < 1e-3) flag = false;
                        if (Math.Abs(l1) < 1e-3) flag = false;
                        if (Math.Abs(l2) < 1e-3) flag = false;
                    }

                    //// An arc is end
                    if (!flag)
                    {
                        arc.EndIndex = i - 1;

                        endedArc.Add(arc);
                    }
                }

                foreach (var arc in endedArc)
                {
                    startedArc.Remove(arc);

                    if (arc.Length >= Options.ARC_MIN_LENGTH)
                    {
                        if (!Arcs.ContainsKey(arc.PRN))
                        {
                            Arcs.Add(arc.PRN, new List<OArc>());
                        }

                        Arcs[arc.PRN].Add(arc);
                    }
                }

                endedArc.Clear();

                for (int j = 0; j < Epoches[i].PRNList.Count; j++)
                {
                    prn = Epoches[i].PRNList[j];

                    if (!prn.StartsWith("G")) continue;

                    //// Check if some meas missing
                    if (!Epoches[i][prn].SatData.TryGetValue("P2", out p2)) continue;
                    if (!Epoches[i][prn].SatData.TryGetValue("L1", out l1)) continue;
                    if (!Epoches[i][prn].SatData.TryGetValue("L2", out l2)) continue;

                    if (!Epoches[i][prn].SatData.TryGetValue("P1", out p1)) flag = false;
                    else if (Math.Abs(p1) < 0.001)
                    {
                        if (!Epoches[i][prn].SatData.TryGetValue("C1", out p1)) flag = false;
                    }

                    if (Math.Abs(p1) < 1e-3) flag = false;
                    else if (Math.Abs(p2) < 1e-3) flag = false;
                    else if (Math.Abs(l1) < 1e-3) flag = false;
                    else if (Math.Abs(l2) < 1e-3) flag = false;

                    //// A new arc start
                    if (!startedArc.Any(o => o.PRN == prn))
                    {
                        OArc arc = new OArc();
                        arc.StartIndex = i;
                        arc.PRN = prn;
                        arc.Station = this;
                        startedArc.Add(arc);
                    }
                }
            }
        }

        public void DetectCycleSlip()
        {
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    OArc arc = Arcs[prn][i];
                    Observation.DetectCycleSlip(ref arc);
                }
            }
        }

        public void DetectClockJump() { }

        public void DetectOutlier()
        {
            if (Arcs is null) return;

            double c1 = 0d, p1 = 0d, p2 = 0d;
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    for (int j = 0; j < Arcs[prn][i].Length; j++)
                    {
                        //// 检查P1P2
                        if (Arcs[prn][i][j].SatData.TryGetValue("P1", out p1) && Arcs[prn][i][j].SatData.TryGetValue("P2", out p2))
                        {
                            if (Math.Abs(p1 - p2) > Options.OUTLIER_P1P2) Arcs[prn][i][j].Outlier = true;
                        }

                        //// 检查P1C1
                        else if (Arcs[prn][i][j].SatData.TryGetValue("P1", out p1) && Arcs[prn][i][j].SatData.TryGetValue("C1", out c1))
                        {
                            if (Math.Abs(p1 - c1) > Options.OUTLIER_P1P2) Arcs[prn][i][j].Outlier = true;
                        }
                    }
                }
            }
        }

        public void CalP4L4()
        {
            OArc arc = null;
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    // 取某颗卫星的一个观测弧段
                    arc = Arcs[prn][i];

                    for (int j = 0; j < arc.Length; j++)
                    {
                        if (!arc[j].SatData.ContainsKey("P4"))
                        {
                            arc[j].SatData.Add("P4", arc[j]["P1"] - arc[j]["P2"]);
                        }
                        else
                        {
                            arc[j]["P4"] = arc[j]["P1"] - arc[j]["P2"];
                        }

                        if (!arc[j].SatData.ContainsKey("L4"))
                        {
                            arc[j].SatData.Add("L4", arc[j]["L1"] - arc[j]["L2"]);
                        }
                        else
                        {
                            arc[j]["L4"] = arc[j]["L1"] - arc[j]["L2"];
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算相位平滑伪距值
        /// 对于30s采样率的
        /// </summary>
        public void SmoothP4()
        {
            double f1 = Common.GPS_F1;
            double f2 = Common.GPS_F2;

            OArc arc = null;
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    // 取某颗卫星的一个观测弧段
                    arc = Arcs[prn][i];

                    // 整个弧段P4+L4的均值<P4+L4>
                    double p4l4 = 0;
                    for (int j = 1; j < arc.Length; j++)
                    {
                        p4l4 = p4l4 * j / (j + 1) + arc[j]["P4"] / (j + 1);
                    }

                    // 平滑P4
                    for (int j = 0; j < arc.Length; j++)
                    {
                        arc[j].SatData.Add("SP4", arc[j]["P4"] + p4l4);
                    }
                }
            }
        }

        /// <summary>
        /// 预处理(周跳，钟跳探测)
        /// </summary>
        public void Preprocess()
        {
            DetectArcs();
            DetectClockJump();
            //DetectCycleSlip();
            DetectOutlier();
            CalP4L4();
            SmoothP4();
        }
    }
}
