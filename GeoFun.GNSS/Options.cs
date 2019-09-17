using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Options
    {
        /// <summary>
        /// 观测弧段最短长度
        /// </summary>
        public static int ARC_MIN_LENGTH = 100;

        /// <summary>
        /// 设置数据处理开始的时间
        /// </summary>
        public static GPST START_TIME = new GPST(2013,7,12,20,0,0m);

        /// <summary>
        /// 数据处理结束的时间
        /// </summary>
        public static GPST END_TIME = new GPST(2013,7,13,4,0,0m);

        /// <summary>
        /// 周跳探测方法(0-MW 1-Turbo-Edit)
        /// </summary>
        public static int CYCLE_SLIP_DETECT_FUNCTION = 0;

        /// <summary>
        /// 电离层建模类型(0-球谐 1-多项式 2-球冠谐)
        /// </summary>
        public static int IONOSPHERE_MODEL = 0;
    }
}
