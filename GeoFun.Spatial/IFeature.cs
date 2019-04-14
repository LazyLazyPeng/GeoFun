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
        /// Size in bytes
        /// </summary>
        //int Size { get; }
    }
}
