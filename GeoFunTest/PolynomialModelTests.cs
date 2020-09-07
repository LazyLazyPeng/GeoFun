using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.MathUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.MathUtils.Tests
{
    [TestClass()]
    public class PolynomialModelTests
    {
        [TestMethod()]
        public void FitTest()
        {
            Random rand = new Random();
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            for(int i =0; i < 10; i++)
            {
                x.Add(i);
                y.Add(3d + 5d * i + 7d * i * i + 9d * i * i * i+rand.NextDouble()*1);
            }

            PolynomialModel pm = new PolynomialModel();
            pm.Order = 3;
            pm.Fit(x, y);
            for(int i =0; i <pm.Factor.Count;i++)
            {
                Console.WriteLine(pm.Factor[i]);
            }

            for(int i =0; i < 10; i++)
            {
                Console.WriteLine(pm.CalFit(i));
            }
        }
    }
}