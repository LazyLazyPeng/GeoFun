using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentFTP;

namespace GeoFun.GNSS.Net
{
    public class Downloader
    {
        public static string ANONY_USER = "anonymous";
        public static string ANONY_PSSD = "";

        public static bool DownloadSp3(int week,int day,string outPath = "temp",string center="IGS")
        {
            string productName = center + "_EPH";
            string url;
            if (!Common.URL.TryGetValue(productName, out url)) return false;
            url = url.Replace("%W","{0}").Replace("%D","{1}");
            url = string.Format(url, week, day);

            string host = UrlHelper.GetHost(url);

            FtpClient client = new FtpClient(host,ANONY_USER,ANONY_PSSD);

            string localName = string.Format("{0}{1}{2}.sp3.Z",center.ToLower(),week,day);
            string localPath = Path.Combine(Path.GetFullPath(outPath),localName);

            return client.DownloadFile(localPath, url);
        }

        public static void DownloadClk()
        {

        }

        public void DownloadN()
        { }

        public void DonwloadO()
        { }

        public void DownloadP1P2()
        { }

        public void DownloadP1C1()
        {
        }


    }
}
