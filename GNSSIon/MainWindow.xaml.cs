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
        public object lockObj = new object();
        public int MaxTaskNum = 2;
        public ObservableCollection<Job> JobList = new ObservableCollection<Job>();
        public ConcurrentQueue<Job> jobQueue = new ConcurrentQueue<Job>();
        public List<Worker> workers = new List<Worker>();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                IonJob job = new IonJob();
                job.ID = i;
                job.Name = i.ToString();
                job.Status = GeoFun.MultiThread.enumJobStatus.New;
                job.MaxProgressValue = 100;
                job.ProgressValue = i * 5;
                job.Log = i.ToString();

                JobList.Add(job);
                jobQueue.Enqueue(job);
            }

            dgJobs.ItemsSource = JobList;

            for (int i = 0; i < MaxTaskNum; i++)
            {
                IonWorker ionw = new IonWorker();
                ionw.Code = i;
                ionw.OnStatusChanged += worker_FinishedJob;
                Worker worker = ionw as Worker;
                worker.Status = enumWorkerStatus.Working;
                workers.Add(ionw);
                worker.Status = enumWorkerStatus.Idle;
            }
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
        }

        private void worker_FinishedJob(object sender, EventArgs e)
        {
            if (sender is null) return;
            IonWorker worker = sender as IonWorker;
            if (worker is null) return;
            if (worker.Status == enumWorkerStatus.Working) return;

            Job job = ApplyJob();
            worker.MyJob = job as IonJob;

            worker.Work();
        }

        public Job ApplyJob()
        {
            lock (lockObj)
            {
                Job job = null;
                if (!jobQueue.TryDequeue(out job))
                {
                    while (!jobQueue.TryDequeue(out job)||job is null)
                    {
                        Thread.Sleep(2000);
                    }
                }
                return job;
            }
        }
    }
}
