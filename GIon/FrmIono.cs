using GeoFun.GNSS;
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
    public partial class FrmIono : Form
    {
        public static MessageHelper MessHelper;
        public FrmIono()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbxFolderIn.Text))
            {
                MessageBox.Show("文件夹不存在");
                return;
            }

            var messHelper = new MessageHelper(tbxMsg);
            Common.msgBox = messHelper;

            enumSolutionType solType = enumSolutionType.SingleStationSingleDay;
            if (rbSingleStationMultiDay.Checked)
            {
                solType = enumSolutionType.SingleStationMultiDay;
            }
            else if (rbMultiStationMultiDay.Checked)
            {
                solType = enumSolutionType.MultiStationMultiDay;
            }

            enumFitType fitType = enumFitType.Polynomial;
            if(rbDifference.Checked)
            {
                fitType = enumFitType.DoubleDifference;
            }

            Case ionoCase = new Case(tbxFolderIn.Text);
            ionoCase.FitType = fitType;
            MessHelper = messHelper;

            Task task = new Task(() =>
            {
                DirectoryInfo dir = new DirectoryInfo(tbxFolderIn.Text);
                switch(solType)
                {
                    case enumSolutionType.SingleStationSingleDay:
                        ionoCase.ResolveSingleStationSingleDay();
                        break;
                    case enumSolutionType.SingleStationMultiDay:
                        ionoCase.ResolveSingleStationMultiDay();
                        break;
                    case enumSolutionType.MultiStationMultiDay:
                        ionoCase.MultiStationMultiDay();
                        break;
                    default:
                        break;
                }
            });
            task.ContinueWith(t => MessageBox.Show("解算完成"), TaskContinuationOptions.ExecuteSynchronously);
            task.Start();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = @"E:\Data\Typhoon\";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxFolderIn.Text = fbd.SelectedPath;
            }
        }
    }
}
