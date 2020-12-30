using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MultiThread
{
    public abstract class Worker
    {
        public delegate void NumChangeEventHandler(int value);
        public delegate void ExceptionEventHandler(Exception ex);

        public event NumChangeEventHandler OnProgressChanged;
        public event NumChangeEventHandler OnProgressMaxChanged;
        public event EventHandler OnStatusChanged;

        /// <summary>
        /// 编号 
        /// </summary>
        public int Code { get; set; }

        private enumWorkerStatus status = enumWorkerStatus.Idle;
        /// <summary>
        /// 任务状态
        /// </summary>
        public enumWorkerStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if(status!=value)
                {
                    status = value;
                    OnStatusChanged(this, new EventArgs());
                }
            }
        }

        public int maxProgressValue = 100;
        public int MaxProgressValue
        {
            get
            {
                return maxProgressValue;
            }
            set
            {
                if (maxProgressValue == value) return;

                int pr = value;
                if (pr < 0) pr = 0;
                maxProgressValue = pr;
                OnProgressMaxChanged(pr);
            }
        }
        public int curProgressValue = 0;
        public int CurProgressValue
        {
            get
            {
                return curProgressValue;
            }
            set
            {
                if (value == curProgressValue) return;

                int pr = value;
                if (pr > maxProgressValue) pr = maxProgressValue;
                if (pr < 0) pr = 0;
                curProgressValue = pr;
                OnProgressChanged(pr);
            }
        }

        public abstract void Work();
    }
}
