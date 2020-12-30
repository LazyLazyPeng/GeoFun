using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.MultiThread;

namespace GNSSIon
{
    public class IonJob:Job
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public string RootFolder { get; set; } = "";
    }
}
