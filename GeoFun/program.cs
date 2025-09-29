using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GeoFun
{
    public class program
    {
        //[DllImport("GeoFunC.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int Sum(int a, int b);

        public static void Main()
        {
            decimal b = 31.6518531252m * Angle.D2RM;
            decimal l = 106.5479580455m * Angle.D2RM;
            double h = 133.0075659;
            Trans trans = new Trans();
            double xx, yy, zz;
            Coordinate.BLH2XYZ((double)b, (double)l, h, out xx, out yy, out zz,Ellipsoid.ELLIP_CGCS2000);
            string pathName = "F:\\Data\\whu\\test\\points2.txt";
            FileInfo fileInfo = new FileInfo(pathName);
            string content = fileInfo.OpenText().ReadToEnd();
            string[] lines = content.Split('\n');
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for (int i = 1;i<lines.Length; i++)
            {
                var segs = lines[i].Split(',');
                if (segs.Length < 3) continue;
                x.Add(double.Parse(segs[1]));
                y.Add(double.Parse(segs[2]));
            }

            Projection pj = new Projection();
            pj.L0 = 115*(double)Angle.D2RM;
            pj.Proj(ref x, ref y);

            Application.EnableVisualStyles();
            Application.Run(new FrmMain());
            ////int c = Sum(100, 200);

            //double X, Y, Z;
            //double b = 35d;
            //double l = 112d;
            //double h = 1000d;

            //Coordinate.BLH2XYZ(b*Angle.D2R, l*Angle.D2R, h, out X, out Y, out Z,Ellipsoid.ELLIP_XIAN80);
            //Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5}",b,l,h,X,Y,Z));
            //Coordinate.XYZ2BLH(X,Y,Z,out b, out l, out h,Ellipsoid.ELLIP_XIAN80);
            //b *= Angle.R2D;
            //l *= Angle.R2D;
            //Console.WriteLine(string.Format("{0} {1} {2} {3} {4} {5}",b,l,h,X,Y,Z));

            //double dB, dL, dH;

            //SevenPara para = new SevenPara();
            //para.XRot = -1;
            //para.YRot = -1;
            //para.ZRot = -1;
            //para.M = 1;
            //para.XOff = 100;
            //para.YOff = 100;
            //para.ZOff = 100;

            //Trans trans = new Trans();

            //b = 35;
            //l = 112;
            //h = 1000;
            //trans.Seven3d(out dB, out dL, out dH, b * Angle.D2R, l * Angle.D2R, h, para, Ellipsoid.ELLIP_XIAN80, Ellipsoid.ELLIP_CGCS2000);
            ////double b1 = Angle.Arc2DMS(b * Angle.D2R + dB);
            ////double l1 = Angle.Arc2DMS(l * Angle.D2R + dL);
            //double b1 = (b * Angle.D2R + dB)*Angle.R2D;
            //double l1 = (l * Angle.D2R + dL) * Angle.R2D;
            //double h1 = h + dH;
            //Console.WriteLine("三维七参数结果 {0} {1} {2}", b1, l1, h1);

            //trans.Seven2d(out dB, out dL, b * Angle.D2R, l * Angle.D2R, para, Ellipsoid.ELLIP_XIAN80, Ellipsoid.ELLIP_CGCS2000);
            //double b2 = (b * Angle.D2R + dB)*Angle.R2D;
            //double l2 = (l * Angle.D2R + dL)*Angle.R2D;
            //Console.WriteLine("三维七参数结果 {0} {1} {2}", b2, l2, h);

            //Console.ReadKey();
        }
    }
}