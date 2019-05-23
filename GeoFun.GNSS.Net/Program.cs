﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codeaddicts.libArgument;

namespace GeoFun.GNSS.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = ArgumentParser<Options>.Parse(args);
            Console.WriteLine(options.list);
            Console.WriteLine(options.output);
            //Downloader.DownloadSp3(1748, 5,center:"IGS");
            //Downloader.DownloadClk(1748, 5,center:"IGS");
            //Downloader.DownloadN(2013, 193);
            //Downloader.DownloadDCB(2013, 9,"P1P2");
        }
    }
}