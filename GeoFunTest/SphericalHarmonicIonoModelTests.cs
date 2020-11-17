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
    public class SphericalHarmonicIonoModelTests
    {
        [TestMethod()]
        public void LoadTest()
        {
            SphericalHarmonicIonoModel.Load(AppDomain.CurrentDomain.BaseDirectory+"\\Data\\spm\\2016190_0000.spm.txt");
        }
    }
}