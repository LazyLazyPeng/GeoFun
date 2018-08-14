using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class FourPara
    {
        /// <summary>
        /// 四参数模型，默认与PowerCoor一致
        /// </summary>
        public enumFourMode Mode { get; set; } = enumFourMode.ORS;

        /// <summary>
        /// X平移量(米),默认0
        /// </summary>
        public double DX { get; set; } = 0d;
        /// <summary>
        /// Y平移量(米),默认0
        /// </summary>
        public double DY { get; set; } = 0d;
        /// <summary>
        /// 旋转量(秒),默认0
        /// </summary>
        public double R { get; set; } = 0d;
        /// <summary>
        /// 尺度(ppm),默认0
        /// </summary>
        public double S { get; set; } = 0d;

        /// <summary>
        /// 四参数转仿射参数
        /// </summary>
        /// <param name="four"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        public static void Four2Affine(FourPara four, out double A, out double B, out double C, out double D, out double E, out double F)
        {
            Four2Affine(four.DX, four.DY, four.R, four.S, Mode2Str(four.Mode), out A, out B, out C, out D, out E, out F);
        }

        /// <summary>
        /// 将四参数变为仿射参数  
        ///     仿射参数公式为 
        ///     X2 = A*X1 + B*Y1 + C
        ///     Y2 = D*X1 + E*Y1 + F
        /// </summary>
        /// <param name="dx">平移量(米)</param>
        /// <param name="dy">平移量(米)</param>
        /// <param name="r">旋转量(弧度)</param>
        /// <param name="s">尺度(ppm)</param>
        /// <param name="mode">模型 三个字母，每个字母表示一个顺序 o(ffset) R(otate) S(cale) 例如powercoor模型为ors</param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        public static void Four2Affine(double dx, double dy, double r, double s, string mode, out double A, out double B, out double C,
            out double D, out double E, out double F)
        {
            A = 1d; B = 0d; C = 0d;
            D = 0d; E = 1d; F = 0d;

            A = (1 + s * 1e-6) * Math.Cos(r);
            B = (1 + s * 1e-6) * Math.Sin(r);
            D = -(1 + s * 1e-6) * Math.Sin(r);
            E = (1 + s * 1e-6) * Math.Cos(r);

            if (mode.StartsWith("o"))
            {
                C = (1 + s * 1e-6) * (Math.Cos(r) * dx + Math.Sin(r) * dy);
                F = (1 + s * 1e-6) * (-Math.Sin(r) * dx + Math.Cos(r) * dy);
            }
            else if (mode.EndsWith("o"))
            {
                C = dx;
                F = dy;
            }
        }

        /// <summary>
        /// 将四参数变为仿射参数  
        ///     仿射参数公式为 
        ///     X2 = A*X1 + B*Y1 + C
        ///     Y2 = D*X1 + E*Y1 + F
        /// </summary>
        /// <param name="dx">平移量(米)</param>
        /// <param name="dy">平移量(米)</param>
        /// <param name="r">旋转量(弧度)</param>
        /// <param name="s">尺度(ppm)</param>
        /// <param name="mode">模型 三个字母，每个字母表示一个顺序 o(ffset) R(otate) S(cale) 例如powercoor模型为ors</param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="D"></param>
        /// <param name="E"></param>
        /// <param name="F"></param>
        public static void Four2Affine(double dx, double dy, double r, double s, enumFourMode mode, out double A, out double B, out double C,
            out double D, out double E, out double F)
        {
            Four2Affine(dx, dy, r, s, Mode2Str(mode), out A, out B, out C, out D, out E, out F);
        }

        /// <summary>
        /// 根据一种四参数模型的参数推导另一种四参数模型的参数
        /// </summary>
        /// <param name="dx1">平移量(米)</param>
        /// <param name="dy1">平移量(米)</param>
        /// <param name="r1">旋转量(弧度)</param>
        /// <param name="s1">尺度(ppm)</param>
        /// <param name="mode1">输入模型</param>
        /// <param name="dx2">平移量(米)</param>
        /// <param name="dy2">平移量(米)</param>
        /// <param name="r2">旋转量(弧度)</param>
        /// <param name="s2">尺度(ppm)</param>
        /// <param name="mode2">输出模型</param>
        public static void ChangeMode(double dx1, double dy1, double r1, double s1, enumFourMode mode1,
            out double dx2, out double dy2, out double r2, out double s2, enumFourMode mode2)
        {
            string modeStr1 = Mode2Str(mode1);
            string modeStr2 = Mode2Str(mode2);

            ChangeMode(dx1, dy1, r1, s1, modeStr1, out dx2, out dy2, out r2, out s2, modeStr2);
        }

        /// <summary>
        /// 根据一种四参数模型的参数推导另一种四参数模型的参数
        /// </summary>
        /// <param name="dx1">平移量(米)</param>
        /// <param name="dy1">平移量(米)</param>
        /// <param name="r1">旋转量(弧度)</param>
        /// <param name="s1">尺度(ppm)</param>
        /// <param name="mode1">输入模型</param>
        /// <param name="dx2">平移量(米)</param>
        /// <param name="dy2">平移量(米)</param>
        /// <param name="r2">旋转量(弧度)</param>
        /// <param name="s2">尺度(ppm)</param>
        /// <param name="mode2">输出模型</param>
        public static void ChangeMode(double dx1, double dy1, double r1, double s1, string mode1,
            out double dx2, out double dy2, out double r2, out double s2, string mode2)
        {
            dx2 = dy2 = r2 = s2 = 0d;
            if (string.IsNullOrWhiteSpace(mode1)) return;

            if (mode1.StartsWith("o"))
            {
                if (mode2.StartsWith("o"))
                {
                    dx2 = dx1;
                    dy2 = dy1;
                    r2 = r1;
                    s2 = s1;
                }
                else
                {
                    r2 = r1;
                    s2 = s1;
                    dx2 = (1 + s1 * 1e-6) * (Math.Cos(r1) * dx1 + Math.Sin(r1) * dy1);
                    dy2 = (1 + s1 * 1e-6) * (-Math.Sin(r1) * dx1 + Math.Cos(r1) * dy1);
                }
            }
            else if (mode1.EndsWith("o"))
            {
                if (mode2.EndsWith("o"))
                {
                    dx2 = dx1;
                    dy2 = dy1;
                    r2 = r1;
                    s2 = s1;
                }
                else
                {
                    r2 = r1;
                    s2 = s1;
                    dx2 = 1 / (1 + s1 * 1e-6) * (Math.Cos(r1) * dx1 - Math.Sin(r1) * dy1);
                    dy2 = 1 / (1 + s1 * 1e-6) * (Math.Sin(r1) * dx1 + Math.Cos(r1) * dy1);
                }
            }
        }

        /// <summary>
        /// 根据一种四参数模型的参数推导另一种四参数模型的参数
        /// </summary>
        /// <param name="inFour">输入四参数</param>
        /// <param name="outMode">输出模型</param>
        /// <returns></returns>
        public static FourPara ChangeMode(FourPara inFour, enumFourMode outMode)
        {
            if (inFour is null) return null;

            double dx2, dy2, r2, s2;
            ChangeMode(inFour.DX, inFour.DY, inFour.R, inFour.S, inFour.Mode, out dx2, out dy2, out r2, out s2, outMode);
            FourPara outFour = new FourPara
            {
                DX = dx2,
                DY = dy2,
                R = r2,
                S = s2,
                Mode = outMode,
            };

            return outFour;
        }

        /// <summary>
        /// 根据正算的四参数 计算 反算的四参数
        /// </summary>
        /// <param name="mode">四参数模型</param>
        /// <param name="dx1">正算:平移量(米)</param>
        /// <param name="dy1">正算:平移量(米)</param>
        /// <param name="r1">正算:旋转量(弧度)</param>
        /// <param name="s1">正算:尺度(ppm)</param>
        /// <param name="dx2">反算:平移量(米)</param>
        /// <param name="dy2">反算:平移量(米)</param>
        /// <param name="r2">反算:旋转量(弧度)</param>
        /// <param name="s2">反算:尺度(ppm)</param>
        public static void GetInv(string mode, double dx1, double dy1, double r1, double s1,
            out double dx2, out double dy2, out double r2, out double s2)
        {
            if (mode.StartsWith("o"))
            {
                s2 = (1d / (1 + s1 * 1e-6) - 1d) * 1e6;
                r2 = -r1;

                dx2 = -(1 + s1 * 1e-6) * (Math.Cos(r1) * dx1 + Math.Sin(r1) * dy1);
                dy2 = -(1 + s1 * 1e-6) * (-Math.Sin(r1) * dx1 + Math.Cos(r1) * dy1);

            }
            else if (mode.EndsWith("o"))
            {
                r2 = -r1;
                s2 = (1d / (1d + s1 * 1e-6) - 1d) * 1e6;

                dx2 = -(1 + s2 * 1e-6) * (Math.Cos(r2) * dx1 + Math.Sin(r2) * dy1);
                dy2 = -(1 + s2 * 1e-6) * (-Math.Sin(r2) * dx1 + Math.Cos(r2) * dy1);
            }
            else
            {
                throw new Exception("无法识别的模型");
            }
        }

        /// <summary>
        /// 根据正算的四参数计算反算的四参数
        /// </summary>
        /// <param name="inFour">正算四参数</param>
        /// <returns>反算四参数</returns>
        public static FourPara GetInv(FourPara inFour)
        {
            if (inFour is null) return null;

            string modeStr = Mode2Str(inFour.Mode);

            double dx, dy, r, s;
            GetInv(modeStr, inFour.DX, inFour.DY, inFour.R, inFour.S, out dx, out dy, out r, out s);

            FourPara outFour = new FourPara
            {
                DX = dx,
                DY = dy,
                R = r,
                S = s,
                Mode = inFour.Mode,
            };

            return outFour;
        }

        public static string Mode2Str(enumFourMode mode)
        {
            switch (mode)
            {
                case enumFourMode.ORS:
                    return "ors";
                case enumFourMode.OSR:
                    return "osr";
                case enumFourMode.RSO:
                    return "rso";
                case enumFourMode.SRO:
                    return "sro";
                default:
                    return "ors";
            }
        }
    }
}
