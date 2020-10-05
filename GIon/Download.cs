using DevComponents.DotNetBar.Controls;
using GeoFun.GNSS.Net;
using GeoFun.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GIon
{
    public partial class Download : Form
    {
        public Download()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            lbxStations.Items.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "所有文件(*.*)|*.*|文本文件(*.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var lines = File.ReadAllLines(ofd.FileName);
                foreach (var line in lines)
                {
                    lbxStations.Items.Add(line);
                    lbxStations.SetItemChecked(lbxStations.Items.Count - 1, true);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxOutFolder.Text = fbd.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;

            //if (dt1 > dt2)
            //{
            //    Print("开始日期大于结束日期,没有数据可以下载");
            //}

            List<string> staNames = new List<string>();
            for (int i = 0; i < lbxStations.Items.Count; i++)
            {
                if (lbxStations.GetItemChecked(i))
                {
                    staNames.Add(lbxStations.Items[i].ToString());
                }
            }
            //if (staNames.Count <= 0) return;
            string outFolder = tbxOutFolder.Text;

            Task task = new Task(() =>
            {
                List<string[]> lines = FileHelper.ReadThenSplitLine(@"C:\Users\Administrator\Desktop\info.txt", ' ');
                foreach (var line in lines)
                {
                    string tyName = line[1].ToLower();
                    string area = line[2];
                    string stDate = line[3];
                    string edDate = line[4];

                    if (area == "t")
                    {
                        staNames = new List<string>(File.ReadAllLines(@"C: \Users\Administrator\Desktop\taiwan.txt"));
                    }
                    else
                    {
                        staNames = new List<string>(File.ReadAllLines(@"C: \Users\Administrator\Desktop\japan.txt"));
                    }

                    dt1 = DateTime.ParseExact(stDate, "yyyyMMdd", CultureInfo.InvariantCulture);
                    dt2 = DateTime.ParseExact(edDate, "yyyyMMdd", CultureInfo.InvariantCulture);

                    dt1 = dt1.AddDays(-3);
                    dt2 = dt2.AddDays(+3);

                    outFolder = Path.Combine(@"E:\Data\Graduation\chapter3\igs", tyName);
                    if (!Directory.Exists(outFolder))
                    {
                        Directory.CreateDirectory(outFolder);
                    }

                    foreach (var sta in staNames)
                    {
                        Print("开始下载测站:" + sta);

                        var dt = dt1;
                        int n = 0;
                        while (dt <= dt2 && n < 1000)
                        {
                            string fileName1 = string.Format("{0}{1:D3}0.{2:D2}o", sta, dt.DayOfYear, dt.Year - 2000);
                            string fileName2 = string.Format("{0}{1:D3}2.{2:D2}o", sta, dt.DayOfYear, dt.Year - 2000);
                            string filePath1 = Path.Combine(outFolder, fileName1);
                            string filePath2 = Path.Combine(outFolder, "obs", fileName2);

                            if (File.Exists(filePath1) || File.Exists(filePath2))
                            {
                                dt = dt.AddDays(1);
                                n++;
                                continue;
                            }


                            Print(string.Format("正在下载 {0} {1}", dt.Year, dt.DayOfYear));
                            Downloader.DownloadO(dt.Year, dt.DayOfYear, sta, outFolder, "WHU");
                            dt = dt.AddDays(1);
                            n++;
                        }
                    }
                }
            });
            task.Start();
        }

        private void Print(string msg)
        {
            tbxInfo.Invoke(new Action(() =>
            {
                tbxInfo.AppendText(string.Format("{0}:{1}\r\n", DateTime.Now, msg));
            }));
        }
    }
}
