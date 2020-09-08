using Microsoft.VisualStudio.TestTools.UnitTesting;
using GIon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GIon.Tests
{
    [TestClass()]
    public class CaseTests
    {
        [TestMethod()]
        public void SearchObsFilesTest()
        {
            Case case1 = new Case(@"C:\Users\Administrator\Desktop\新建文件夹");
            //Assert.IsTrue(case1.SearchObsFiles() == 14);
            foreach (var file in case1.obsFiles)
            {
                Console.WriteLine(file);
            }
        }

        [TestMethod()]
        public void GetStationDOYTest()
        {
            Case case1 = new Case();
            case1.Path = @"C:\Users\Administrator\Desktop\新建文件夹\obs";
            //case1.SearchObsFiles();
            //case1.GetStationDOY();
            //foreach (var item in case1.DOYs)
            //{
            //    Console.Write(item.Key + ":");
            //    foreach (var doy in item.Value)
            //    {
            //        Console.Write(doy.Day.ToString() + " ");
            //    }
            //    Console.Write("\n");
            //}
        }

        [TestMethod()]
        public void CheckDOYTest()
        {
            Case case1 = new Case(@"C:\Users\Administrator\Desktop\新建文件夹");
            //case1.SearchObsFiles();
            //case1.GetStationDOY();
            //Assert.IsFalse(case1.CheckDOY());
        }

        [TestMethod()]
        public void DownloadTest()
        {
            Case case1 = new Case(@"C:\Users\Administrator\Desktop\新建文件夹");
            //case1.SearchObsFiles();
            //case1.GetStationDOY();
            //case1.Download();
        }

        [TestMethod()]
        public void ReadFilesTest()
        {
            Case case1 = new Case(@"C:\Users\Administrator\Desktop\新建文件夹");
        }
    }
}