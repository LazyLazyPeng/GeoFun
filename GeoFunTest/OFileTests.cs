using Microsoft.VisualStudio.TestTools.UnitTesting;
using GeoFun.GNSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeoFun.GNSS.Tests
{
    [TestClass()]
    public class OFileTests
    {
        [TestMethod()]
        public void SearchAllArcsTest()
        {
            OFile ofile = OFile.Read(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "rinex", "30900700.11o"));
            Assert.IsNotNull(ofile, "读取文件失败!");
            var arcs = ofile.SearchAllArcs();

            foreach(var key in arcs.Keys)
            {
                for(int i = 0; i < arcs[key].Count; i++)
                {
                    Console.WriteLine("{0} 第{1}段 {2,6} {3,6}",key,i+1,arcs[key][i].StartIndex,arcs[key][i].EndIndex);
                }
                Console.WriteLine("");
            }
        }
    }
}