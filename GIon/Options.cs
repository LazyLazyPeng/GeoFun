﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using GeoFun.GNSS;

namespace GIon
{
    public class Option
    {
        /// <summary>
        /// 要处理的系统
        /// </summary>
        public static string SAT_SYS = "G";

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
        /// 截止高度角(度)
        /// </summary>
        public static double ElevationMaskAngle = 15;

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
        /// 卫星dcb处理方式(默认从文件读取)
        /// </summary>
        public enumDCBOption SatDCB = enumDCBOption.Estimation;
        /// <summary>
        /// 接收机dcb的处理方式(默认作为未知数估计)
        /// </summary>
        public enumDCBOption RecDCB = enumDCBOption.Estimation;

        /// <summary>
        /// 建模区域(度)
        /// </summary>
        public double MODEL_AREA_LEFT = 0d;
        /// <summary>
        /// 建模区域(度)
        /// </summary>
        public double MODEL_AREA_RIGHT = 0d;
        /// <summary>
        /// 建模区域(度)
        /// </summary>
        public double MODEL_AREA_TOP = 0d;
        /// <summary>
        /// 建模区域(度)
        /// </summary>
        public double MODEL_AREA_BOTTOM = 0d;
        /// <summary>
        /// 模型分辨率(度)
        /// </summary>
        public double MODEL_RESOLUTION = 0.05;
    }
}
