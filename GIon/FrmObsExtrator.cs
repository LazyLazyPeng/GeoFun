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
    public partial class FrmObsExtrator : Form
    {
        public FrmObsExtrator()
        {
            InitializeComponent();
        }

        private void btnObsAll_Click(object sender, EventArgs e)
        {
            if (chkBoxObsTypes.Items.Count <= 0) return;
            for (int i = 0; i < chkBoxObsTypes.Items.Count; i++)
            {
                if (!chkBoxObsTypes.GetItemChecked(i))
                {
                    chkBoxObsTypes.SetItemChecked(i, true);
                }
            }
        }

        private void btnObsOthers_Click(object sender, EventArgs e)
        {
            if (chkBoxObsTypes.Items.Count <= 0) return;
            bool flag;
            for (int i = 0; i < chkBoxObsTypes.Items.Count; i++)
            {
                flag = chkBoxObsTypes.GetItemChecked(i);
                chkBoxObsTypes.SetItemChecked(i, !flag);
            }

        }

        private void btnObsNone_Click(object sender, EventArgs e)
        {
            if (chkBoxObsTypes.Items.Count <= 0) return;
            for (int i = 0; i < chkBoxObsTypes.Items.Count; i++)
            {
                if (!chkBoxObsTypes.GetItemChecked(i))
                {
                    chkBoxObsTypes.SetItemChecked(i, true);
                }
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
