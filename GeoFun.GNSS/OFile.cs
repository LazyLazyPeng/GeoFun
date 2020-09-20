using GeoFun.IO;
using GeoFun.MathUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace GeoFun.GNSS
{
    public class OFile : IComparable<OFile>
    {
        /// <summary>
        /// 测站名称
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 观测时间,年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 观测时间,年积日
        /// </summary>
        public int DOY { get; set; }

        /// <summary>
        /// 采样率(s)
        /// </summary>
        public double Interval
        {
            get
            {
                return Header.interval;
            }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// 观测开始时间
        /// </summary>
        public GPST StartTime
        {
            get
            {
                if (Header is null) return null;
                else if (Header.startTime is null)
                {
                    if (Epoches is null || Epoches.Count == 0) return null;
                    else return Epoches[0].Epoch;
                }
                else
                {
                    return StartTime;
                }
            }
        }

        /// <summary>
        /// 观测结束时间
        /// </summary>
        public GPST EndTime;

        /// <summary>
        /// 第一个历元的索引
        /// </summary>
        public int StartIndex = -1;

        /// <summary>
        /// 最后一个历元的索引
        /// </summary>
        public int EndIndex = -1;

        /// <summary>
        /// 文件头
        /// </summary>
        public OHeader Header;

        /// <summary>
        /// 所有历元的观测数据
        /// </summary>
        public List<OEpoch> Epoches = new List<OEpoch>(2880 * 2);

        /// <summary>
        /// 所有弧段
        /// </summary>
        public Dictionary<string, List<OArc>> Arcs = new Dictionary<string, List<OArc>>();

        public OFile(string path)
        {
            Path = path;

            try
            {
                if(File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);

                    int year,  doy;
                    string stationName;
                    FileName.ParseRinex2(info.Name, out stationName, out doy, out year);

                    DOY = doy;
                    Year = year;
                    StationName = stationName;
                }
            }
            catch
            { }
        }

        public bool TryRead()
        {
            int month, dom;
            Time.DOY2MonthDay(Year, DOY, out month, out dom);
            GPST startTime = new GPST(Year, month, dom, 0, 0, 0m);
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    Header = new OHeader();
                    string line = sr.ReadLine();  //一行数据
                                                  //读取头
                    while (line != "" && line != null)
                    {
                        string flag = line.Substring(60).Trim();
                        if (flag == "END OF HEADER") break;
                        switch (flag)
                        {
                            case "RINEX VERSION / TYPE":
                                Header.version = Convert.ToDouble(line.Substring(0, 9).Trim());
                                Header.systemType = line.Substring(40, 1).Trim();
                                break;
                            case "MARKER NAME":
                                Header.markName = line.Substring(0, 4).Trim();
                                break;
                            case "MARKER NUMBER":
                                Header.markNumber = line.Substring(0, 20).Trim();
                                break;
                            case "OBSERVER / AGENCY":
                                Header.observerName = line.Substring(0, 20).Trim();
                                Header.observerAgencyName = line.Substring(20, 40).Trim();
                                break;
                            case "REC # / TYPE / VERS":
                                Header.rcvNumber = line.Substring(0, 20).Trim();
                                Header.rcvType = line.Substring(20, 20).Trim();
                                Header.rcvVer = line.Substring(40, 20).Trim();
                                break;
                            case "ANT # / TYPE":
                                Header.antNumber = line.Substring(0, 20).Trim();
                                Header.antType = line.Substring(20, 20).Trim();
                                break;
                            case "APPROX POSITION XYZ":
                                Header.approxPos.X = Convert.ToDouble(line.Substring(0, 14).Trim());
                                Header.approxPos.Y = Convert.ToDouble(line.Substring(14, 14).Trim());
                                Header.approxPos.Z = Convert.ToDouble(line.Substring(28, 14).Trim());
                                break;
                            case "ANTENNA: DELTA H/E/N":
                                Header.antDelta = new Coor3();
                                Header.antDelta.U = Convert.ToDouble(line.Substring(0, 14).Trim());
                                Header.antDelta.E = Convert.ToDouble(line.Substring(14, 14).Trim());
                                Header.antDelta.N = Convert.ToDouble(line.Substring(28, 14).Trim());
                                break;
                            case "WAVELENGTH FACT L1/2":
                                Header.waveLenFactorL1 = Convert.ToInt32(line.Substring(0, 6).Trim());
                                Header.waveLenFactorL2 = Convert.ToInt32(line.Substring(6, 6).Trim());
                                break;
                            case "# / TYPES OF OBSERV":
                                Header.obsTypeNum = Convert.ToInt32(line.Substring(0, 6).Trim());
                                Header.obsTypeList = new List<string>();

                                int obsTypeNum = Header.obsTypeNum;
                                while (obsTypeNum > 9)
                                {
                                    obsTypeNum -= 9;
                                    for (int i = 0; i < 9; i++)
                                    {
                                        Header.obsTypeList.Add(line.Substring(6 + 6 * i, 6).Trim());
                                    }
                                    line = sr.ReadLine();
                                }

                                for (int i = 0; i < obsTypeNum; i++)
                                {
                                    Header.obsTypeList.Add(line.Substring(6 + 6 * i, 6).Trim());
                                }
                                break;
                            case "INTERVAL":
                                Header.interval = double.Parse(line.Substring(0, 11).Trim());
                                break;
                            case "TIME OF FIRST OBS":
                                break;

                        }
                        line = sr.ReadLine();
                    }//头已读取完

                    GPST epoStart = new GPST(Year, DOY);
                    Epoches = new List<OEpoch>(2880 * 2);
                    int epochNum = (int)Math.Floor((Time.SecondsPerDay+0.1d) / Header.interval);
                    for (int i = 0; i < epochNum; i++)
                    {
                        var curEpo = new GPST(epoStart); 
                        curEpo.AddSeconds(Header.interval*epochNum);
                        var obsEpo = new OEpoch();
                        obsEpo.Epoch = curEpo;
                        Epoches.Add(obsEpo);
                    }

                    //读取观测值
                    line = sr.ReadLine();
                    //obsdata_allepoch = new List<OBSDATA_EPOCH>();//全局变量初始化
                    OEpoch epoch = new OEpoch();//
                    epoch.AllSat = new Dictionary<string, OSat>();
                    epoch.PRNList = new List<string>();
                    while (line != "" && line != null)
                    {
                        string flag = "";
                        if (line.Length > 60)
                        {
                            flag = line.Substring(60).Trim();
                        }

                        //// 60行以后，如果包含COMMENT,则认为是注释行
                        if (flag.Contains("COMMENT"))
                        {
                            line = sr.ReadLine();
                            continue;
                        }

                        try
                        {
                            epoch = new OEpoch();

                            //// 读取历元
                            GPST epochT = GPST.Decode(line.Substring(0, 26));
                            if (epochT is null)
                            {
                                line = sr.ReadLine();
                                continue;
                            }
                            epoch.Epoch = epochT;

                            //// 历元标识
                            int epoFlag = 0;
                            if (int.TryParse(line.Substring(26, 3), out epoFlag))
                            {
                                epoch.Flag = epoFlag;
                            }

                            //// 观测到的卫星的数量
                            int prnNum = int.Parse(line.Substring(29, 3).Trim());

                            while (prnNum > 12)
                            {
                                prnNum -= 12;
                                for (int j = 0; j < 12; j++)
                                {
                                    epoch.PRNList.Add(line.Substring(32 + j * 3, 3).Trim().Replace(' ', '0'));
                                }
                                line = sr.ReadLine();
                            }

                            //// 小于12颗卫星，只需读一行
                            for (int j = 0; j < prnNum; j++)
                            {
                                epoch.PRNList.Add(line.Substring(32 + j * 3, 3).Trim().Replace(' ', '0'));
                            }

                            for (int satI = 0; satI < epoch.PRNList.Count; satI++)
                            {
                                OSat oSat = new OSat();
                                oSat.StationName = Header.markName;
                                oSat.SatData = new Dictionary<string, double>();
                                oSat.LLI = new Dictionary<string, int>();
                                oSat.SignalStrength = new Dictionary<string, int>();
                                oSat.Epoch = epoch.Epoch;
                                oSat.SatPRN = epoch.PRNList[satI].ToString();

                                // 初始化观测值
                                for (int i = 0; i < Header.obsTypeList.Count; i++)
                                {
                                    oSat.SatData.Add(Header.obsTypeList[i], 0d);
                                    oSat.LLI.Add(Header.obsTypeList[i], 0);
                                    oSat.SignalStrength.Add(Header.obsTypeList[i], 0);
                                }

                                double obsValue = 0d;
                                int lli = -1;
                                int singalStrength = -1;
                                int lineNum = (int)Math.Ceiling(Header.obsTypeNum / 5d);
                                int obsNumPerLine = 5;
                                int obsIndex = 0;
                                for (int i = 0; i < lineNum; i++)
                                {
                                    line = ReadLine(sr);
                                    obsNumPerLine = (Header.obsTypeNum - i * 5) > 5 ? 5 : Header.obsTypeNum - i * 5;

                                    if (string.IsNullOrWhiteSpace(line.Trim()))
                                    {
                                        for (int j = 0; j < obsNumPerLine; j++)
                                        {
                                            oSat.SatData[Header.obsTypeList[obsIndex]] = 0;
                                            oSat.LLI[Header.obsTypeList[obsIndex]] = 0;
                                            oSat.SignalStrength[Header.obsTypeList[obsIndex]] = 0;
                                        }

                                        continue;
                                    }

                                    for (int j = 0; j < obsNumPerLine; j++)
                                    {
                                        obsIndex = i * 5 + j;

                                        try
                                        {
                                            if (double.TryParse(line.Substring(j * 16, 14), out obsValue))
                                            {
                                                oSat.SatData[Header.obsTypeList[obsIndex]] = obsValue;
                                            }

                                            if (int.TryParse(line.Substring(j * 16 + 14, 1), out lli))
                                            {
                                                oSat.LLI[Header.obsTypeList[obsIndex]] = lli;
                                            }

                                            if (int.TryParse(line.Substring(j * 16 + 15, 1), out singalStrength))
                                            {
                                                oSat.SignalStrength[Header.obsTypeList[obsIndex]] = singalStrength;
                                            }
                                        }
                                        catch (Exception ex01)
                                        {
                                            continue;
                                        }
                                    }
                                }

                                epoch.AllSat.Add(oSat.SatPRN, oSat);
                            }
                            int index = (int)Math.Floor(((double)epoch.Epoch.SecondsOfDay + 0.1) / Header.interval);
                            Epoches[index] = epoch;
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }

                        line = sr.ReadLine();
                    }

                    sr.Close();
                    fs.Close();
                }
            }

            for (int i = 0; i < Epoches.Count; i++)
            {
                if (Epoches[i].Epoch is null)
                {
                    GPST epo = new GPST(startTime);
                    epo.AddSeconds(Interval * i);
                    Epoches[i].Epoch = epo;
                }
            }

            if (Epoches is null || Epoches.Count == 0) return false;
            return true;
        }
        public string ReadLine(StreamReader sr)
        {
            string line = sr.ReadLine();
            if (line.Length < 80)
            {
                line = line.PadRight(80);
            }

            return line;
        }

        public static OFile Read(string path)
        {
            OFile file = new OFile(path);

            if (file.TryRead())
            {
                return file;
            }
            else
            {
                return null;
            }
        }

        public int SearchAllArcs(int arcLength = 0)
        {
            // 弧段最短长度
            if (arcLength == 0) arcLength = Options.ARC_MIN_LENGTH;

            Dictionary<string, List<OArc>> allArc = new Dictionary<string, List<OArc>>();
            if (Epoches is null | Epoches.Count <= 0) return 0;
            if (Options.SATELLITE_SYS == 'G')
            {
                for (int i = 1; i < 33; i++)
                {
                    string prn = "G" + i.ToString("0#");
                    allArc.Add(prn, SearchArcs(prn));
                }
            }
            Arcs = allArc;
            return allArc.Sum(a => a.Value.Count);
        }

        public List<OArc> SearchArcs(string prn, int arcLen = 0)
        {
            if (arcLen == 0) arcLen = Options.ARC_MIN_LENGTH;
            List<OArc> arcs = new List<OArc>();

            // 观测值缺失
            if (
                !Header.obsTypeList.Contains("L1") ||
                !Header.obsTypeList.Contains("L2") ||
                !Header.obsTypeList.Contains("P2") ||
               (!Header.obsTypeList.Contains("P1") &&
                !Header.obsTypeList.Contains("C1"))
                )
            {
                return arcs;
            }

            OArc arc = null;
            int startIndex = 0;
            do
            {
                arc = SearchArc(prn, startIndex);

                if (arc != null)
                {
                    if (arc.Length >= arcLen)
                    {
                        arc.StartIndex += 40;
                        arc.EndIndex -= 10;
                        arcs.Add(arc);
                    }
                    startIndex = arc.EndIndex + 1;
                }
            }
            while (arc != null);

            return arcs;
        }

        public OArc SearchArc(string prn, int startIndex)
        {
            if (startIndex < 0 || startIndex >= Epoches.Count) return null;
            int start = startIndex;
            int end = startIndex;
            bool arcStarted = false;
            for (int i = startIndex; i < Epoches.Count; i++)
            {
                bool flag = false;
                if (Epoches[i].AllSat.ContainsKey(prn))
                {
                    if (
                        Epoches[i][prn]["L1"] != 0 &&
                        Epoches[i][prn]["L2"] != 0 &&
                        Epoches[i][prn]["P2"] != 0 &&
                        (Epoches[i][prn]["P1"] != 0 ||
                        Epoches[i][prn]["C1"] != 0) &&
                        !Epoches[i][prn].Outlier &&
                        !Epoches[i][prn].CycleSlip
                        )
                    {
                        flag = true;
                    }
                }

                if (flag)
                {
                    if (arcStarted)
                    {
                        continue;
                    }
                    else
                    {
                        arcStarted = true;
                        start = i;
                    }
                }
                else
                {
                    if (arcStarted)
                    {
                        end = i - 1;
                        arcStarted = false;

                        OArc arc = new OArc();
                        arc.PRN = prn;
                        arc.StartIndex = start;
                        arc.EndIndex = end;
                        arc.File = this;
                        return arc;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return null;
        }

        public int CompareTo(OFile other)
        {
            return StartTime.CompareTo(other.StartTime);
        }

        public void DetectOutlier()
        {
            ObsHelper.DetectOutlier(ref Epoches);
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

        public void CalIPP()
        {
            if (Arcs is null) return;
            double[] ApproxPos = { Header.approxPos.X, Header.approxPos.Y, Header.approxPos.Z };
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
                    }
                }
            }
        }


        public void CalSP4()
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

        public void Fit()
        {
            foreach (var prn in Arcs.Keys)
            {
                var arcs = Arcs[prn];
                for (int i = 0; i < arcs.Count; i++)
                {
                    var arc = arcs[i];

                    Fit(ref arc, 20, 2);
                }
            }

        }

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
                        if (Math.Abs(residue[i]) < 10 * sigma)
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

        public void WriteTEC(string folder)
        {
            var sta = this;
            {
                List<string[]> lines = new List<string[]>();
                List<string[]> linesB = new List<string[]>();
                List<string[]> linesL = new List<string[]>();

                for (int i = 0; i < Epoches.Count; i++)
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
                        lineb[index] = (sta.Epoches[i].AllSat[prn].IPP[0] * Angle.R2D).ToString("#.##########");
                        linel[index] = (sta.Epoches[i].AllSat[prn].IPP[1] * Angle.R2D).ToString("#.##########");
                    }
                    lines.Add(line);
                    linesB.Add(lineb);
                    linesL.Add(linel);
                }

                string fileName = string.Format("{0}_{1}.meas.p4.txt",StationName,DOY);
                string filePath = System.IO.Path.Combine(folder, fileName);
                FileHelper.WriteLines(filePath, lines, ',');

                string fileNameB = string.Format("{0}_{1}.ipp.b.txt",StationName,DOY);
                string fileNameL = string.Format("{0}_{1}.ipp.l.txt",StationName,DOY);

                string filePathB = System.IO.Path.Combine(folder, fileNameB);
                string filePathL = System.IO.Path.Combine(folder, fileNameL);

                FileHelper.WriteLines(filePathB, linesB, ',');
                FileHelper.WriteLines(filePathL, linesL, ',');
            }


        }
    }
}