using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class Coor2
    {
        public double x { get; set; } = 0d;

        public double y { get; set; } = 0d;

        public Coor2(double x, double y, enum2DAxis north = enum2DAxis.X)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// 默认X是北方向
        /// </summary>
        public enum2DAxis North { get; set; } = enum2DAxis.X;

        /// <summary>
        /// 交换xy的值
        /// </summary>
        ///<param name="changeOrientation">是否改变坐标轴指向(默认不改变)</param>
        public void SwapXY(bool changeOrientation = false)
        {
            double temp = x;
            x = y;
            y = temp;

            if (changeOrientation)
            {
                if (North == enum2DAxis.X) North = enum2DAxis.Y;
                else North = enum2DAxis.X;
            }
        }

        /// <summary>
        /// 改变坐标轴指向
        /// </summary>
        public void ChangeOrientation()
        {
            if (North == enum2DAxis.X) North = enum2DAxis.Y;
            else North = enum2DAxis.X;

        }

        /// <summary>
        /// 自动检测xy的方向并返回实例
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Coor2 DetectXY(double x, double y)
        {
            // 6位数判定为东方向,6位以上判定为北方向
            if(x>99999.9999&&x<999999.9999 && y>999999.9999)
            {
                return new Coor2(y, x);
            }
            else if(y>99999.9999&&y<999999.9999 && x>999999.9999)
            {
                return new Coor2(x, y);
            }
            else
            {
                return new Coor2(x, y);
            }
        }
    }
}
