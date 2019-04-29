using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class OHelper
    {
        public static void CycleSlipDetect(ref List<OEpoch> epoches)
        {
        }

        public static void CalP4L4(ref List<OEpoch> epoches,string prn, int start, int end)
        {
            if (epoches is null) return;

            double P1,P2,L1,L2;
            for(int i = start; i <(epoches.Count<(end+1)?epoches.Count:end+1); i++)
            {
                if(epoches[i].AllSat[prn].ObsData.TryGetValue("P1",out P1)
                    &&epoches[i].AllSat[prn].ObsData.TryGetValue("P2",out P2)
                    &&epoches[i].AllSat[prn].ObsData.TryGetValue("L1",out L1)
                    &&epoches[i].AllSat[prn].ObsData.TryGetValue("L1",out L2))
                {
                    //// P4 单位米
                    epoches[i].AllSat[prn].ObsData.Add("P4", P1 - P2);

                    //// L4 单位米
                    epoches[i].AllSat[prn].ObsData.Add("L4", L1*Common.lamda1 - L2*Common.lamda2);
                }
            }
        }

        /// <summary>
        /// 相位平滑伪距
        /// </summary>
        /// <param name="epoches">所有的观测历元</param>
        /// <param name="prn">卫星编号</param>
        /// <param name="start">弧段开始历元索引</param>
        /// <param name="end">弧段结束历元索引</param>
        /// <returns></returns>
        public static bool SmoothP4(ref List<OEpoch> epoches, string prn, int start, int end)
        {
            double power = 1;
            int count = 0;
            for(int i = start; i < end+1; i++)
            {
                power = 1 - (i - start) * 0.01d;
                if (power < 0.01) power = 0.01;

                if (i == start) epoches[i].AllSat[prn].ObsData.Add("P4_smooth",epoches[i].AllSat[prn].ObsData["P4"]);
                else
                {
                    epoches[i].AllSat[prn].ObsData.Add("P4_smooth",epoches[i-1].AllSat[prn].ObsData["P4"]+
                        power*(epoches[i].AllSat[prn].ObsData["P4"]-epoches[i-1].AllSat[prn].ObsData["P4"])+
                        (1-power)*(epoches[i].AllSat[prn].ObsData["L4"]-epoches[i-1].AllSat[prn].ObsData["L4"]));
                }

                count++;
            }

            return true;
        }
    }
}
