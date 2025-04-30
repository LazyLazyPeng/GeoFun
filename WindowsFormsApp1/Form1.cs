using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoFun;
using GeoFun.IO;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text;
            if(!File.Exists(filePath))
            {
                MessageBox.Show("路径不存在");
                return;
            }

            var lines = FileHelper.ReadThenSplitLine(filePath, ',');
            List<double> x1 = new List<double>();
            List<double> y1 = new List<double>();
            List<double> x2 = new List<double>();
            List<double> y2 = new List<double>();
            double dx, dy, r, s;
            for(int i =0; i < lines.Count;i++)
            {
                if (lines[i].Length < 4) continue;
                if (string.IsNullOrEmpty(lines[i][1])) continue;
                x1.Add(double.Parse(lines[i][1]));
                y1.Add(double.Parse(lines[i][2]));
                x2.Add(double.Parse(lines[i][3]));
                y2.Add(double.Parse(lines[i][4]));
            }

            var para = FourPara.CalPara(x1, y1, x2, y2);
            dx = para.DX;
            dy = para.DY;
            r = para.R;
            s = para.S;

            textBox2.Text += dx.ToString()+",";
            textBox2.Text += dy.ToString()+",";
            textBox2.Text += r.ToString()+",";
            textBox2.Text += s.ToString()+",";
            List<double> x11 = new List<double>();
            List<double> y11 = new List<double>();
            List<double> x21 = new List<double>();
            List<double> y21 = new List<double>();

            double x, y;
            for(int i =0;i<x1.Count;i++)
            {
                x=x1[i] * Math.Cos(r) * (1 + s * 1e-6)+y1[i]*Math.Sin(r)*(1+s*1e-6) + dx;
                x21.Add(x);
                y = -x1[i] * Math.Sin(r) * (1 + s * 1e-6)+y1[i]*Math.Cos(r)*(1+s*1e-6) + dy;
                y21.Add(y);

                lines[i][3] = ((x - x2[i])*1000).ToString();
                lines[i][4] = ((y - y2[i]) * 1000).ToString();
            }

            FileHelper.WriteLines(filePath+".txt", lines, ',');
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
            }
        }
    }
}
