using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class ProjectionHelper
    {
        /// <summary>
        /// 根据经纬度、平面坐标搜寻中央子午线(二分法查找)
        /// </summary>
        /// <param name="a">长半轴</param>
        /// <param name="f">扁率倒数</param>
        /// <param name="b">纬度(弧度)</param>
        /// <param name="l">经度(弧度)</param>
        /// <param name="x">横坐标(m)</param>
        /// <param name="y">纵坐标(m)</param>
        /// <param name="l1">搜索左边界(弧度)</param>
        /// <param name="l2">搜索右边界(弧度)</param>
        /// <param name="iterationNum">最多搜索的次数</param>
        /// <param name="sigmaMax">中误差限差(mm)</param>
        /// <param name="l0">搜索结果(弧度)</param>
        /// <param name="sigma0">验后单位权中误差(mm)</param>
        /// <returns></returns>
        public bool SearchCenterMeridian(double a, double f
            , List<double> b, List<double> l, List<double> x, List<double> y
            , double l1, double l2
            , int iterationNum, double sigmaMax
            , out double l0, out double sigma0)
        {
            bool flag = false;

            l0 = 0d;
            sigma0 = 0d;

            if (b.Count <= 0) return flag;
            if (l.Count != b.Count
                || x.Count != y.Count
                || x.Count != l.Count) return flag;

            // 椭球
            Ellipsoid ell = new Ellipsoid(a, f);
            // 投影(左边界为l0)
            Projection pjLeft = new Projection(ell, l1);
            // 投影(右边界为l0)
            Projection pjRight = new Projection(ell, l2);
            // 投影(中间为l0)
            Projection pjCenter = new Projection(ell, (l1 + l2) / 2);

            // x最小的点
            int minxIndex = x.IndexOf(x.Min());
            if (minxIndex < 0)
            {
                throw new Exception("无法搜索x值最小的点...");
            }
            int minlIndex = l.IndexOf(l.Min());
            if (minlIndex < 0)
            {
                throw new Exception("无法搜索l值最小的点...");
            }

            if (minxIndex != minlIndex)
            {
                throw new Exception("错误，x最小和l最小不对应,请检查输入坐标!");
            }

            // x最大的点
            int maxxIndex = x.IndexOf(x.Min());
            if (maxxIndex < 0)
            {
                throw new Exception("无法搜索x值最小的点...");
            }
            int maxlIndex = l.IndexOf(l.Min());
            if (maxlIndex < 0)
            {
                throw new Exception("无法搜索l值最小的点...");
            }

            if (maxxIndex != maxlIndex)
            {
                throw new Exception("错误，x最大和l最大不对应,请检查输入坐标!");
            }

            // 大致诊断，看点位是否落在搜索区域内
            double leftx = 0d;
            double lefty = 0d;
            pjRight.Proj(l[minxIndex], b[minxIndex], ref leftx, ref lefty);
            double rightx = 0d;
            double righty = 0d;
            pjLeft.Proj(l[maxlIndex], b[maxlIndex], ref rightx, ref righty);
            if(leftx>x[maxxIndex]
                ||rightx<x[minxIndex])
            {
                //// TODO:有部分点超出了搜索区域范围
            }
            double centerx = 0d;
            double centery = 0d;

            double x1 = 0d;
            double y1 = 0d;
            double x2 = 0d;
            double y2 = 0d;
            double x3 = 0d;
            double y3 = 0d;
            sigma0 = 1000;
            int itNum = 0;
            while (itNum < iterationNum)
            {
                pjRight.Proj(b[0], l[0], ref x1, ref y1);
                pjCenter.Proj(b[0], l[0], ref x2, ref y2);
                pjLeft.Proj(b[0], l[0], ref x2, ref y2);

                //// 在中线右侧
                if(x[0]>=x1&&x[0]<=x2)
                {
                    var center = pjCenter.CenterL.DD;
                    pjCenter.CenterL.DD = (pjCenter.CenterL.DD +pjRight.CenterL.DD)/2;
                    pjLeft.CenterL.DD = center;
                }
                //// 在中线左侧
                else if(x[0]>=x2&&x[0]<=x3)
                {
                    var center = pjCenter.CenterL.DD;
                    pjCenter.CenterL.DD = (pjCenter.CenterL.DD +pjLeft.CenterL.DD)/2;
                    pjRight.CenterL.DD = center;
                }
                else
                {
                    flag = false;
                    break;
                }

                sigma0 = Math.Sqrt(Math.Pow(x[0]-x2,2)+Math.Pow(y[0]-y1,2));

                // 判断限差是否符合条件
                if (sigma0 < sigmaMax)
                {
                    break;
                }

                itNum++;
            }

            return flag;
        }
    }
}
