using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GeoFun.GNSS;

namespace GNSSIon
{
    /// <summary>
    /// WinIonoOption.xaml 的交互逻辑
    /// </summary>
    public partial class WinIonoOption : Window
    {
        public IonoOption Option { get; set; } = new IonoOption();

        public WinIonoOption()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Option.RootFolder = tbxFolderIn.Text;
            Option.CutOffAngle = double.Parse(tbxCutOffAngle.Text);
            Option.MinArcLength = int.Parse(tbxMinArcLength.Text);

            Option.IonoModelType = enumIonoModel.Polinomial;
            if(rbSpheric.IsChecked == true)
            {
                Option.IonoModelType = enumIonoModel.SphericalHarmonic;
            }

            Option.SolType = enumSolType.SingleStationSingleDay;
            if(multiStations.IsChecked == true)
            {
                Option.SolType = enumSolType.MultiStationSingleDay;
            }

            if (chkIPPB.IsChecked == true)
            {
                if (!Option.OutputItems.Contains("ippb"))
                {
                    Option.OutputItems.Add("ippb");
                }
            }
            if (chkIPPL.IsChecked == true)
            {
                if (!Option.OutputItems.Contains("ippl"))
                {
                    Option.OutputItems.Add("ippl");
                }
            }
            if (chkSTEC.IsChecked == true)
            {
                if (!Option.OutputItems.Contains("stec"))
                {
                    Option.OutputItems.Add("stec");
                }
            }
            if (chkVTEC.IsChecked == true)
            {
                if (!Option.OutputItems.Contains("vtec"))
                {
                    Option.OutputItems.Add("vtec");
                }
            }
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(tbxFolderIn.Text))
            {
                fbd.SelectedPath = tbxFolderIn.Text;
            }

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxFolderIn.Text = fbd.SelectedPath;
            }
        }
    }
}
