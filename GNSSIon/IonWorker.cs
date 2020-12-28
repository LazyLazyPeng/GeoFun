using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.MultiThread;

namespace GNSSIon
{
    public class IonWorker : Worker
    {
        public IonJob MyJob;

        public override void Work()
        {
            Status = enumWorkerStatus.Working;
            Status = enumWorkerStatus.Idle;
        }
    }
}
