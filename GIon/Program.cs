using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoFun;

namespace GIon
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //double b, l, h;
            //Coordinate.XYZ2BLH(new double[] { -3530185.5598, 4118797.3133, 3344036.9115 },out b,out l,out h,Ellipsoid.ELLIP_CGCS2000);
            //Case case1 = new Case(@"E:\Data\Typhoon\feiyan\gnss");
            //case1.SearchObsFiles();
            //case1.GetStationDOY();
            //case1.Download();
            //case1.ReadFiles();

            //IonoHelper.Calculate(@"E:\Data\Typhoon\Case\ramasun\gnss\qion1971.140");
            Application.EnableVisualStyles();
            Application.Run(new FrmSPMModel());
        }
    }
}
