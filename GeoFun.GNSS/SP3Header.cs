using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class SP3Header
    {
        //第一行
        /// <summary>
        /// 版本标识符
        /// </summary>
        public string Version;
        /// <summary>
        /// 位置（P）或位置/速度（V）标识符
        /// </summary>
        public string P_V_Flag;
        /// <summary>
        /// 轨道数据首历元时间
        /// </summary>
        public GPST StartTime;
        /// <summary>
        /// 总历元数目
        /// </summary>
        public int EpochNum;
        /// <summary>
        /// 数据处理所采用的数据类型
        /// </summary>
        public string Data_Used;
        /// <summary>
        /// 轨道数据所属坐标参照系
        /// </summary>
        public string Coordinate_Sys;
        /// <summary>
        /// 轨道类型
        /// </summary>
        public string OrbitType;
        /// <summary>
        /// 发布机构
        /// </summary>
        public string Agency;
        //第二行
        /// <summary>
        /// 首历元GPS时间
        /// </summary>
        public GPST StartGPSTime;
        /// <summary>
        /// 历元间隔
        /// </summary>
        public double Epoch_Interval;
        //第3-7行
        /// <summary>
        /// 轨道数据所涉及卫星的数量
        /// </summary>
        public int Num_Sats;
        /// <summary>
        /// 卫星的PRN号
        /// </summary>
        public List<string> SatPRN;
        //第8-12行
        /// <summary>
        /// 卫星的精度
        /// </summary>
        public List<string> SatAccuracy;

        /// <summary>
        /// 最后历元时间
        /// </summary>
        public GPST EndTime;
    }
}