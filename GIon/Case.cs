using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GeoFun.Sys;
using GeoFun.GNSS;
using System.Text.RegularExpressions;

namespace GIon
{
    public class Case
    {
        private string obsFolder { get; set; } = "";
        private string resFolder { get; set; } = "";
        private string tmpFolder { get; set; } = "";
        private string orbFolder { get; set; } = "";

        public List<string> StationNames { get; set; } = new List<string>();
        public Dictionary<string, List<DOY>> DOYs { get; set; } = new Dictionary<string, List<DOY>>();

        /// <summary>
        /// 本次解算设置
        /// </summary>
        public Options Option { get; set; } = new Options();

        /// <summary>
        /// 任务路径(文件夹)
        /// </summary>
        public string Path { get; set; } = "";
        /// <summary>
        /// 所有的观测文件路径
        /// </summary>
        public List<string> obsFiles { get; set; } = new List<string>();

        /// <summary>
        /// 所有测站
        /// </summary>
        public List<OStation> Stations { get; set; } = new List<OStation>();

        public List<DCBFile> dCBFiles { get; set; } = new List<DCBFile>();

        public Case(string path = "")
        {
            Path = path;
            Init();
        }

        public void Start()
        {
            /** 1.预处理
                  1.1 下载星历、钟差、dcb、电离层文件
                  1.2  转换rinex版本(调用gfzrnx程序)
            */

            /** 2.读取数据
             *    2.1 读观测文件
             *    2.2 读星历、钟差、dcb
             */

            /** 3.误差改正、粗差剔除
                  3.1 粗差探测
                  3.2 钟跳探测
                  3.3 周跳探测
             */

            /** 4.中间量计算
                  4.1 相位平滑伪距观测值
                  4.2 高度角，方位角
             */

            /** 5.列法方程并求解
             */

            /** 6.将结果写出到文件
                  6.1 将结果写出到txt文本
                  6,2 分析结果并绘图
             */

        }

        public void Init()
        {
            obsFolder = System.IO.Path.Combine(Path, "obs");
            resFolder = System.IO.Path.Combine(Path, "out");
            tmpFolder = System.IO.Path.Combine(Path, "tmp");
            orbFolder = System.IO.Path.Combine(Path, "orb");
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <returns></returns>
        public bool CreateFolders()
        {
            if (!CreateFolder(obsFolder))
            {
                Message.Error("创建文件夹失败:" + obsFolder);
                return false;
            }

            if (!CreateFolder(resFolder))
            {
                Message.Error("创建文件夹失败:" + resFolder);
                return false;
            }

            if (!CreateFolder(tmpFolder))
            {
                Message.Error("创建文件夹失败:" + tmpFolder);
                return false;
            }

            return true;
        }
        public bool CreateFolder(string folder)
        {
            if (!Directory.Exists(folder))
            {
                try
                {
                    Directory.CreateDirectory(folder);
                }
                catch { }
            }
            return Directory.Exists(folder);
        }

        /// <summary>
        /// 搜索o文件
        /// </summary>
        /// <returns>o文件的个数</returns>
        public int SearchObsFiles()
        {
            if (string.IsNullOrWhiteSpace(obsFolder) || !Directory.Exists(obsFolder))
            {
                Message.Error("工作文件夹不存在:" + obsFolder);
                return -1;
            }

            DirectoryInfo dir = new DirectoryInfo(obsFolder);

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
                    Message.Warning("解压文件失败:" + file.FullName);
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
                    Message.Warning("解压文件失败:" + file.FullName);
                }
            }

            //// 3.扫描o文件
            foreach (var file in dir.GetFiles("*.??o"))
            {
                obsFiles.Add(file.Name);
            }

            return obsFiles.Count;
        }
        /// <summary>
        /// 获取观测数据的测站以及测站的所有doy
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void GetStationDOY()
        {
            DirectoryInfo dir = new DirectoryInfo(obsFolder);
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
        /// 从网上下载必须的数据
        /// </summary>
        /// <returns></returns>
        public bool Download()
        {
            return true;
        }
    }
}
