using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class OSat
    {
        /// <summary>
        /// 测站名
        /// </summary>
        public string StationName;
        /// <summary>
        /// 历元时间
        /// </summary>
        public GPST Epoch;
        /// <summary>
        /// 卫星PRN号
        /// </summary>
        public string SatPRN;

        public enumGNSSSystem GNSSSystem { get; set; } = enumGNSSSystem.GPS;

        /// <summary>
        /// 各种不同的观测值，例如
        ///     C1、P1、P2、L1、L2、
        ///     P4、L4、SM_P4
        ///     LP1（相位平滑伪距结果）、LP2（相位平滑伪距结果）
        /// 用哈希表储存，便于查找.
        /// </summary>
        public Dictionary<string, double> SatData = new Dictionary<string, double>();
        /// <summary>
        /// 失锁标识符
        /// Loss of lock ndicator(LLI). Range:0-7
        /// 0 or blank: OK or not known 
        ///  Bit 0 set : Lost lock between previous and     
        ///              current observation: cycle slip    
        ///              possible                           
        ///  Bit 1 set : Opposite wavelength factor to the  
        ///              one defined for the satellite by a 
        ///              previous WAVELENGTH FACT L1/2 line 
        ///              or opposite to the default.        
        ///              Valid for the current epoch only.  
        ///  Bit 2 set : Observation under Antispoofing     
        ///              (may suffer from increased noise)  
        ///                                                 
        ///  Bits 0 and 1 for phase only.
        /// </summary>
        public Dictionary<string, int> LLI;
        /// <summary>
        /// 信号强度，1表示可能的最小信号强度，5表示良好S/N比的值，9表示可能的最大信号强度，0或空表示未知或未给出
        /// </summary>
        public Dictionary<string, int> SignalStrength;
        /// <summary>
        /// 卫星坐标，可由精密星历或者广播星历计算得到
        /// </summary>
        public double[] SatCoor = { 0d, 0d, 0d };
        /// <summary>
        /// 卫星钟差，可由精密星历或者广播星历计算得到
        /// </summary>
        public double SatClock;
        /// <summary>
        /// 高度角，单位：度
        /// </summary>
        public double altitude_angle;
        /// <summary>
        /// 测站近似坐标
        /// </summary>
        public Coor3 approPOS;

        /// <summary>
        /// 模糊度N1
        /// </summary>
        public double N1;
        /// <summary>
        /// 模糊度N2
        /// </summary>
        public double N2;

        /// <summary>
        /// 利用P码伪距求得的带DCB影响的STEC
        /// </summary>
        public double STEC_PWithDCB;
        /// <summary>
        /// 利用载波相位和模糊度求得的带DCB影响的STEC
        /// </summary>
        public double STEC_CPWithDCB;

        /// <summary>
        /// 该历元是否发生周跳
        /// </summary>
        public bool CycleSlip = false;
        /// <summary>
        /// 是否粗差
        /// </summary>
        public bool Outlier = false;
        /// <summary>
        /// 伪距是否被平滑过
        /// </summary>
        public bool CodePhased = false;
        /// <summary>
        /// 是否缺少P1
        /// </summary>
        public bool LackOfP1 = false;

        /// <summary>
        /// 穿刺点位置
        /// </summary>
        public double[] IPP = { 0d, 0d };
        /// <summary>
        /// 方位角
        /// </summary>
        public double Azimuth = 0d;
        /// <summary>
        /// 高度角
        /// </summary>
        public double Elevation = 0d;

        /// <summary>
        /// 根据观测值名称索引
        /// </summary>
        /// <param name="obsType"></param>
        /// <returns></returns>
        public double this[string obsType]
        {
            get
            {
                if (SatData.ContainsKey(obsType))
                {
                    return SatData[obsType];
                }
                else
                {
                    switch (obsType)
                    {
                        case "MW":
                        case "mw":
                            return 0d;
                        case "GF":
                        case "gf":
                            if (SatData.ContainsKey("L1") && SatData.ContainsKey("L2"))
                            {
                                double l1 = SatData["L1"];
                                double l2 = SatData["L2"];

                                if (l1 != 0 && l2 != 0)
                                {
                                    return l1 - l2 * Common.GPS_F1 / Common.GPS_F2;
                                }
                            }
                            return 0d;
                        default:
                            return 0d;
                    }
                }
            }
            set
            {
                if (SatData.ContainsKey(obsType))
                {
                    SatData[obsType] = value;
                }
                else
                {
                    SatData.Add(obsType, value);
                }
            }
        }
    }
}
