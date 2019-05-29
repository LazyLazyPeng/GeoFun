using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoFun;
using FluentFTP;
using GeoFun.Sys;

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
            string name = UrlHelper.GetFileName(remoteFullPath);//string.Format("{0}{1}{2}.sp3.Z", center.ToLower(), week, day);
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "weekly", week.ToString(), name);

            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
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
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH = new CMDHelper();
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new Exception("无法复制到路径:" + localPathZ, ex);
            }
            if (!File.Exists(localPathZ))
            {
                throw new Exception("无法复制到路径:" + localPathZ + "!原因未知");
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }
            return true;
        }

        public static bool DownloadClk(int week, int day, string outPath = "temp", string center = "IGS")
        {
            string productName = center + "_CLK";

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
            string name = string.Format("{0}{1}{2}.clk.Z", center.ToLower(), week, day);
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
            string localTempPath = Path.Combine(Common.TEMP_DIR, "weekly", week.ToString(), name);

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
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new Exception("无法复制到路径:" + localPathZ, ex);
            }
            if (!File.Exists(localPathZ))
            {
                throw new Exception("无法复制到路径:" + localPathZ + "!原因未知");
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }

            return true;
        }

        public static bool DownloadN(int year, int doy, string outPath = "temp", string center = "IGS")
        {
            int year2 = 0;
            int year4 = 0;

            if (year < 50)
            {
                year2 = year;
                year4 = year + 2000;
            }
            else if (year >= 50 && year < 100)
            {
                year2 = year;
                year4 = year + 1900;
            }
            else
            {
                year4 = year;
                if (year4 < 2000) year2 = year4 - 1900;
                else year2 = year4 - 2000;
            }

            string productName = center + "_NAV";

            // ftp全路径
            string remoteFullPath;
            if (!Common.URL.TryGetValue(productName, out remoteFullPath)) return false;
            remoteFullPath = remoteFullPath.Replace("%Y", "{0}").Replace("%n", "{1}").Replace("%y", "{2:D2}");
            remoteFullPath = string.Format(remoteFullPath, year4, doy, year2);

            // ftp主机名和相对路径
            string host = UrlHelper.GetHost(remoteFullPath);
            string remoteRelPath = UrlHelper.GetRelPath(remoteFullPath);
            if (string.IsNullOrWhiteSpace(remoteRelPath)) return false;

            /// 文件名
            string name = UrlHelper.GetFileName(remoteFullPath);//string.Format("brdc{0}0.{1}n.Z", doy, year2);

            // 本地路径,压缩后
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            // 本地路径,解压后
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "daily", year4.ToString(), doy.ToString(), year2.ToString("D2") + "n", name);

            // 下载文件到临时目录
            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
            if (!File.Exists(localTempPath))
            {
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new IOException("无法复制到路径", ex);
            }
            if (!File.Exists(localPathZ))
            {
                throw new IOException("无法复制到路径,原因未知");
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }
            return true;
        }

        public static bool DownloadO(int year,int doy,string station, string outPath = "temp",string center="IGS")
        {
            int year2 = 0;
            int year4 = 0;

            if (year < 50)
            {
                year2 = year;
                year4 = year + 2000;
            }
            else if (year >= 50 && year < 100)
            {
                year2 = year;
                year4 = year + 1900;
            }
            else
            {
                year4 = year;
                if (year4 < 2000) year2 = year4 - 1900;
                else year2 = year4 - 2000;
            }

            string productName = center + "_OBS";

            // ftp全路径
            string remoteFullPath;
            if (!Common.URL.TryGetValue(productName, out remoteFullPath)) return false;
            remoteFullPath = remoteFullPath.Replace("%Y", "{0}").Replace("%n", "{1}").Replace("%y", "{2:D2}").Replace("%s",station);
            remoteFullPath = string.Format(remoteFullPath, year4, doy, year2);

            // ftp主机名和相对路径
            string host = UrlHelper.GetHost(remoteFullPath);
            string remoteRelPath = UrlHelper.GetRelPath(remoteFullPath);
            if (string.IsNullOrWhiteSpace(remoteRelPath)) return false;

            /// 文件名
            string name = UrlHelper.GetFileName(remoteFullPath);//string.Format("brdc{0}0.{1}n.Z", doy, year2);

            // 本地路径,压缩后
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            // 本地路径,解压后
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "daily", year4.ToString(), doy.ToString(), year2.ToString("D2") + "n", name);

            // 下载文件到临时目录
            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
            if (!File.Exists(localTempPath))
            {
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new IOException("无法复制到路径", ex);
            }
            if (!File.Exists(localPathZ))
            {
                throw new IOException("无法复制到路径,原因未知");
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            cmd = string.Format("\"{0}crx2rnx.exe\" \"{1}\" &exit",AppDomain.CurrentDomain.BaseDirectory,localPath);
            cmdH.Execute(cmd);

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }
            return true;
        }

        public static bool DownloadDCB(int year, int month, string code = "P1C1", string outPath = "temp")
        {
            int year2 = 0;
            int year4 = 0;

            if (year < 50)
            {
                year2 = year;
                year4 = year + 2000;
            }
            else if (year >= 50 && year < 100)
            {
                year2 = year;
                year4 = year + 1900;
            }
            else
            {
                year4 = year;
                if (year4 < 2000) year2 = year4 - 1900;
                else year2 = year4 - 2000;
            }

            string productName = "COD_DCB" + "_" + code.ToUpper();

            // ftp全路径
            string remoteFullPath;
            if (!Common.URL.TryGetValue(productName, out remoteFullPath)) return false;
            remoteFullPath = remoteFullPath.Replace("%Y", "{0}").Replace("%m", "{1,2:00}").Replace("%y", "{2,2:00}");
            remoteFullPath = string.Format(remoteFullPath, year4, month, year2);

            // ftp主机名和相对路径
            string host = UrlHelper.GetHost(remoteFullPath);
            string remoteRelPath = UrlHelper.GetRelPath(remoteFullPath);
            if (string.IsNullOrWhiteSpace(remoteRelPath)) return false;

            /// 本地路径
            string name = string.Format("{0}{1,2:00}{2,2:00}.DCB.Z", code.ToUpper(), year2, month);
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "dcb", name);

            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
            if (!File.Exists(localTempPath))
            {
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new IOException("无法复制到路径", ex);
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }
            return true;
        }

        public static bool DownloadI(int year, int doy, string outPath = "temp", string center = "IGS")
        {
            int year2 = 0;
            int year4 = 0;

            if (year < 50)
            {
                year2 = year;
                year4 = year + 2000;
            }
            else if (year >= 50 && year < 100)
            {
                year2 = year;
                year4 = year + 1900;
            }
            else
            {
                year4 = year;
                if (year4 < 2000) year2 = year4 - 1900;
                else year2 = year4 - 2000;
            }

            string productName = center + "_TEC";

            // ftp全路径
            string remoteFullPath;
            if (!Common.URL.TryGetValue(productName, out remoteFullPath)) return false;
            remoteFullPath = remoteFullPath.Replace("%Y", "{0}").Replace("%n", "{1}").Replace("%y", "{2:D2}");
            remoteFullPath = string.Format(remoteFullPath, year4, doy, year2);

            // ftp主机名和相对路径
            string host = UrlHelper.GetHost(remoteFullPath);
            string remoteRelPath = UrlHelper.GetRelPath(remoteFullPath);
            if (string.IsNullOrWhiteSpace(remoteRelPath)) return false;

            /// 文件名
            string name = UrlHelper.GetFileName(remoteFullPath);//string.Format("brdc{0}0.{1}n.Z", doy, year2);

            // 本地路径,压缩后
            string localPathZ = Path.Combine(Path.GetFullPath(outPath), name);
            // 本地路径,解压后
            string localPath = localPathZ.Substring(0, localPathZ.Length - 2);

            // 本地缓存路径
            string localTempPath = Path.Combine(Common.TEMP_DIR, "daily", year4.ToString(), doy.ToString(), year2.ToString("D2") + "n", name);

            // 下载文件到临时目录
            string cmd = "";
            CMDHelper cmdH = new CMDHelper();
            if (!File.Exists(localTempPath))
            {
                cmd = string.Format("\"{0}\\wget.exe\" -P\"{1}\" \"{2}\" --ftp-user={3} --ftp-password={4} &exit", AppDomain.CurrentDomain.BaseDirectory, Path.GetDirectoryName(localTempPath), remoteFullPath, ANONY_USER, ANONY_PSSD); ;
                cmdH.Execute(cmd);
            }
            if (!File.Exists(localTempPath)) return false;

            try
            {
                File.Copy(localTempPath, localPathZ, true);
            }
            catch (Exception ex)
            {
                throw new IOException("无法复制到路径", ex);
            }
            if (!File.Exists(localPathZ))
            {
                throw new IOException("无法复制到路径,原因未知");
            }

            // 解压文件
            cmd = string.Format("\"{0}7z.exe\" x \"{1}\" -y -o\"{2}\" &exit", AppDomain.CurrentDomain.BaseDirectory, localPathZ, Path.GetDirectoryName(localPathZ));
            cmdH.Execute(cmd);

            if (!File.Exists(localPath))
            {
                throw new Exception("解压失败,路径为:" + localPathZ);
            }

            try
            {
                File.Delete(localPathZ);
            }
            catch
            { }
            return true;
        }
    }
}
