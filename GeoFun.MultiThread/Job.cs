using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Schema;

namespace GeoFun.MultiThread
{
    public class Job : INotifyPropertyChanged
    {
        private int id = 0;
        /// <summary>
        /// 编号 
        /// </summary>
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                if (id != value)
                {
                    id = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ID"));
                }
            }
        }

        private string name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                    }
                }
            }
        }

        private enumJobStatus status = enumJobStatus.New;
        /// <summary>
        /// 任务状态
        /// </summary>
        public enumJobStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        /// <summary>
        /// 其它属性
        /// </summary>
        public Dictionary<string, object> Properties { get; set; }

        private int maxProgressValue = 100;
        public int MaxProgressValue
        {
            get
            {
                return maxProgressValue;
            }
            set
            {
                if (maxProgressValue != value && value >= 0)
                {
                    maxProgressValue = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxProgressValue"));
                }
            }
        }
        private int progressValue = 0;
        public int ProgressValue
        {
            get
            {
                return progressValue;
            }
            set
            {
                int value1 = value;
                if (value1 < 0) value1 = 0;
                if (value1 > maxProgressValue) value1 = maxProgressValue;
                if (progressValue != value1)
                {
                    progressValue = value1;

                    PropertyChanged?.Invoke("ProgressValue", new PropertyChangedEventArgs("ProgressValue"));
                }
            }
        }

        private string log;
        public string Log
        {
            get
            {
                return log;
            }
            set
            {
                if (log != value)
                {
                    log = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Log"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
