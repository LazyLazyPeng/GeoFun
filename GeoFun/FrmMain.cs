using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoFun
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void 参数计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileInfo info = new FileInfo(@"F:\Data\chengdu\bl.csv");
            List<string> lines = new List<string>();
            using (var fs = new FileStream(info.FullName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                    reader.Close();
                    fs.Close();
                }
            }

            List<string[]> fields = new List<string[]>();
            foreach(var line in lines)
            {
                fields.Add(StringHelper.SplitFields(line, ','));
            }

            double l0 = 104 + 04 / 60d + 8.87758 / 3600d;
            Projection pj = new Projection();

            double x = 0, y = 0;
            foreach (var field in fields)
            {
                double b = Angle.DD2Arc(Convert.ToDouble(field[3]));
                double l = Angle.DD2Arc(Convert.ToDouble(field[2]));
                pj.Proj(b, l, ref x, ref y);
                Console.WriteLine("{0},{1},{2}", field[0], x.ToString("F3"), y.ToString("F3"));
            }
        }
    }
}
