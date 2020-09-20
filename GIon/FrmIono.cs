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
            MessHelper = messHelper;

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
            if (rbDifference.Checked)
            {
                fitType = enumFitType.DoubleDifference;
            }
            else if (rbSmooth.Checked)
            {
                fitType = enumFitType.Smooth;
            }

            //DirectoryInfo root = new DirectoryInfo(@"E:\Data\Graduation\chapter3\igs");
            //var dirs = root.GetDirectories();
            DirectoryInfo dir = new DirectoryInfo(tbxFolderIn.Text);

            Task task = new Task(() =>
            {
                string folder = tbxFolderIn.Text;
                Case ionoCase = new Case(dir.FullName);
                ionoCase.FitType = fitType;
                MessHelper.Print("正在计算台风:" + dir.Name);

                switch (solType)
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
            if (Directory.Exists(tbxFolderIn.Text))
            {
                fbd.SelectedPath = tbxFolderIn.Text;
            }
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                tbxFolderIn.Text = fbd.SelectedPath;
            }
        }

        private void FrmIono_Load(object sender, EventArgs e)
        {

        }
    }
}
