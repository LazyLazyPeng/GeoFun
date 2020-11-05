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
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.Data.Matlab;

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
                if (DOYs is null || DOYs.Count <= 0) return null;
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

            //// 检查数据采样率是否一致
            else if (Math.Abs(OFiles.Min(o => o.Interval) - OFiles.Max(o => o.Interval)) > 0.001)
            {
                throw new Exception("测站数据采样率不一致，测站为:" + Name);
            }

            //// 将Ｏ文件按照观测起始时间排序(未检测文件重复的情况，会出错)
            OFiles = (from o in OFiles
                      orderby o.DOY
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
            string searchPattern = string.Format("{0}???*.??o", Name);
            foreach (var file in dir.GetFiles(searchPattern))
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

            // 检查数据某天是否缺失
            for (int i = 1; i < DOYs.Count; i++)
            {
                int dayNum = DOYs[i] - DOYs[i - 1];
                // 有缺失
                if (dayNum > 1)
                {
                    for (int j = 0; j < dayNum - 1; j++)
                    {
                        DOY doy = DOYs[i - 1].AddDays(j + 1);
                        OFile file = OFile.CreateEmptyFile(Name, doy.Year, doy.Day, 30);
                        OFiles.Add(file);
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
                            arc[j]["vtec"] = Iono.STEC2VTEC(9.52437 * arc[j]["SP4"], arc[j].Elevation);
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
            if (Epoches is null) return;
            // 测站概略坐标错误，需要估计
            if (Math.Abs(ApproxPos[0]) < 1e-1
             || Math.Abs(ApproxPos[1]) < 1e-1
             || Math.Abs(ApproxPos[2]) < 1e-1) return;

            double recb, recl, rech;
            Coordinate.XYZ2BLH(ApproxPos, out recb, out recl, out rech, Ellipsoid.ELLIP_WGS84);

            double az, el, ippb, ippl;
            for (int i = 0; i < Epoches.Count; i++)
            {
                var epo = Epoches[i];
                foreach (var prn in epo.AllSat.Keys)
                {
                    var sat = epo[prn];
                    az = 0d;
                    el = 0d;
                    ippb = 0d;
                    ippl = 0d;

                    MathHelper.CalAzEl(ApproxPos, sat.SatCoor, out az, out el);
                    MathHelper.CalIPP(recb, recl, Common.EARTH_RADIUS2, Common.IONO_HIGH, az, el, out ippb, out ippl);

                    sat.Azimuth = az;
                    sat.Elevation = el;
                    sat.IPP[0] = ippl;
                    sat.IPP[1] = ippb;
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
        public void Smooth()
        {
            foreach (var prn in Arcs.Keys)
            {
                var arcs = Arcs[prn];
                for (int i = 0; i < arcs.Count; i++)
                {
                    var arc = arcs[i];

                    Smoother.Smooth(ref arc, "vtec", 29);
                }
            }
        }
        public void ROTI()
        {
            foreach (var prn in Arcs.Keys)
            {
                var arcs = Arcs[prn];
                for (int i = 0; i < arcs.Count; i++)
                {
                    var arc = arcs[i];

                    ObsHelper.CalROTI(ref arc);
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
                    arc[si]["vtec"] = 0d;
                    break;
                }

                List<double> x = new List<double>();
                List<double> y = new List<double>();
                List<int> index = new List<int>();
                for (int i = si; i < ei; i++)
                {
                    sp4 = arc[i]["vtec"];
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
                            arc[index[i]]["vtec"] = residue[i];
                        }
                        else
                        {
                            arc[index[i]]["vtec"] = 0d;
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
                            sat["vtec"].ToString("f4")
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
                        line[index] = sta.Epoches[i].AllSat[prn]["vtec"].ToString("#.##########");
                        lineb[index] = (sta.Epoches[i].AllSat[prn].IPP[0] * Angle.R2D).ToString("#.##########");
                        linel[index] = (sta.Epoches[i].AllSat[prn].IPP[1] * Angle.R2D).ToString("#.##########");
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
            //var matLon = new DenseMatrix(EpochNum, 32);
            //var matLat = new DenseMatrix(EpochNum, 32);
            //var matP1 = new DenseMatrix(EpochNum, 32);
            //var matP2 = new DenseMatrix(EpochNum, 32);
            //var matL1 = new DenseMatrix(EpochNum, 32);
            //var matL2 = new DenseMatrix(EpochNum, 32);
            //var matP4 = new DenseMatrix(EpochNum, 32);

            List<string[]> lines = new List<string[]>();
            List<string[]> linesB = new List<string[]>();
            List<string[]> linesL = new List<string[]>();

            for (int i = 0; i < EpochNum; i++)
            {
                string[] line = new string[38];
                string[] lineb = new string[38];
                string[] linel = new string[38];
                for (int j = 0; j < 38; j++)
                {
                    line[j] = "0.0000000000";
                    lineb[j] = "0.0000000000";
                    linel[j] = "0.0000000000";
                }

                line[0] = Epoches[i].Epoch.CommonT.Year.ToString();
                line[1] = Epoches[i].Epoch.CommonT.Month.ToString("0#");
                line[2] = Epoches[i].Epoch.CommonT.Day.ToString("0#");
                line[3] = Epoches[i].Epoch.CommonT.Hour.ToString("0#");
                line[4] = Epoches[i].Epoch.CommonT.Minute.ToString("0#");
                line[5] = Epoches[i].Epoch.CommonT.Second.ToString("0#.##########");

                lineb[0] = Epoches[i].Epoch.CommonT.Year.ToString();
                lineb[1] = Epoches[i].Epoch.CommonT.Month.ToString("0#");
                lineb[2] = Epoches[i].Epoch.CommonT.Day.ToString("0#");
                lineb[3] = Epoches[i].Epoch.CommonT.Hour.ToString("0#");
                lineb[4] = Epoches[i].Epoch.CommonT.Minute.ToString("0#");
                lineb[5] = Epoches[i].Epoch.CommonT.Second.ToString("0#.##########");

                linel[0] = Epoches[i].Epoch.CommonT.Year.ToString();
                linel[1] = Epoches[i].Epoch.CommonT.Month.ToString("0#");
                linel[2] = Epoches[i].Epoch.CommonT.Day.ToString("0#");
                linel[3] = Epoches[i].Epoch.CommonT.Hour.ToString("0#");
                linel[4] = Epoches[i].Epoch.CommonT.Minute.ToString("0#");
                linel[5] = Epoches[i].Epoch.CommonT.Second.ToString("0#.##########");

                lines.Add(line);
                linesB.Add(lineb);
                linesL.Add(linel);
            }

            OSat sat;
            string prn;
            int rowIndex;
            int colIndex;
            foreach (var item in Arcs)
            {
                prn = item.Key;
                foreach (var arc in item.Value)
                {
                    for (int i = 0; i < arc.Length; i++)
                    {
                        sat = arc[i];

                        rowIndex = i + arc.StartIndex;
                        colIndex = int.Parse(prn.Substring(1)) - 1 + 6;

                        lines[rowIndex][colIndex] = sat[meas].ToString("#.##########");
                        linesB[rowIndex][colIndex] = (sat.IPP[1] * Angle.R2D).ToString("#.##########");
                        linesL[rowIndex][colIndex] = (sat.IPP[0] * Angle.R2D).ToString("#.##########");

                        //matP1[i, index] = sat["P1"];
                        //matP2[i, index] = sat["P2"];
                        //matL1[i, index] = sat["L1"];
                        //matL2[i, index] = sat["L2"];
                        //matP4[i, index] = sat["SP4"];
                        //matLat[i, index] = sat.IPP[0] * Angle.R2D;
                        //matLon[i, index] = sat.IPP[1] * Angle.R2D;
                    }
                }

                string fileName = string.Format("{0}.meas.{1}.txt", Name, meas.ToLower());
                string filePath = Path.Combine(folder, fileName);
                FileHelper.WriteLines(filePath, lines, ',');

                string fileNameB = Name + ".ipp.b.txt";
                string fileNameL = Name + ".ipp.l.txt";

                string filePathB = Path.Combine(folder, fileNameB);
                string filePathL = Path.Combine(folder, fileNameL);

                FileHelper.WriteLines(filePathB, linesB, ',');
                FileHelper.WriteLines(filePathL, linesL, ',');

                //List<MatlabMatrix> mats = new List<MatlabMatrix>
                //{
                //    MatlabWriter.Pack(matP1,"p1"),
                //    MatlabWriter.Pack(matP2,"p2"),
                //    MatlabWriter.Pack(matL1,"l1"),
                //    MatlabWriter.Pack(matL2,"l2"),
                //    MatlabWriter.Pack(matP1,"p4"),
                //    MatlabWriter.Pack(matLat,"lat"),
                //    MatlabWriter.Pack(matLon,"lon"),
                //};

                //MatlabWriter.Store(Path.Combine(folder, Name + ".mat"), mats);
            }
        }


        public void WriteMeas1(string folder, string meas)
        {
            double[,] data = new double[EpochNum, 38];
            double[,] dataB = new double[EpochNum, 38];
            double[,] dataL = new double[EpochNum, 38];

            string[] format = new string[38];
            format[0] = "{0:0000}";
            format[1] = ",{0:00}";
            format[2] = ",{0:00}";
            format[3] = ",{0:00}";
            format[4] = ",{0:00}";
            format[5] = ",{0:00.0000000000}";
            for (int i = 0; i < 32; i++)
            {
                format[i + 6] = ",{0:000.000}";
            }

            for(int i = 0; i < EpochNum; i++)
            {
                data[i, 0] = Epoches[i].Epoch.Year;
                data[i, 1] = Epoches[i].Epoch.Month;
                data[i, 2] = Epoches[i].Epoch.DayOfMonth;
                data[i, 3] = Epoches[i].Epoch.Hour;
                data[i, 4] = Epoches[i].Epoch.Minute;
                data[i, 5] = Epoches[i].Epoch.Second;

                dataB[i, 0] = Epoches[i].Epoch.Year;
                dataB[i, 1] = Epoches[i].Epoch.Month;
                dataB[i, 2] = Epoches[i].Epoch.DayOfMonth;
                dataB[i, 3] = Epoches[i].Epoch.Hour;
                dataB[i, 4] = Epoches[i].Epoch.Minute;
                dataB[i, 5] = Epoches[i].Epoch.Second;

                dataL[i, 0] = Epoches[i].Epoch.Year;
                dataL[i, 1] = Epoches[i].Epoch.Month;
                dataL[i, 2] = Epoches[i].Epoch.DayOfMonth;
                dataL[i, 3] = Epoches[i].Epoch.Hour;
                dataL[i, 4] = Epoches[i].Epoch.Minute;
                dataL[i, 5] = Epoches[i].Epoch.Second;
            }

            OSat sat;
            string prn;
            int rowIndex;
            int colIndex;
            foreach (var item in Arcs)
            {
                prn = item.Key;
                foreach (var arc in item.Value)
                {
                    for (int i = 0; i < arc.Length; i++)
                    {
                        sat = arc[i];

                        rowIndex = i + arc.StartIndex;
                        colIndex = int.Parse(prn.Substring(1)) - 1 + 6;

                        data[rowIndex, colIndex] = sat[meas];
                        dataB[rowIndex, colIndex] = sat.IPP[1] * Angle.R2D;
                        dataL[rowIndex, colIndex] = sat.IPP[0] * Angle.R2D;
                    }
                }

                string fileName = string.Format("{0}.meas.{1}.txt", Name, meas.ToLower());
                string filePath = Path.Combine(folder, fileName);
                FileHelper.WriteMatrix(filePath, data, format, ',');

                string fileNameB = Name + ".ipp.b.txt";
                string fileNameL = Name + ".ipp.l.txt";

                string filePathB = Path.Combine(folder, fileNameB);
                string filePathL = Path.Combine(folder, fileNameL);

                FileHelper.WriteMatrix(filePathB, dataB,format, ',');
                FileHelper.WriteMatrix(filePathL, dataL,format, ',');

                //List<MatlabMatrix> mats = new List<MatlabMatrix>
                //{
                //    MatlabWriter.Pack(matP1,"p1"),
                //    MatlabWriter.Pack(matP2,"p2"),
                //    MatlabWriter.Pack(matL1,"l1"),
                //    MatlabWriter.Pack(matL2,"l2"),
                //    MatlabWriter.Pack(matP1,"p4"),
                //    MatlabWriter.Pack(matLat,"lat"),
                //    MatlabWriter.Pack(matLon,"lon"),
                //};

                //MatlabWriter.Store(Path.Combine(folder, Name + ".mat"), mats);
            }

        }
    }
}
