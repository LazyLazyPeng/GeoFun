using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MultiThread
{
    public abstract class Worker
    {
        public delegate void SimpleEnventHandler();
        public delegate void NumChangeEventHandler(int value);
        public delegate void ExceptionEventHandler(Exception ex);

        public event SimpleEnventHandler OnCreated;
        public event SimpleEnventHandler OnStarted;
        public event SimpleEnventHandler OnFinished;
        public event ExceptionEventHandler OnExceptionThrowed;

        public event NumChangeEventHandler OnProgressChanged;
        public event NumChangeEventHandler OnProgressMaxChanged;

        /// <summary>
        /// 编号 
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public enumWorkerStatus Status { get; set; }

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
