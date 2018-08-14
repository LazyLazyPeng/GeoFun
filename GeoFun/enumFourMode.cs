using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    /// <summary>
    /// 四参数模型
    /// </summary>
    public enum enumFourMode
    {
        /// <summary>
        /// O(ffset) R(otate) S(cale)
        /// </summary>
        ORS,
        /// <summary>
        /// O(ffset) S(cale) R(otate)
        /// </summary>
        OSR,
        /// <summary>
        /// R(otate) S(cale) O(ffset)
        /// </summary>
        RSO,
        /// <summary>
        /// S(cale) R(otateI) O(ffset)
        /// </summary>
        SRO,
    }
}
