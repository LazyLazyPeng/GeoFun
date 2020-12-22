using Microsoft.Win32;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GeoFun.GNSS;

namespace GNSSIon
{
    /// <summary>
    /// Interaction logic for WinObsExtract.xaml
    /// </summary>
    public partial class WinObsExtract : Window
    {
        public WinObsExtract()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "o文件(*.??o)|*.??o|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == true)
            {
                tbxPath.Text = ofd.FileName;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string path = tbxPath.Text;
            if (!File.Exists(path))
            {
                MessageBox.Show("文件不存在");
                return;
            }

            OFile of = new OFile(path);
            if (!of.TryRead())
            {
                MessageBox.Show("读取文件失败");
                return;
            }

            var UTF8NoBOM = new System.Text.UTF8Encoding(false);

            string pathL1 = of.Path.Substring(0, of.Path.Length - 4) + ".meas.l1";
            string pathL2 = of.Path.Substring(0, of.Path.Length - 4) + ".meas.l2";
            string pathP1 = of.Path.Substring(0, of.Path.Length - 4) + ".meas.p1";
            string pathP2 = of.Path.Substring(0, of.Path.Length - 4) + ".meas.p2";
            string pathC1 = of.Path.Substring(0, of.Path.Length - 4) + ".meas.c1";

            StreamWriter writerL1 = new StreamWriter(new FileStream(pathL1, FileMode.Create, FileAccess.Write), UTF8NoBOM);
            StreamWriter writerL2 = new StreamWriter(new FileStream(pathL2, FileMode.Create, FileAccess.Write), UTF8NoBOM);
            StreamWriter writerP1 = new StreamWriter(new FileStream(pathP1, FileMode.Create, FileAccess.Write), UTF8NoBOM);
            StreamWriter writerP2 = new StreamWriter(new FileStream(pathP2, FileMode.Create, FileAccess.Write), UTF8NoBOM);
            StreamWriter writerC1 = new StreamWriter(new FileStream(pathC1, FileMode.Create, FileAccess.Write), UTF8NoBOM);

            OSat os;
            OEpoch oe;
            string prn;
            string date;
            for (int i = 0; i < of.Epoches.Count; i++)
            {
                oe = of.Epoches[i];
                date = string.Format("{0},{1}", oe.Epoch.Year, oe.Epoch.DOYTime);
                writerL1.Write(date);
                writerL2.Write(date);
                writerP1.Write(date);
                writerP2.Write(date);
                writerC1.Write(date);

                for (int j = 1; j <= 32; j++)
                {
                    if (j < 10) prn = "G0" + j.ToString();
                    else prn = "G" + j.ToString();

                    writerL1.Write(',');
                    writerL2.Write(',');
                    writerP1.Write(',');
                    writerP2.Write(',');
                    writerC1.Write(',');

                    if (!oe.AllSat.ContainsKey(prn))
                    {
                        writerL1.Write("0.0000000000");
                        writerL2.Write("0.0000000000");
                        writerP1.Write("0.000");
                        writerP2.Write("0.000");
                        writerC1.Write("0.000");
                        continue;
                    }

                    os = oe[prn];
                    writerL1.Write(os["L1"].ToString("#.000"));
                    writerL2.Write(os["L2"].ToString("#.000"));
                    writerP1.Write(os["P1"].ToString("#.000"));
                    writerP2.Write(os["P2"].ToString("#.000"));
                    writerC1.Write(os["C1"].ToString("#.000"));
                }

                writerL1.Write("\r\n");
                writerL2.Write("\r\n");
                writerP1.Write("\r\n");
                writerP2.Write("\r\n");
                writerC1.Write("\r\n");
            }

            writerL1.Close();
            writerL2.Close();
            writerP1.Close();
            writerP2.Close();
            writerC1.Close();

            MessageBox.Show("提取成功!");
        }
    }
}
