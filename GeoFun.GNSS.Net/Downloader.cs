using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;
using FluentFTP;

namespace GeoFun.GNSS.Net
{
    public class Downloader
    {
        public static string ANONY_USER = "anonymous";
        public static string ANONY_PSSD = "landwill@163.com";

        public static bool DownloadSp3(int week, int day, string outPath = "temp", string center = "IGS")
        {
            string productName = center + "_EPH";

            // ftp全路径
            string remoteFullPath;
            if (!Common.URL.TryGetValue(productName, out remoteFullPath)) return false;
            remoteFullPath = remoteFullPath.Replace("%W", "{0}").Replace("%D", "{1}");
            remoteFullPath = string.Format(remoteFullPath, week, day);

            // ftp主机名和相对路径
            string host = UrlHelper.GetHost(remoteFullPath);
            string remoteRelPath = UrlHelper.GetRelPath(remoteFullPath);
            if (string.IsNullOrWhiteSpace(remoteRelPath)) return false;

            /// 本地路径
            string name = string.Format("{0}{1}{2}.sp3.Z", center.ToLower(), week, day);
            string localPath = Path.Combine(Path.GetFullPath(outPath), name);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "orbit", "sp3", name);

            if (!File.Exists(localTempPath))
            {
                //FtpClient client = new FtpClient(host, ANONY_USER, ANONY_PSSD);
                //client.Connect();

                //try
                //{
                //    client.DownloadFile(localTempPath, remoteRelPath);
                //}
                //catch
                //{
                //    return false;
                //}
                string cmd = string.Format("{0}\\wget.exe -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4}", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPath, true);
            }
            catch (Exception ex)
            {
                return false;
            }

            if (!File.Exists(localPath)) return false;
            return true;
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
