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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbxFolderIn.Text))
                MessageBox.Show("文件夹不存在");

            DirectoryInfo dir = new DirectoryInfo(tbxFolderIn.Text);

            Task task = new Task(() =>
            {
                foreach (var file in dir.GetFiles("*?.??o"))
                {
                    tbxMsg.Invoke(new Action(() =>
                    {
                        tbxMsg.Text += string.Format("\r\n {0} 正在解算:{1}", DateTime.Now.ToString(), file.Name);
                        tbxMsg.Select(tbxMsg.Text.Length-1,0);
                        tbxMsg.ScrollToCaret();
                    }));
                    IonoHelper.Calculate(file.FullName);
                    tbxMsg.Invoke(new Action(() =>
                    {
                        tbxMsg.Text += string.Format("\r\n {0} 解算完成:{1}\r\n", DateTime.Now.ToString(), file.Name);
                        tbxMsg.Select(tbxMsg.Text.Length-1,0);
                        tbxMsg.ScrollToCaret();
                    }));
                }
            });
            task.ContinueWith(t=>MessageBox.Show("解算完成"),TaskContinuationOptions.ExecuteSynchronously);
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
