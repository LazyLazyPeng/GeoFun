using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoFun;
using GeoFun.GNSS;
using GeoFun.GNSS.Net;
using GeoFun.MathUtils;

namespace GIon
{
    public class IonoHelper
    {
        /// <summary>
        /// 计算对应o文件的P4，穿刺点
        /// </summary>
        /// <param name="oPath"></param>
        public static void Calculate(string oPath)
        {
            FileInfo info = new FileInfo(oPath);
            if (info.Name.Length != 12)
            {
                throw new ArgumentException("文件名称正确，请检查:" + info.FullName);
                return;
            }

            // 分解出测站名，doy和年份
            string stationName;
            int doy, year, week, dow;
            FileName.ParseRinex2(info.Name, out stationName, out doy, out year);


            Time.DOY2GPS(year, doy, out week, out dow);

            // 检查是否存在sp3文件
            string sp3Path = Path.Combine(info.DirectoryName, string.Format("igs{0}{1}.sp3", week, dow));
            if (!File.Exists(sp3Path))
            {
                if (!Downloader.DownloadSp3(week, dow, info.DirectoryName))
                {
                    throw new Exception("下载sp3文件失败...");
                }
            }

            // 读取文件
            OFile oFile = new OFile(oPath);
            SP3File sp3 = new SP3File(sp3Path);
            oFile.TryRead();
            sp3.TryRead();

            //
            Observation.CalP4(ref oFile.AllEpoch);
            Observation.CalL4(ref oFile.AllEpoch);
            var arcs = oFile.SearchAllArcs();
            foreach (var prn in arcs.Keys)
            {
                for (int i = 0; i < arcs[prn].Count; i++)
                {
                    OArc arc = arcs[prn][i];
                    Smoother.SmoothP4ByL4(ref arc);
                }
            }

            double[,] ippb = new double[2880, 32];
            double[,] ippl = new double[2880, 32];
            double[,] sp4 = new double[2880, 32];

            if (oFile.Header.approxPos.X == 0d) throw new Exception("无法获得近似坐标");

            double recX = oFile.Header.approxPos.X;
            double recY = oFile.Header.approxPos.Y;
            double recZ = oFile.Header.approxPos.Z;
            double[] recp = { recX, recY, recZ };
            double b, l, h;
            Coordinate.XYZ2BLH(recX, recY, recZ, out b, out l, out h, Ellipsoid.ELLIP_WGS84);
            for (int p = 0; p < arcs.Keys.Count; p++)
            {
                string prn = arcs.Keys.ElementAt(p);
                for (int i = 0; i < arcs[prn].Count; i++)
                {
                    OArc arc = arcs[prn][i];
                    for (int j = 0; j < arc.Length; j++)
                    {
                        GPST t0 = new GPST(arc[j].Epoch);
                        t0.AddSeconds(-arc[j]["P2"] / GeoFun.GNSS.Common.C0);

                        double[] satp = sp3.GetSatPos(t0, prn);
                        arc[j].SatCoor = new GeoFun.GNSS.Coor3
                        {
                            X = satp[0],
                            Y = satp[1],
                            Z = satp[2]
                        };

                        double az, el;
                        MathHelper.CalAzEl(recp, satp, out az, out el);
                        double bb, ll;
                        MathHelper.CalIPP(b, l, 63781000, 450000, az, el, out bb, out ll);
                        ippb[arc.StartIndex + j, p] = bb;
                        ippl[arc.StartIndex + j, p] = ll;

                        sp4[arc.StartIndex + j, p] = arc[j]["P4"];
                    }
                }
            }

            Write(oPath + ".ipp.b.txt", ippb);
            Write(oPath + "ipp.l.txt", ippl);
            Write(oPath + "meas.p4.txt", sp4);
        }

        public static void Write(string path, double[,] data)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    for (int i = 0; i < 2880; i++)
                    {
                        writer.Write("{0,20:f10}", data[i, 0]);
                        for (int j = 1; j < 32; j++)
                        {
                            writer.Write(",{0,30:f10}", data[i, j]);
                        }
                        writer.Write("\r\n");
                    }
                    writer.Close();
                    fs.Close();
                }
            }
        }
    }
}
