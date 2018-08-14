using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun 
{
    public enum enumCoordinateType
    {
        BL = 2,
        BLH = 3,
        xy = 0,
        yx = 1,
        LB = 4,
        LBH = 5,
    }

    internal class CoorSystemPara
    {
        public enumCoordinateType CoorType { get; set; }

        /// <summary>
        /// 中央子午线(弧度)
        /// </summary>
        public double L0 { get; set; }

        /// <summary>
        /// 西向平移量(米)
        /// </summary>
        public double FalseEasting { get; set; }
        /// <summary>
        /// 北向平移量(米)
        /// </summary>
        public double FalseNorthing { get; set; }
    }
}
