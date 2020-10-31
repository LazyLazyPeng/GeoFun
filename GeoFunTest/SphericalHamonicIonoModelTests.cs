using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun.IO;

namespace GeoFun.MathUtils.Tests
{
    [TestClass()]
    public class SphericalHamonicIonoModelTests
    {
        [TestMethod()]
        public void CalculateModelTest()
        {
            var lines = FileHelper.ReadThenSplitLine(".\\data\\ionex\\codg0150.16I.txt",',');
            List<double> b = new List<double>(10000);
            List<double> l = new List<double>(10000);
            List<double> tec = new List<double>(10000);

            for(int i =0; i < 71; i++)
            {
                for(int j =0; j < 72; j++)
                {
                    double bb = (87.5 - 2.5 * i) * Angle.D2R;
                    double ll = (-180d + j * 5) * Angle.D2R;
                    double ion = double.Parse(lines[i][j]);
                    b.Add(bb);
                    l.Add(ll);
                    tec.Add(ion);
                }
            }

            SphericalHarmonicIonoModel sm = SphericalHarmonicIonoModel.CalculateModel(15,15,b,l,tec);
            var res = sm.Calculate(87.5 * Angle.D2R,-180d * Angle.D2R);
            Console.WriteLine(res-23);
        }
    }
}