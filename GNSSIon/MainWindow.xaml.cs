using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using GeoFun.MultiThread;

namespace GNSSIon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int MaxTaskNum = 2;
        public ObservableCollection<Job> JobList = new ObservableCollection<Job>();

        public MainWindow()
        {
            InitializeComponent();

            for(int i = 0; i < 5; i++)
            {
                IonJob job = new IonJob();
                job.ID = i;
                job.Name = i.ToString();
                job.Status = GeoFun.MultiThread.enumJobStatus.New;
                job.MaxProgressValue = 100;
                job.ProgressValue = i*20;
                job.Log = i.ToString();

                JobList.Add(job);
            }

            dgJobs.ItemsSource = JobList;

            ThreadStart st = new ThreadStart(() => {
                Thread.Sleep(2000);
                for (int i = 0; i < 20; i++)
                {
                    JobList[0].ProgressValue = i*5+5;
                    Thread.Sleep(1000);
                }
                JobList[0].Status = enumJobStatus.Finished;
            });

            Thread th = new Thread(st);
            th.Start();
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            WinDownload win = new WinDownload();
            win.Show();
        }

        private void menuObsExtract_Click(object sender, RoutedEventArgs e)
        {
            WinObsExtract win = new WinObsExtract();
            win.Show();

            // 下载星历、dcb
            // 读取文件
            // 计算轨道、穿刺点
            // 估计接收机dcb
            // 计算STE/CVTEC
            // 输出文件
        }
    }
}
