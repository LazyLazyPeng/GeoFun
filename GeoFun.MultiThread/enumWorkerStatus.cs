using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MultiThread
{
    public enum enumWorkerStatus
    {
        /// <summary>
        /// 空闲
        /// </summary>
        Idle=0,
        /// <summary>
        /// 正在运行
        /// </summary>
        Working=1,
    }
}
