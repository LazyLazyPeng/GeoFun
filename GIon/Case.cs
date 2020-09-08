using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GeoFun.Sys;
using GeoFun.GNSS;
using GeoFun.GNSS.Net;
using System.Text.RegularExpressions;

namespace GIon
{
    public class Case
    {
        /// <summary>
        /// 观测值文件夹
        /// </summary>
        private string obsFolder { get; set; } = "";
        /// <summary>
        /// 结果输出文件夹
        /// </summary>
        private string resFolder { get; set; } = "";
        /// <summary>
        /// 临时文件夹
        /// </summary>
        private string tmpFolder { get; set; } = "";
        /// <summary>
        /// 星历/轨道/钟差文件夹
        /// </summary>
        private string orbFolder { get; set; } = "";
        /// <summary>
        /// 各种表文件
        /// </summary>
        private string tabFolder { get; set; } = "";
        /// <summary>
        /// 日志文件夹
        /// </summary>
        private string logFolder { get; set; } = "";

        public Observation Obs;
        public Orbit Orb;
        public List<string> StationNames { get; set; } = new List<string>();

        /// <summary>
        /// 本次解算设置
        /// </summary>
        public GeoFun.GNSS.Options Option { get; set; } = new GeoFun.GNSS.Options();

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
            Obs = new Observation(obsFolder);
            Orb = new Orbit(orbFolder);

            /** 1.预处理
                  1.1 下载星历、钟差、dcb、电离层文件
                  1.2  转换rinex版本(调用gfzrnx程序)
            */
            Obs.SearchFiles();
            Obs.CheckDataConsit();
            if (!Download(Obs.DOYs)) return;
            Orb.GetAllSp3Files();

            /** 2.读取数据
             *    2.1 读观测文件
             *    2.2 读星历、钟差、dcb
             */
            ReadFiles();

            /** 3.误差改正、粗差剔除
                  3.1 粗差探测
                  3.2 钟跳探测
                  3.3 周跳探测
             */

            /** 4.中间量计算
                  4.1 相位平滑伪距观测值
                  4.2 高度角，方位角
             */
            foreach(var station in Obs.Stations)
            {
                for(int i =0; i < station.EpochNum; i++)
                {
                    OEpoch oepo = station.Epoches[i];
                    Orb.GetSatPos(ref oepo);
                }
            }
            Obs.Preprocess();
            Obs.SmoothP4();

            Obs.Fit();

            /** 5.列法方程并求解DCB
                  5.1 列出单天解的法方程
                  5.2 多天解法方程叠加
             */

            /** 6.代入DCB，求解TEC
             */

            /** 7.将结果写出到文件
                  7.1 将结果写出到txt文本
                  7,2 分析结果并绘图
             */
            Obs.WriteTECMap(resFolder,10);

        }

        public void Init()
        {
            obsFolder = System.IO.Path.Combine(Path, "obs");
            resFolder = System.IO.Path.Combine(Path, "out");
            tmpFolder = System.IO.Path.Combine(Path, "tmp");
            orbFolder = System.IO.Path.Combine(Path, "orb");
            tabFolder = System.IO.Path.Combine(Path, "tab");

            CreateFolders();
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

            if (!CreateFolder(orbFolder))
            {
                Message.Error("创建文件夹失败:" + orbFolder);
                return false;
            }

            if (!CreateFolder(orbFolder))
            {
                Message.Error("创建文件夹失败:" + orbFolder);
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
        /// 从网上下载必须的数据
        /// </summary>
        /// <returns></returns>
        public bool Download(Dictionary<string,List<DOY>> DOYs)
        {
            // 获取数据的起始和结束时间
            DOY start = null;
            DOY end = null;
            foreach (var station in DOYs.Keys)
            {
                DOY min = DOYs[station].Min();
                DOY max = DOYs[station].Max();
                if (start is null)
                {
                    start = min;
                    end = max;
                }
                else
                {
                    if (min < start) start = min;
                    if (max > end) end = max;
                }
            }

            // 前一天的星历
            start = start.AddDays(-1);
            // 后一天的星历
            end = end.AddDays(1);

            while (start <= end)
            {
                Downloader.DownloadSp3DOY(start.Year, start.Day, orbFolder);
                Downloader.DownloadClkDOY(start.Year, start.Day, orbFolder);
                Downloader.DownloadI(start.Year, start.Day, orbFolder);

                start=start.AddDays(1);
            }

            return true;
        }

        public void ReadFiles()
        {
            Obs.Read(obsFolder);
            Orb.Read(orbFolder);
        }

        /// <summary>
        /// 读取dcb文件
        /// </summary>
        public void ReadDCBFiles() { }
        /// <summary>
        /// 读取星历
        /// </summary>
        public void ReadOrbFiles() { }
    }
}
