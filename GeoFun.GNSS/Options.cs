using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class Options
    {
        public static char SATELLITE_SYS = 'G';
        /// <summary>
        /// 观测弧段最短长度
        /// </summary>
        public static int ARC_MIN_LENGTH = 100;

        /// <summary>
        /// 截止高度角
        /// </summary>
        public static double CutOffAngle = 30;

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

        /// <summary>
        /// 伪距粗差探测阈值P1-C1(m)
        /// </summary>
        /// <remarks>
        /// 张小红, 郭斐, 李盼, et al. GNSS精密单点定位中的实时质量控制[J]. 武汉大学学报·信息科学版, 2012, 37(8): 940-944. ZHANG Xiaohong， GUO Fei， LI Pan， ZUO Xiang. Real-time Quality Control Procedure for GNSS Precise Point Positioning. GEOMATICS AND INFORMATION SCIENCE OF WUHAN UNIVERS, 2012, 37(8):940-944.
        /// </remarks>
        public static double OUTLIER_P1C1 = 10;
        /// <summary>
        /// 伪距粗差探测阈值P1-C2(m)
        /// </summary>
        /// <remarks>
        /// 张小红, 郭斐, 李盼, et al. GNSS精密单点定位中的实时质量控制[J]. 武汉大学学报·信息科学版, 2012, 37(8): 940-944. ZHANG Xiaohong， GUO Fei， LI Pan， ZUO Xiang. Real-time Quality Control Procedure for GNSS Precise Point Positioning. GEOMATICS AND INFORMATION SCIENCE OF WUHAN UNIVERS, 2012, 37(8):940-944.
        /// </remarks>
        public static double OUTLIER_P1P2 = 30;

        /// <summary>
        /// gf组合周跳探测阈值(周)
        /// </summary>
        public static double Threshold_GF = 0.28;

        /// <summary>
        /// 卫星dcb处理方式(默认从文件读取)
        /// </summary>
        public enumDCBOption SatDCB = enumDCBOption.ReadFromFile;
        /// <summary>
        /// 接收机dcb的处理方式(默认作为未知数估计)
        /// </summary>
        public enumDCBOption RecDCB = enumDCBOption.Estimation;

        /// <summary>
        /// 二维电离层建模
        /// </summary>
        public enumIonoModel IonoModel = enumIonoModel.SphericalHarmonic;
        public int IonoModelOrder = 9;
        public int IonoModelDegree = 9;
    }
}
