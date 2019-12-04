using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class OEpoch : IEnumerable<KeyValuePair<string, OSat>>
    {
        /// <summary>
        /// 历元
        /// </summary>
        public GPST Epoch { get; set; }

        /// <summary>
        /// 历元标识
        /// </summary>
        /// <remarks>
        /// 0:OK
        /// 1:power failure between previous and current epoch
        /// >1:event flag
        ///  2:start moving antenna
        ///  3:new site occupation
        ///  4:header information follows
        ///  5:external event(epoch is significant,same time frame as observation time tags)
        ///  6:cycle slip records follow to optionally report detected and repaired cycle slip
        /// </remarks>
        public int Flag { get; set; } = 0;

        /// <summary>
        /// 是否发生钟跳
        /// </summary>
        public bool ClockJump { get; set; } = false;

        /// <summary>
        /// 钟跳类型 
        /// 0 仅时标阶跃 伪距相位连续 2 时标伪距阶跃 相位连续 3 时标相位连续 伪距阶跃 4 时标连续 伪距相位均阶跃
        /// 见 郭斐.《GPS精密单点定位质量控制与分析的相关理论与方法研究》
        /// </summary>
        public int ClockJumpType { get; set; } = -1;

        /// <summary>
        /// 钟跳值(微秒)
        /// The value of clock jump(unit:us)
        /// </summary>
        public int ClockJumpValue { get; set; } = 0;

        /// <summary>
        /// 卫星数
        /// </summary>
        public int SatNum
        {
            get
            {
                return AllSat.Count;
            }
        }

        /// <summary>
        /// 本历元所有的卫星编号
        /// </summary>
        public List<string> PRNList = new List<string>();

        /// <summary>
        /// 一个历元的所有观测值
        /// </summary>
        public Dictionary<string, OSat> AllSat = new Dictionary<string, OSat>();

        /// <summary>
        /// 接收机钟差
        /// </summary>
        public double ClockBias { get; set; } = 0d;

        /// <summary>
        /// 根据卫星PRN号检索观测数据
        /// </summary>
        /// <param name="prn"></param>
        /// <returns></returns>
        public OSat this[string prn]
        {
            get
            {
                if (PRNList.Contains(prn))
                {
                    return AllSat[prn];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                AllSat[prn] = value;
            }
        }

        /// <summary>
        /// 按顺序返回第index颗卫星的观测数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public OSat this[int index]
        {
            get
            {
                return AllSat[PRNList[index]];
            }
            set
            {
                AllSat[PRNList[index]] = value;
            }
        }

        public IEnumerator<KeyValuePair<string, OSat>> GetEnumerator()
        {
            return AllSat.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return AllSat.GetEnumerator();
        }
    }
}