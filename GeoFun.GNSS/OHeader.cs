using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class OHeader
    {
        /// <summary>
        /// RINEX 文件版本号
        /// </summary>
        public double version = 2.11;
        /// <summary>
        /// 观测的导航卫星系统类型，G为GPS，R为GLONASS，S为地球同步卫星信号有效载荷，T为NNSS子午卫星，M为混合系统
        /// </summary>
        public string systemType;
        /// <summary>
        ///  marker name,点名
        /// </summary>
        public string markName; 
        /// <summary>
        ///  marker number,点号
        /// </summary>
        public string markNumber;
        /// <summary>
        /// 点类型，marker type
        /// </summary>
        public string markType;
        /// <summary>
        /// 观测者姓名
        /// </summary>
        public string observerName;
        /// <summary>
        /// 观测机构姓名
        /// </summary>
        public string observerAgencyName;
        /// <summary>
        /// the SN for receiver 接收机序列号
        /// </summary>
        public string rcvNumber;
        /// <summary>
        /// the PN for the receiver 接收机类型 
        /// </summary>
        public string rcvType;
        /// <summary>
        /// the firmware version for receiver 接收机版本号
        /// </summary>
        public string rcvVer;
        /// <summary>
        /// the SN for antenna
        /// </summary>
        public string antNumber;
        /// <summary>
        /// the type of antenna,天线类型
        /// </summary>
        public string antType;
        /// <summary>
        /// 近似坐标
        /// </summary>
        public Coor3 approxPos;
        /// <summary>
        /// 近似坐标，功能更强大
        /// </summary>
        public Coor3 approxPosCoor;
        /// <summary>
        /// 天线高，以及天线中心相对于标志在东向和北向上的偏移量
        /// </summary>
        public Coor3 antDelta;
        /// <summary>
        /// L1的波长因子，1表示为全波，2表示为半波，0（位于L2的位置上）表示所用接收机为单频仪器
        /// </summary>
        public int waveLenFactorL1;
        /// <summary>
        /// L2的波长因子
        /// </summary>
        public int waveLenFactorL2;
        /// <summary>
        /// 观测值数据类型数量
        /// </summary>
        public int obsTypeNum;
        /// <summary>
        /// 观测值数据类型列表,L1、L2上的相位观测值，C1采用L1上C/A码所测定的伪距，P1、P2采用L1、L2上P码所测定的伪距
        /// </summary>
        public List<string> obsTypeList;
        /// <summary>
        /// 数据采样率
        /// </summary>
        public double interval = 0d;
        /// <summary>
        /// 跳秒数
        /// </summary>
        public double leapSeconds;
        /// <summary>
        /// 接收机钟差是否使用
        /// </summary>
        public int rcvClockOffsetApply;
        /// <summary>
        /// 起始时间
        /// </summary>
        public GPST startTime;
        /// <summary>
        /// 终止时间
        /// </summary>
        public GPST endTime;
        /// <summary>
        /// 衔接索引，该天数据的起始观测数据索引
        /// </summary>
        public int StartIndex;
    }
}