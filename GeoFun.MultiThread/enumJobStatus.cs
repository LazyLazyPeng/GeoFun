using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MultiThread
{
    public enum enumJobStatus
    {
        /// <summary>
        /// 新创建，尚未运行
        /// </summary>
        New=0,
        /// <summary>
        /// 正在运行
        /// </summary>
        Running=1,
        /// <summary>
        /// 已完成
        /// </summary>
        Finished=9,
        /// <summary>
        /// 暂停
        /// </summary>
        Paused=-1,
        /// <summary>
        /// 报错
        /// </summary>
        Error=-99,
    }
}
