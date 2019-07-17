using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public interface IFeature
    {
        /// <summary>
        /// 是否有M值
        /// </summary>
        bool HasM { get; set; }

        /// <summary>
        /// 所有点的数量
        /// </summary>
        int NumPoints { get; set; }

        /// <summary>
        /// 获取点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        Point GetPointAt(int index);

        /// <summary>
        /// 获取点的xy坐标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void GetPointAt(int index, out double x, out double y);

        /// <summary>
        /// 设置点
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pt"></param>
        void SetPointAt(int index, Point pt);

        /// <summary>
        /// 设置点的xy坐标
        /// </summary>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetPointAt(int index, double x, double y);

        /// <summary>
        /// Size in bytes
        /// </summary>
        //int Size { get; }
    }
}
