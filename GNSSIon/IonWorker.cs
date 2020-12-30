using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.GNSS;
using GeoFun.MultiThread;

namespace GNSSIon
{
    public class IonWorker : Worker
    {
        public IonJob MyJob;

        public override void Work()
        {
            Action action = new Action(() =>
            {
                Status = enumWorkerStatus.Working;
                try
                {
                    if (MyJob is null) return;
                    try
                    {
                        Iono.Resolve();
                        MyJob.Status = enumJobStatus.Finished;
                        MyJob.ProgressValue = 100;
                        MyJob.Log = "由Worker"+Code.ToString()+"完成";
                    }
                    catch (Exception ex)
                    {
                        MyJob.Status = enumJobStatus.Error;
                        MyJob.Log += "\n" + ex.ToString();
                    }
                }
                finally
                {
                    Status = enumWorkerStatus.Idle;
                }
            });
            Task.Run(action);
        }
    }
}
