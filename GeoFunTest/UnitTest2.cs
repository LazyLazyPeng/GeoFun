using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;

using GeoFun;
using GeoFun.GNSS;
using GeoFun.Spatial;
using System.IO;

namespace GeoFunTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            CommonT dt1, dt2;
            Week week1, week2;

            dt1 = new CommonT(2002, 1, 1, 3, 59, 60 - 1e-11);
            week1 = Time.CommonToGPS(dt1);

            dt2 = Time.GPSToCommon(week1);

            Assert.IsTrue(dt1 == dt2, "dt1不等于dt2");
        }

        [TestMethod]
        public void TestMethod2()
        {
            GPST time = new GPST(2019, 365);
            Assert.IsTrue(time.Week.Weeks ==2086);
            Assert.IsTrue(time.Week.DayOfWeek ==2);
        }

        [TestMethod]
        public void TestSP3File()
        {
            SP3File sp3 = new SP3File(@"E:\Data\Typhoon\201307_Soulik\cod17484.sp3");
            sp3.TryRead();
        }

        [TestMethod]
        public void TestOFile()
        {
            OrderedDictionary od = new OrderedDictionary();
            OFile ofile = new OFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\fjpt1930.13o");
            //ofile.TryRead();
            Console.WriteLine(".........");
        }

        [TestMethod]
        public void TestDCBFile()
        {
            //DCBFile dcbF = new DCBFile(@"E:\Data\Typhoon\201307_Soulik\P1C11307.DCB");
        }
        [TestMethod]
        public void TestSHPFile_Point()
        {
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\point.shp", @"C: \Users\niuni\Desktop\新建文件夹\point_after.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");

                    double x = 0d, y = 0d;
                    for(int i =0; i < records.Count; i++)
                    {
                        if (records[i].Feature is null) continue;

                        for(int j = 0; j < records[i].Feature.NumPoints;j++)
                        {
                            try
                            {
                                records[i].Feature.GetPointAt(j, out x, out y);
                                x += 500;
                                y += 500;
                                records[i].Feature.SetPointAt(j, x, y);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PointM()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\pointm.shp", @"C: \Users\niuni\Desktop\新建文件夹\pointm_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PointZ()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            //SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory+"\\Data\\SHP\\pointzm.shp", @"C: \Users\niuni\Desktop\新建文件夹\pointzm_after.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\pointzm.shp", @"C: \Users\niuni\Desktop\新建文件夹\pointzm_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_MPoint()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\mpoint.shp", @"C: \Users\niuni\Desktop\新建文件夹\mpoint_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_MPointM()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\mpointm.shp", @"C: \Users\niuni\Desktop\新建文件夹\mpointm_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_MPointZ()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            //SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory+"\\Data\\SHP\\pointzm.shp", @"C: \Users\niuni\Desktop\新建文件夹\pointzm_after.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\mpointz.shp", @"C: \Users\niuni\Desktop\新建文件夹\mpointz_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_Polyline()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polyline.shp", @"C: \Users\niuni\Desktop\新建文件夹\polyline_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PolyLineM()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polylinem.shp", @"C: \Users\niuni\Desktop\新建文件夹\polylinem_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PolyLineZ()
        {
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polylinez.shp", @"C: \Users\niuni\Desktop\新建文件夹\polylinez_after.shp");

            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");

                    double x = 0d, y = 0d;
                    for (int i = 0; i < records.Count; i++)
                    {
                        if (records[i].Feature is null) continue;

                        for (int j = 0; j < records[i].Feature.NumPoints; j++)
                        {
                            try
                            {
                                records[i].Feature.GetPointAt(j, out x, out y);
                                x += 500;
                                y += 500;
                                records[i].Feature.SetPointAt(j, x, y);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();
        }

        [TestMethod]
        public void TestSHPFile_Polygon()
        {
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polygon.shp", @"C: \Users\niuni\Desktop\新建文件夹\polygon_after.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");

                    double x = 0d, y = 0d;
                    for (int i = 0; i < records.Count; i++)
                    {
                        if (records[i].Feature is null) continue;

                        for (int j = 0; j < records[i].Feature.NumPoints; j++)
                        {
                            try
                            {
                                records[i].Feature.GetPointAt(j, out x, out y);
                                x += 500;
                                y += 500;
                                records[i].Feature.SetPointAt(j, x, y);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PolygonM()
        {
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\2000_pt.shp");
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polygonm.shp", @"C: \Users\niuni\Desktop\新建文件夹\polygonm_after.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_line.shp");
            //SHPFile shpF = new SHPFile(@"E:\Data\ShenZhen\Z坐标转换\54shp\grid_54_xy114_point.shp");
            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();

            //Console.ReadKey();
        }

        [TestMethod]
        public void TestSHPFile_PolygonZ()
        {
            SHPFile shpF = new SHPFile(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\SHP\\polygonz.shp", @"C: \Users\niuni\Desktop\新建文件夹\polygonz_after.shp");

            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");

                    double x = 0d, y = 0d;
                    for (int i = 0; i < records.Count; i++)
                    {
                        if (records[i].Feature is null) continue;

                        for (int j = 0; j < records[i].Feature.NumPoints; j++)
                        {
                            try
                            {
                                records[i].Feature.GetPointAt(j, out x, out y);
                                x += 500;
                                y += 500;
                                records[i].Feature.SetPointAt(j, x, y);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();
        }

        [TestMethod]
        public void TestSHPFile_BigData()
        {
            SHPFile shpF = new SHPFile(@"E:\Data\Shanxi\T图件转换\大数据\test\test.shp", @"C: \Users\niuni\Desktop\新建文件夹\big.shp");

            shpF.OpenRead();
            shpF.OpenWrite();
            if (shpF.ReadHeader())
            {
                shpF.WriteHeader();
                List<SHPRecord> records = new List<SHPRecord>();
                uint readNum = 10000;
                int writeNum = 0;
                do
                {
                    records = shpF.ReadRecords(readNum);
                    Console.WriteLine("读取1000条数据...");
                    writeNum = shpF.WriteRecords(records);
                    Console.WriteLine(string.Format("写入{0}条数据...", writeNum));
                } while (records.Count == 10000);
            }
            shpF.CloseRead();
            shpF.CloseWrite();
        }
    }
}
