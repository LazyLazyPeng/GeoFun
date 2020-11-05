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
using GeoFun.GNSS.Net;
using GeoFun;

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
            GeoFun.GNSS.Common.msgBox = messHelper;
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

            enumFitType fitType = enumFitType.None;
            if(rbOriginalMeas.Checked)
            {
                fitType = enumFitType.None;
            }
            if (rbDifference.Checked)
            {
                fitType = enumFitType.DoubleDifference;
            }
            else if (rbSmooth.Checked)
            {
                fitType = enumFitType.Smooth;
            }
            else if(rbROTI.Checked)
            {
                fitType = enumFitType.ROTI;
            }

            GeoFun.GNSS.Options.CutOffAngle = (double)tbxCutOffAngle.Value*Angle.D2R;
            GeoFun.GNSS.Options.ARC_MIN_LENGTH = (int)tbxMinArcLen.Value;
            //GeoFun.GNSS.Net.Common.TEMP_DIR = Application.StartupPath + "\\temp";

            DirectoryInfo dir = new DirectoryInfo(tbxFolderIn.Text);
            Task task = new Task(() =>
            {
                string folder = tbxFolderIn.Text;
                Case ionoCase = new Case(dir.FullName);
                ionoCase.FitType = fitType;
                ionoCase.SetProgressMax = SetProgressMax;
                ionoCase.SetProgressValue = SetProgressValue;
                MessHelper.Print("正在计算:" + dir.Name);

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
            fbd.SelectedPath = @"E:\Data\Graduation\chapter3\igs\";
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
            //GeoFun.GNSS.Net.Common.TEMP_DIR = Application.StartupPath + "\\temp";
        }

        public void SetProgressMax(int max)
        {
            Invoke(new Action(() =>
            {
                progressBar.Maximum = max;
                statusLabel.Text = string.Format("{0}/{1}",progressBar.Value,progressBar.Maximum);
            }));
        }

        public void SetProgressValue(int value)
        {
            Invoke(new Action(() =>
            {
                progressBar.Value = value;

                statusLabel.Text = string.Format("{0}/{1}",progressBar.Value,progressBar.Maximum);
            }));
        }
    }
}
