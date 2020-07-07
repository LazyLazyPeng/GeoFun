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
            //Downloader.DownloadSp3DOY(2015,210, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadSp3DOY(2015,211, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadSp3DOY(2015,212, @"C:\Users\Administrator\Desktop\soudelor211");

            //Downloader.DownloadClkDOY(2015,210, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadClkDOY(2015,211, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadClkDOY(2015,212, @"C:\Users\Administrator\Desktop\soudelor211");

            //Downloader.DownloadN(2015,210, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadN(2015,211, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadN(2015,212, @"C:\Users\Administrator\Desktop\soudelor211");

            //Downloader.DownloadI(2015,210, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadI(2015,211, @"C:\Users\Administrator\Desktop\soudelor211");
            //Downloader.DownloadI(2015,212, @"C:\Users\Administrator\Desktop\soudelor211");


            Downloader.DownloadDCB(2015,7,"P1C1", @"C:\Users\Administrator\Desktop\soudelor211");
            Downloader.DownloadDCB(2015,7,"P1P2", @"C:\Users\Administrator\Desktop\soudelor211");
        }
    }
}
