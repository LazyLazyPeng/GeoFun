using System;
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
            //Downloader.DownloadI(2013, 193, @"E:\Data\Typhoon\Case\201307_Soulik");
            //Downloader.DownloadSp3(1748, 4, @"E:\Data\Typhoon\Case\201307_Soulik", center: "IGS");
            //Downloader.DownloadSp3(1748, 5, @"E:\Data\Typhoon\Case\201307_Soulik", center: "IGS");
            //Downloader.DownloadSp3(1748, 6, @"E:\Data\Typhoon\Case\201307_Soulik", center: "IGS");
            //Downloader.DownloadClk(1748, 5,center:"IGS");
            //Downloader.DownloadN(2013, 193);
            Downloader.DownloadDCB(2013, 9,"P1P2",@"E:\Data\Typhoon\Case\201307_Soulik");
            //Downloader.DownloadDCB(2013, 9,"P1C1",@"E:\Data\Typhoon\Case\201307_Soulik");
        }
    }
}
