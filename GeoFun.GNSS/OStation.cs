using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using GeoFun.MathUtils;
using System.Collections;
using System.Text.RegularExpressions;
using System.ComponentModel;
using GeoFun.IO;
using MathNet.Numerics.LinearAlgebra.Solvers;

namespace GeoFun.GNSS
{
    /// <summary>
    /// 一个测站的所有观测数据
    /// </summary>
    public class OStation
    {
        public List<DOY> DOYs = new List<DOY>();

        public DOY StartDOY
        {
            get
            {
                if (DOYs is null||DOYs.Count<=0) return null;
                return DOYs.Min();
            }
        }
        public DOY EndDOY
        {
            get
            {
                if (DOYs is null || DOYs.Count <= 0) return null;
                return DOYs.Max();
            }
        }

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
        public double[] ApproxPos { get; set; } = new double[3];

        /// <summary>
        /// 测站法向量
        /// </summary>
        public double[] ApproxPosNEU { get; set; } = new double[3];

        public int FileNum
        {
            get
            {
                return OFiles.Count;
            }
        }

        public List<OFile> OFiles = new List<OFile>();
        public List<OEpoch> Epoches = new List<OEpoch>(2880 * 2);

        public int EpochNum
        {
            get
            {
                if (Epoches is null) return 0;
                return Epoches.Count;
            }
        }

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
            Epoches = new List<OEpoch>(OFiles.Sum(o => o.Epoches.Count));
            foreach (var o in OFiles)
            {
                o.StartIndex = Epoches.Count;
                Epoches.AddRange(o.Epoches);
            }
        }

        /// <summary>
        /// 读取文件夹下该测站的所有观测文件(rinex2版本)
        /// </summary>
        /// <param name="dir"></param>
        public void ReadAllObs()
        {
            foreach (var oFile in OFiles)
            {
                try
                {
                    if (oFile.TryRead())
                    {
                        if (Math.Abs(oFile.Header.approxPos.X) > 1e3)
                        {
                            ApproxPos[0] = oFile.Header.approxPos.X;
                            ApproxPos[1] = oFile.Header.approxPos.Y;
                            ApproxPos[2] = oFile.Header.approxPos.Z;
                        }
                    }
                    else
                    {
                        Common.msgBox.Print("文件读取失败,路径为:" + oFile.Path);
                    }
                }
                catch (Exception ex)
                {
                    Common.msgBox.Print(string.Format("文件读取失败！\n路径为:{0}\n 原因是:{1}",
                        oFile.Path, ex.ToString()));
                    //// todo:读取失败
                }
            }
        }

        /// <summary>
        /// 在文件夹中查找该测站所有O文件
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public int SearchAllOFiles(string folder)
        {
            if (string.IsNullOrEmpty(Name)) return 0;
            if (!Directory.Exists(folder)) return 0;

            int fileCount = 0;
            DirectoryInfo dir = new DirectoryInfo(folder);
            string searchPattern = string.Format("{0}???*.??o",Name);
            foreach(var file in dir.GetFiles(searchPattern))
            {
                OFiles.Add(new OFile(file.FullName));
                fileCount++;
            }

            return fileCount;
        }
        
        public void GetStationDOY()
        {
            DOYs = new List<DOY>();
            foreach (var file in OFiles)
            {
                DOYs.Add(new DOY(file.Year, file.DOY));
            }

            //// DOY排序
            DOYs.Sort();
        }
        /// <summary>
        /// 检查数据DOY是否连续
        /// </summary>
        public bool CheckDOY()
        {
            bool flag = true;
            if (DOYs.Count < 2) return true; 
            for (int i = 1; i < DOYs.Count; i++)
            {
                //// 判断doy是否连续
                // 1.不跨年
                if (DOYs[i].Year == DOYs[i - 1].Year)
                {
                    if (DOYs[i].Day - DOYs[i - 1].Day <= 1)
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }

                // 2.跨年
                else if (DOYs[i].Year - DOYs[i - 1].Year > 1)
                {
                    flag = false;
                    break;
                }
                else
                {
                    if (DOYs[i].Day != 1)
                    {
                        flag = false;
                        break;
                    }

                    // 闰年最后一天
                    if (DateTime.IsLeapYear(DOYs[i - 1].Year)
                        && DOYs[i - 1].Day == 366)
                    {
                        continue;
                    }
                    // 非闰年最后一天
                    else if (DateTime.IsLeapYear(DOYs[i - 1].Year)
                        && DOYs[i - 1].Day == 365)
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        break;
                    }
                }
            }

            return flag;
        }

        /// <summary>
        /// 检查测站数据是否连续
        /// </summary>
        /// <returns></returns>
        public bool CheckDataConsist()
        {
            GetStationDOY();
            return CheckDOY();
        }

        public void CalVTEC()
        {
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    OArc arc = Arcs[prn][i];

                    for (int j = 0; j < arc.Length; j++)
                    {
                        if (Math.Abs(arc[j]["SP4"]) > 1e-10)
                        {
                            arc[j]["SP4"] = Iono.STEC2VTEC(9.52437 * arc[j]["SP4"], arc[j].Elevation);
                        }
                    }
                }
            }
        }

        public void DetectArcs()
        {
            for (int i = 0; i < 32; i++)
            {
                string prn = string.Format("G{0:0#}", i + 1);
                List<OArc> arcList = OArc.Detect(this, prn);
                Arcs.Add(prn, arcList);
            }
        }

        public void DetectCycleSlip()
        {
            for (int k = 0; k < Arcs.Keys.Count; k++)
            {
                string prn = Arcs.Keys.ElementAt(k);

                //// 旧的弧段
                Stack<OArc> oldArcs = new Stack<OArc>();
                //// 探测后新的弧段
                List<OArc> newArcs = new List<OArc>();

                for (int i = Arcs[prn].Count - 1; i >= 0; i--)
                {
                    oldArcs.Push(Arcs[prn][i].Copy());
                }

                int index = -1;
                OArc arc = null;
                while (oldArcs.Count() > 0)
                {
                    arc = oldArcs.Pop();
                    if (ObsHelper.DetectCycleSlip(ref arc, out index))
                    {
                        // 根据返回周跳的索引将弧段分为2段
                        OArc[] arcs = arc.Split(index);

                        // 前一段加入已检测的列表
                        if (arcs[0].Length >= Options.ARC_MIN_LENGTH)
                        {
                            newArcs.Add(arcs[0]);
                        }

                        // 后一段加入未检测列表
                        if (arcs[1].Length >= Options.ARC_MIN_LENGTH)
                        {
                            oldArcs.Push(arcs[1]);
                        }
                    }
                    else
                    {
                        newArcs.Add(arc);
                    }
                }

                Arcs[prn] = newArcs;
            }
        }

        public void DetectClockJump() { }

        public void DetectOutlier()
        {
            if (Epoches is null) return;

            double c1 = 0d, p1 = 0d, p2 = 0d;
            for (int i = 0; i < Epoches.Count; i++)
            {
                var epoch = Epoches[i];
                foreach (var prn in epoch.AllSat.Keys)
                {
                    OSat sat = epoch.AllSat[prn];
                    p2 = sat["P2"];
                    p1 = sat["P1"];
                    c1 = sat["C1"];

                    //// 检查P1P2
                    if (p1 != 0d && p2 != 0d)
                    {
                        if (Math.Abs(p1 - p2) > Options.OUTLIER_P1P2) sat.Outlier = true;
                    }

                    //// 检查P1C1
                    else if (p1 != 0 && c1 != 0)
                    {
                        if (Math.Abs(p1 - c1) > Options.OUTLIER_P1C1) sat.Outlier = true;
                    }
                }
            }
        }

        public void CalP4L4()
        {
            OArc arc = null;
            double p1 = 0d, p2 = 0d, l1 = 0d, l2 = 0d;

            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    // 取某颗卫星的一个观测弧段
                    arc = Arcs[prn][i];

                    for (int j = 0; j < arc.Length; j++)
                    {
                        p1 = arc[j].SatData["P1"];
                        p2 = arc[j].SatData["P2"];
                        l1 = arc[j].SatData["L1"];
                        l2 = arc[j].SatData["L2"];
                        if (p1 == 0d) p1 = arc[j].SatData["C1"];

                        if (p1 != 0d || p2 != 0d)
                        {
                            arc[j]["P4"] = p2 - p1;
                        }
                        else
                        {
                            arc[j]["P4"] = 0;
                        }

                        if (l1 != 0 && l2 != 0)
                        {
                            arc[j]["L4"] = l2 - l1;
                        }
                        else
                        {
                            arc[j]["L4"] = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 计算相位平滑伪距值
        /// 对于30s采样率的
        /// </summary>
        /// <remarks>
        /// 参考 任晓东. 多系统GNSS电离层TEC高精度建模及差分码偏差精确估计[D].
        /// </remarks>
        public void SmoothP41()
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
                        arc[j].SatData.Add("SP4", arc[j]["L4"] - p4l4);
                    }
                }
            }
        }

        /// <summary>
        /// 计算相位平滑伪距值
        /// </summary>
        public void SmoothP4()
        {
            ObsHelper.CalL4(ref Epoches);
            ObsHelper.CalP4(ref Epoches);

            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    OArc arc = Arcs[prn][i];
                    Smoother.SmoothP4ByL4(ref arc);
                }
            }
        }

        public void CalAzElIPP()
        {
            if (Arcs is null) return;
            // 测站概略坐标错误，需要估计
            if (Math.Abs(ApproxPos[0]) < 1e-1
             || Math.Abs(ApproxPos[1]) < 1e-1
             || Math.Abs(ApproxPos[2]) < 1e-1) return;

            double recb, recl, rech;
            Coordinate.XYZ2BLH(ApproxPos, out recb, out recl, out rech, Ellipsoid.ELLIP_WGS84);

            double az, el, ippb, ippl;
            foreach (var prn in Arcs.Keys)
            {
                for (int i = 0; i < Arcs[prn].Count; i++)
                {
                    OArc arc = Arcs[prn][i];
                    for (int j = 0; j < arc.Length; j++)
                    {
                        az = 0d;
                        el = 0d;
                        ippb = 0d;
                        ippl = 0d;

                        MathHelper.CalAzEl(ApproxPos, arc[j].SatCoor, out az, out el);
                        MathHelper.CalIPP(recb, recl, Common.EARTH_RADIUS2, Common.IONO_HIGH, az, el, out ippb, out ippl);

                        arc[j].Azimuth = az;
                        arc[j].Elevation = el;
                        arc[j].IPP[0] = ippb;
                        arc[j].IPP[1] = ippl;

                        if (ippb < 0)
                        {
                            int b = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 预处理(周跳，钟跳探测)
        /// </summary>
        public void Preprocess()
        {
            DetectOutlier();
            DetectArcs();
            DetectCycleSlip();
            //DetectClockJump();
            //DetectCycleSlip();
            CalP4L4();
            SmoothP4();
        }

        public void Fit()
        {
            foreach (var prn in Arcs.Keys)
            {
                var arcs = Arcs[prn];
                for (int i = 0; i < arcs.Count; i++)
                {
                    var arc = arcs[i];

                    Fit(ref arc, 20, 2);
                    //Smoother.Smooth(ref arc, "SP4", 5);
                }
            }
        }
        public void Fit(ref OArc arc)
        {
            List<int> index = new List<int>();
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for (int i = 0; i < arc.Length; i++)
            {
                if (Math.Abs(arc[i]["SP4"]) > 1e-3)
                {
                    index.Add(i);

                    x.Add(i);
                    y.Add(arc[i]["SP4"]);
                }
            }

            PolynomialModel pm = PolynomialModel.Fit(x, y, 3);

            for (int i = 0; i < x.Count; i++)
            {
                arc[index[i]]["SP4"] -= pm.CalFit(x[i]);
            }
        }
        public void DoubleDiff()
        {
            foreach (var prn in Arcs.Keys)
            {
                var arcs = Arcs[prn];
                for (int i = 0; i < arcs.Count; i++)
                {
                    var arc = arcs[i];

                    ObsHelper.CalDoubleDiff(ref arc);
                }
            }

        }

        /// <summary>
        /// 对弧段进行滑动窗口拟合
        /// </summary>
        /// <param name="arc"></param>
        /// <param name="length">窗口的长度 单位:历元</param>
        /// <param name="order">多项式拟合的阶数</param>
        public void Fit(ref OArc arc, int length, int order)
        {
            int si = 0;
            int ei = si + length;
            double sp4 = 0d;
            PolynomialModel pm = new PolynomialModel();
            while (si < arc.Length)
            {
                if (ei > arc.Length) ei = arc.Length;

                // 只有一个历元
                if (si == ei)
                {
                    arc[si]["SP4"] = 0d;
                    break;
                }

                List<double> x = new List<double>();
                List<double> y = new List<double>();
                List<int> index = new List<int>();
                for (int i = si; i < ei; i++)
                {
                    sp4 = arc[i]["SP4"];
                    if (Math.Abs(sp4) > 1e-10)
                    {
                        x.Add(i);
                        y.Add(sp4);
                        index.Add(i);
                    }
                }

                double sigma;
                List<double> residue;
                if (pm.Fit(x, y, out residue, out sigma))
                {
                    for (int i = 0; i < x.Count; i++)
                    {
                        // 剔粗差
                        if (Math.Abs(residue[i]) < 5 * sigma && Math.Abs(residue[i]) < 2.5)
                        {
                            arc[index[i]]["SP4"] = residue[i];
                        }
                        else
                        {
                            arc[index[i]]["SP4"] = 0d;
                        }
                    }
                }

                si = ei;
                ei += length;
            }
        }

        public void WriteMeas(string folder, int epoNum)
        {
            // start time(st)
            GPST st = new GPST(Epoches[0].Epoch);
            // end time(et)
            GPST et = new GPST(Epoches[0].Epoch);

            // start index(si) end index(ei)
            int si = 0, ei = si;
            while (si < EpochNum)
            {
                ei = si + epoNum;
                if (ei >= EpochNum) ei = EpochNum - 1;

                st = Epoches[si].Epoch;
                et = Epoches[ei].Epoch;

                List<string[]> lines = new List<string[]>();
                for (int i = 0; i < ei; i++)
                {
                    foreach (var sat in Epoches[i].AllSat.Values)
                    {
                        if (Math.Abs(sat["SP4"]) < 1e-10) continue;

                        lines.Add(new string[]
                        {
                            sat.SatPRN,
                            (sat.IPP[1]*Angle.R2D).ToString("#.##########"),
                            (sat.IPP[0]*Angle.R2D).ToString("#.##########"),
                            sat["SP4"].ToString("f4")
                        });
                    }
                }

                string fileName = Name + string.Format(".{0}{1:0#}{2:0#}{3:0#}{4:0#}{5:##.#}.{6}{7:0#}{8:0#}{9:0#}{10:0#}{11:##.#}.tec",
                    st.CommonT.Year,
                    st.CommonT.Month,
                    st.CommonT.Day,
                    st.CommonT.Hour,
                    st.CommonT.Minute,
                    st.CommonT.Second,
                    et.CommonT.Year,
                    et.CommonT.Month,
                    et.CommonT.Day,
                    et.CommonT.Hour,
                    et.CommonT.Minute,
                    et.CommonT.Second
                    );
                string filePath = Path.Combine(folder, fileName);
                FileHelper.WriteLines(filePath, lines, ',');

                si += epoNum;
            }
        }

        public void WriteTEC(string folder)
        {
            var sta = this;
            {
                List<string[]> lines = new List<string[]>();
                List<string[]> linesB = new List<string[]>();
                List<string[]> linesL = new List<string[]>();

                for (int i = 0; i < sta.EpochNum; i++)
                {
                    string[] line = new string[32];
                    string[] lineb = new string[32];
                    string[] linel = new string[32];
                    for (int j = 0; j < 32; j++)
                    {
                        line[j] = "0.0000000000";
                        lineb[j] = "0.0000000000";
                        linel[j] = "0.0000000000";
                    }

                    foreach (var prn in sta.Epoches[i].AllSat.Keys)
                    {
                        // 去掉高度角小于15°的
                        if (sta.Epoches[i][prn].Elevation < 30 * Angle.D2R)
                        {
                            continue;
                        }

                        if (!prn.StartsWith("G")) continue;
                        int index = int.Parse(prn.Substring(1)) - 1;
                        line[index] = sta.Epoches[i].AllSat[prn]["SP4"].ToString("#.##########");
                        lineb[index] = (sta.Epoches[i].AllSat[prn].IPP[1] * Angle.R2D).ToString("#.##########");
                        linel[index] = (sta.Epoches[i].AllSat[prn].IPP[0] * Angle.R2D).ToString("#.##########");
                    }
                    lines.Add(line);
                    linesB.Add(lineb);
                    linesL.Add(linel);
                }

                string fileName = sta.Name + ".meas.p4.txt";
                string filePath = Path.Combine(folder, fileName);
                FileHelper.WriteLines(filePath, lines, ',');

                string fileNameB = sta.Name + ".ipp.b.txt";
                string fileNameL = sta.Name + ".ipp.l.txt";

                string filePathB = Path.Combine(folder, fileNameB);
                string filePathL = Path.Combine(folder, fileNameL);

                FileHelper.WriteLines(filePathB, linesB, ',');
                FileHelper.WriteLines(filePathL, linesL, ',');
            }

        }

        public void WriteMeas(string folder, string meas)
        {
            var sta = this;
            {
                List<string[]> lines = new List<string[]>();
                List<string[]> linesB = new List<string[]>();
                List<string[]> linesL = new List<string[]>();

                for (int i = 0; i < sta.EpochNum; i++)
                {
                    string[] line = new string[32];
                    string[] lineb = new string[32];
                    string[] linel = new string[32];
                    for (int j = 0; j < 32; j++)
                    {
                        line[j] = "0.0000000000";
                        lineb[j] = "0.0000000000";
                        linel[j] = "0.0000000000";
                    }

                    foreach (var prn in sta.Epoches[i].AllSat.Keys)
                    {
                        // 去掉高度角小于15°的
                        if (sta.Epoches[i][prn].Elevation < 30 * Angle.D2R)
                        {
                            continue;
                        }

                        if (!prn.StartsWith("G")) continue;
                        int index = int.Parse(prn.Substring(1)) - 1;
                        line[index] = sta.Epoches[i].AllSat[prn][meas].ToString("#.##########");
                        lineb[index] = (sta.Epoches[i].AllSat[prn].IPP[1] * Angle.R2D).ToString("#.##########");
                        linel[index] = (sta.Epoches[i].AllSat[prn].IPP[0] * Angle.R2D).ToString("#.##########");
                    }
                    lines.Add(line);
                    linesB.Add(lineb);
                    linesL.Add(linel);
                }

                string fileName = string.Format("{0}.meas.{1}.txt", sta.Name, meas.ToLower());
                string filePath = Path.Combine(folder, fileName);
                FileHelper.WriteLines(filePath, lines, ',');

                string fileNameB = sta.Name + ".ipp.b.txt";
                string fileNameL = sta.Name + ".ipp.l.txt";

                string filePathB = Path.Combine(folder, fileNameB);
                string filePathL = Path.Combine(folder, fileNameL);

                FileHelper.WriteLines(filePathB, linesB, ',');
                FileHelper.WriteLines(filePathL, linesL, ',');
            }


        }
    }
}
