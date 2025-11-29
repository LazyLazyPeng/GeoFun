using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Schema;
using System.Windows;
using System.Windows.Forms;

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
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private enumJobStatus status = enumJobStatus.New;
        /// <summary>
        /// 任务状态
        /// </summary>
        public enumJobStatus Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
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
            get => maxProgressValue;
            set
            {
                if (maxProgressValue != value && value >= 0)
                {
                    maxProgressValue = value;
                    OnPropertyChanged(nameof(MaxProgressValue));
                }
            }
        }
        private int progressValue = 0;
        public int ProgressValue
        {
            get => progressValue;
            set
            {
                int v = value;
                if (v < 0) v = 0;
                if (v > maxProgressValue) v = maxProgressValue;
                if (progressValue != v)
                {
                    progressValue = v;
                    OnPropertyChanged(nameof(ProgressValue));
                }
            }
        }

        private string log;
        public string Log
        {
            get => log;
            set
            {
                if (log != value)
                {
                    log = value;
                    OnPropertyChanged(nameof(Log));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 统一的触发 PropertyChanged 的方法：
        /// - 在 WPF UI 线程上触发（如果可用），否则直接触发。
        /// </summary>
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler == null) return;

            // 修复：CS0117 错误，Application.Current 只在 WPF 项目中可用
            // 检查是否为 WPF 环境
#if NETCOREAPP || NETFRAMEWORK
            // 如果引用了 WPF（PresentationFramework），则可以使用 Application.Current
            if (Type.GetType("System.Windows.Application") != null)
            {
                var appType = Type.GetType("System.Windows.Application");
                var currentProp = appType?.GetProperty("Current");
                var currentApp = currentProp?.GetValue(null, null);
                var dispatcherProp = currentApp?.GetType().GetProperty("Dispatcher");
                var dispatcher = dispatcherProp?.GetValue(currentApp, null);
                var checkAccessMethod = dispatcher?.GetType().GetMethod("CheckAccess");
                var invokeMethod = dispatcher?.GetType().GetMethod("Invoke", new[] { typeof(Action) });
                if (dispatcher != null && checkAccessMethod != null && invokeMethod != null)
                {
                    bool hasAccess = (bool)checkAccessMethod.Invoke(dispatcher, null);
                    if (!hasAccess)
                    {
                        invokeMethod.Invoke(dispatcher, new object[] { new Action(() => handler(this, new PropertyChangedEventArgs(propertyName))) });
                        return;
                    }
                }
            }
#endif
            // 如果没有 WPF 环境（Application.Current 为 null），直接调用
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
