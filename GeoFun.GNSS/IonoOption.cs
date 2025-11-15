using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.GNSS;

namespace GNSSIon
{
    public class IonoOption
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootFolder { get; set; } = "";
        /// <summary>
        /// 解算类型
        /// </summary>
        public enumSolType SolType { get; set; } = enumSolType.SingleStationSingleDay;

        /// <summary>
        /// 截止高度角
        /// Gets or sets the cutoff angle, in degrees, used to define the threshold for filtering or processing
        /// operations.
        /// </summary>
        public double CutOffAngle { get; set; } = 15.0;

        /// <summary>
        /// 最短弧长（历元）
        /// Gets or sets the minimum arc length, in units, used for calculations or rendering.
        /// </summary>
        public int MinArcLength { get; set; } = 30;

        /// <summary>
        /// 电离层函数模型
        /// get or set the ionospheric model type
        /// </summary>
        public enumIonoModel IonoModelType { get; set; } = enumIonoModel.Polinomial;

        /// <summary>
        /// 输出项
        /// </summary>
        public List<string> OutputItems { get; set; } =
            new List<string> { "ippb", "ippl", "stec", "vtec" };
    }
}
