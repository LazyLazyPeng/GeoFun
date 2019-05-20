using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            Downloader.DownloadSp3(1748, 5);
        }
    }
}
