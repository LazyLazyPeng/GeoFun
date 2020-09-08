using GeoFun.Sys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GeoFun.GNSS
{
    public class Observation
    {
        public List<string> StationNames { get; set; } = new List<string>();
        public Dictionary<string, List<DOY>> DOYs { get; set; } = new Dictionary<string, List<DOY>>();
        public string Folder { get; set; }
        public List<string> FilePaths { get; set; } = new List<string>();
        /// <summary>
        /// 所有测站
        /// </summary>
        public List<OStation> Stations { get; set; } = new List<OStation>();
        /// <summary>
        /// 所有弧段
        /// </summary>
        public Dictionary<string, List<OArc>> Arcs = new Dictionary<string, List<OArc>>();

        public Observation(string folder)
        {
            Folder = folder;
        }

        /// <summary>
        /// 获取观测数据的测站以及测站的所有doy
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void GetStationDOY()
        {
            DirectoryInfo dir = new DirectoryInfo(Folder);
            if (!dir.Exists) return;

            foreach (var file in dir.GetFiles("*.??o"))
            {
                //// 匹配文件名(o文件)
                if (file.Name.Length != 12 ||
                    !Regex.IsMatch(file.Name, @"^\S{4}\d{3}\S.\d{2}[Oo]")) continue;

                //// 截取测站名、年份、年积日
                string stationName = file.Name.Substring(0, 4);
                int doy = int.Parse(file.Name.Substring(4, 3));
                int year = int.Parse(file.Name.Substring(9, 2));
                if (year < 50) year += 2000;
                else year += 1900;

                if (!DOYs.ContainsKey(stationName))
                {
                    DOYs.Add(stationName, new List<DOY>());
                }

                StationNames.Add(stationName);
                DOYs[stationName].Add(new DOY(year, doy));
            }

            //// DOY排序
            foreach (var station in DOYs)
            {
                station.Value.Sort();
            }
        }
        /// <summary>
        /// 检查数据DOY是否连续
        /// </summary>
        public bool CheckDOY()
        {
            bool flag = true;
            foreach (var station in DOYs.Keys)
            {
                if (DOYs[station].Count < 2) continue;
                for (int i = 1; i < DOYs[station].Count; i++)
                {
                    //// 判断doy是否连续
                    // 1.不跨年
                    if (DOYs[station][i].Year == DOYs[station][i - 1].Year)
                    {
                        if (DOYs[station][i].Day - DOYs[station][i - 1].Day <= 1)
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
                    else if (DOYs[station][i].Year - DOYs[station][i - 1].Year > 1)
                    {
                        flag = false;
                        break;
                    }
                    else
                    {
                        if (DOYs[station][i].Day != 1)
                        {
                            flag = false;
                            break;
                        }

                        // 闰年最后一天
                        if (DateTime.IsLeapYear(DOYs[station][i - 1].Year)
                            && DOYs[station][i - 1].Day == 366)
                        {
                            continue;
                        }
                        // 非闰年最后一天
                        else if (DateTime.IsLeapYear(DOYs[station][i - 1].Year)
                            && DOYs[station][i - 1].Day == 365)
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
            }

            return flag;
        }

        /// <summary>
        /// 检查数据是否连续
        /// </summary>
        /// <returns></returns>
        public bool CheckDataConsit()
        {
            GetStationDOY();
            return CheckDOY();
        }

        /// <summary>
        /// 搜索o文件
        /// </summary>
        /// <returns>o文件的个数</returns>
        public int SearchFiles()
        {
            if (string.IsNullOrWhiteSpace(Folder) || !Directory.Exists(Folder))
            {
                //Message.Error("工作文件夹不存在:" + Folder);
                return -1;
            }

            FilePaths = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(Folder);

            //// 1.扫描Z压缩文件
            string unzipCmd = "7z.exe x \"{0}\" -o\"{1}\" -y |exit";
            foreach (var file in dir.GetFiles("*d.Z"))
            {
                string cmd = string.Format(unzipCmd, file.FullName, dir.FullName);
                try
                {
                    CMDHelper.ExecuteThenWait(cmd);
                }
                catch
                {
                    //Message.Warning("解压文件失败:" + file.FullName);
                }
            }

            //// 2.扫描d文件
            string transCmd = "crx2rnx.exe \"{0}\" |exit";
            foreach (var file in dir.GetFiles("*.??d"))
            {
                string cmd = string.Format(transCmd, file.FullName);
                try
                {
                    CMDHelper.ExecuteThenWait(cmd);
                }
                catch
                {
                    //Message.Warning("解压文件失败:" + file.FullName);
                }
            }

            //// 3.扫描o文件
            foreach (var file in dir.GetFiles("*.??o"))
            {
                FilePaths.Add(file.Name);
            }

            return FilePaths.Count;
        }
        /// <summary>
        /// 获取观测数据的测站以及测站的所有doy
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <summary>
        /// 读取观测值
        /// </summary>
        public void Read(string folder)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                folder = Folder;
            }
            else
            {
                Folder = folder;
            }

            foreach (var station in DOYs.Keys)
            {
                OStation oSta = new OStation(station);
                oSta.ReadAllObs(Folder);
                oSta.SortObs();
                Stations.Add(oSta);
            }
        }

        /// <summary>
        /// 探测所有弧段 
        /// </summary>
        public void DetectArcs()
        {
            if (Stations is null) return;
            foreach(var sta in Stations)
            {
                sta.DetectArcs();
            }
        }
        /// <summary>
        /// 相位平滑伪距
        /// </summary>
        public void SmoothP4()
        {
            foreach(var sta in Stations)
            {
                sta.SmoothP4();
            }
        }
        /// <summary>
        /// 计算高度角、方位角、穿刺点
        /// </summary>
        public void CalAzElIPP()
        {
            if (Stations is null) return;
            foreach(var osta in Stations)
            {
                osta.CalAzElIPP();
            }
        }

        /// <summary>
        /// 探测粗差
        /// </summary>
        public void DetectOutlier()
        {
            if (Stations is null) return;

            foreach(var sta in Stations)
            {
                sta.DetectOutlier();
            }
        }
        /// <summary>
        /// 探测周跳
        /// </summary>
        public void DetectCycleSlip()
        {
            if (Stations is null) return;

            foreach(var sta in Stations)
            {
                sta.DetectCycleSlip();
            }
        }

        /// <summary>
        /// 预处理
        /// </summary>
        public void Preprocess()
        {
            DetectOutlier();
            DetectArcs();
            DetectCycleSlip();
        }
    }
}
