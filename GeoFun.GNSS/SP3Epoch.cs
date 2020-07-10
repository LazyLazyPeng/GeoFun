using System;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class SP3Epoch
    {
        /// <summary>
        /// ��Ԫʱ��
        /// </summary>
        public GPST Epoch;

        /// <summary>
        /// �������ǵ���ֵ
        /// </summary>
        public Dictionary<string, SP3Sat> AllSat { get; set; } = new Dictionary<string, SP3Sat>();

        public SP3Sat this[string prn]
        {
            get
            {
                return AllSat[prn];
            }
            set
            {
                AllSat[prn] = value;
            }
        }
    }
}