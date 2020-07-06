using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GeoFun.Sys;
using GeoFun.GNSS;
using Codeaddicts.libArgument;

namespace GeoFun.GNSS.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime dt1 = new DateTime(2015, 7, 30);
            DateTime dt2 = new DateTime(1980, 1, 6);
            var ts = dt1 - dt2;

            var len = Encoding.Default.GetBytes("我1");
            var options = ArgumentParser<Options>.Parse(args);
            Console.WriteLine(options.list);
            Console.WriteLine(options.output);
            Downloader.DownloadI(2017, 233, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 234, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 235, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 236, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 237, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 238, @"E:\Data\Typhoon\harvey\GNSS");
            Downloader.DownloadI(2017, 239, @"E:\Data\Typhoon\harvey\GNSS");

            Downloader.DownloadSp3(1963,1, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1963,2, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1963,3, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1963,4, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1963,5, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1963,6, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            Downloader.DownloadSp3(1964,0, @"E:\Data\Typhoon\harvey\GNSS", center: "IGS");
            //Downloader.DownloadSp3(1748, 5, @"E:\Data\Typhoon\Case\201307_Soulik", center: "IGS");
            //Downloader.DownloadSp3(1748, 6, @"E:\Data\Typhoon\Case\201307_Soulik", center: "IGS");
            //Downloader.DownloadClk(1748, 5,center:"IGS");
            //Downloader.DownloadN(2013, 193);
            //Downloader.DownloadO(2011, 100, "ista");
            //Downloader.DownloadDCB(2013, 9,"P1P2",@"E:\Data\Typhoon\Case\201307_Soulik");
            //Downloader.DownloadDCB(2013, 9,"P1C1",@"E:\Data\Typhoon\Case\201307_Soulik");

            DirectoryInfo root = new DirectoryInfo(@"E:\Data\Typhoon\Case");
            foreach (var dir in root.GetDirectories())
            {
                if (dir.Name.EndsWith("Soulik")) continue;

                FileInfo file = dir.GetFiles("*.??o")[0];
                int year = int.Parse(Path.GetExtension(file.FullName).Substring(1, 2)) + 2000;
                int doy = int.Parse(file.Name.Substring(4, 3));
                Downloader down = new Downloader();

                GPST time = new GPST(year, doy - 1);
                //Downloader.DownloadSp3(time.Week.Weeks, time.Week.DayOfWeek,dir.FullName);
                //Downloader.DownloadI(year, doy-1, dir.FullName);

                time = new GPST(year, doy);
                //Downloader.DownloadSp3(time.Week.Weeks, time.Week.DayOfWeek,dir.FullName);
                //Downloader.DownloadI(year, doy, dir.FullName);
                //Downloader.DownloadDCB(year, time.CommonT.Month,"P1P2", dir.FullName);

                //time = new GPST(year, doy+1);
                //Downloader.DownloadSp3(time.Week.Weeks, time.Week.DayOfWeek,dir.FullName);
                //Downloader.DownloadI(year, doy+1, dir.FullName);

                //dir.CreateSubdirectory("Result");
                string controlFile = "" +
                    "#START——开始时间：小时 分钟\n" +
                    "00 00 00\n" +
                    "#STOP ——结束时间：小时 分钟\n" +
                    "23 59 59\n" +
                    "#CUT ANGLE\n" +
                    "15\n" +
                    "#DATA PATH——输入数据路径\n" +
                    "{0}\\\n"+
                    "#RESULT PATH——结果输出路径\n" +
                    "{1}\\\n"+
                    "#TYPE ORDER——建模函数选择和阶数选择，type=1是球谐函数，type=0是多项式；order为阶数\n" +
                    "0 3\n" +
                    "#DCB OPTION——DCB解算方式，1：不解算卫星和接收机DCB，2:解算卫星和接收机DCB，3：只解算接收机DCB， 4：滑动平均去趋势按照历元输出结果\n" +
                    "3\n";

                //File.WriteAllText(Path.Combine(dir.FullName,"1.ctl"),string.Format(controlFile,dir.FullName,Path.Combine(dir.FullName,"Result")));
                Console.Write(dir.FullName+"\\1.ctl ");
            }
        }
    }
}
