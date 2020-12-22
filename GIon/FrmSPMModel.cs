using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevComponents.Editors.DateTimeAdv;
using GeoFun;
using GeoFun.GNSS;
using GeoFun.MathUtils;
using GeoFun.Spatial;

namespace GIon
{
    public partial class FrmSPMModel : Form
    {
        public FrmSPMModel()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbxOpen.Text = ofd.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                tbxSave.Text = sfd.FileName;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            double latMin = double.Parse(tbxLatMin.Text);
            double latMax = double.Parse(tbxLatMax.Text);
            double lonMin = double.Parse(tbxLonMin.Text);
            double lonMax = double.Parse(tbxLonMax.Text);
            double res = double.Parse(tbxResolution.Text);

            int rowNum = (int)((latMax - latMin + 0.5) / res);
            int colNum = (int)((lonMax - lonMin + 0.5) / res);

            ESRIASC ascf = new ESRIASC(rowNum,colNum);
            ascf.XLLCenter = lonMin;
            ascf.YLLCenter = latMin;
            ascf.CellSize = double.Parse(tbxResolution.Text);

            SphericalHarmonicIonoModel spm = SphericalHarmonicIonoModel.Load(tbxOpen.Text);

            int hour = 12;
            int minute = 0;
            int second = 0;
            double sgb, sgl;
            double b, l;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    b = latMin-res/2d + i * res;
                    l = lonMin-res/2d + j * res;

                    b *= Angle.D2R;
                    l *= Angle.D2R;

                    Coordinate.SunGeomagnetic(b, l, hour, minute, second,
                        GeoFun.GNSS.Common.GEOMAGNETIC_POLE_LAT, GeoFun.GNSS.Common.GEOMAGENTIC_POLE_LON,
                        out sgb, out sgl);

                    ascf.Data[i, j] = spm.Calculate(sgb, sgl);
                }
            }

            ascf.WriteAs(tbxSave.Text);
        }

        private void FrmSPMModel_Load(object sender, EventArgs e)
        {

        }
    }
}
