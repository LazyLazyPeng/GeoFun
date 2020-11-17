using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void menuCreateTask_Click(object sender, EventArgs e)
        {
            FrmIono frm = new FrmIono();
            frm.Show();
        }

        private void menuSPM_Click(object sender, EventArgs e)
        {
            FrmSPMModel frmspm = new FrmSPMModel();
            frmspm.Show();
        }
    }
}
