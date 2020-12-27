using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GNSSIon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int MaxTaskNum = 2;

        public MainWindow()
        {
            InitializeComponent();
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
