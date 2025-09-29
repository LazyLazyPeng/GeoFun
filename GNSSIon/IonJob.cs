using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.MultiThread;
using GeoFun.GNSS;

namespace GNSSIon
{
    public class IonJob : Job
    {
        /// <summary>
        /// 根文件夹
        /// </summary>
        public string RootFolder { get; set; }
        /// <summary>
        /// 观测值文件夹
        /// </summary>
        public string ObsFolder { get; set; }
        /// <summary>
        /// 结果输出文件夹
        /// </summary>
        public string ResFolder { get; set; }
        /// <summary>
        /// 临时文件夹
        /// </summary>
        public string TmpFolder { get; set; } = "";
        /// <summary>
        /// 星历/轨道/钟差文件夹
        /// </summary>
        public string OrbFolder { get; set; } = "";
        /// <summary>
        /// 各种表文件
        /// </summary>
        public string TabFolder { get; set; } = "";
        /// <summary>
        /// 日志文件夹
        /// </summary>
        public string LogFolder { get; set; } = "";
        /// <summary>
        /// 求取单站解还是多站解
        /// </summary>
        public bool IsSingleStationResult { get; set; } = false;
        /// <summary>
        /// 电离层函数模型
        /// </summary>
        public enumIonoModel ModelType { get; set; } = enumIonoModel.Polinomial;

        public IonJob(string rootFolder)
        {
            RootFolder = rootFolder;
            ObsFolder = Path.Combine(RootFolder, "obs");
            OrbFolder = Path.Combine(RootFolder, "orb");
            ResFolder = Path.Combine(RootFolder, "res");
        }

        public bool DetectStationNum()
        {
            return false;
        }
    }
}
