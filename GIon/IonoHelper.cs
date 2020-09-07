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

            //Observation.EliminateSatellites(ref oFile.AllEpoch);
            Observation.CalP4(ref oFile.AllEpoch);
            Observation.CalL4(ref oFile.AllEpoch);
            Observation.DetectOutlier(ref oFile.AllEpoch);

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
                    //多项式拟合
                    List<double> x = new List<double>();
                    List<double> y = new List<double>();
                    for (int j = 0; j < arc.Length; j++)
                    {
                        x.Add(j);
                        y.Add(arc[j]["SP4"]);
                    }
                    PolynomialModel pm = new PolynomialModel();
                    pm.Order = 3;
                    pm.Fit(x, y);
                    var yy = pm.CalFit(x);
                    for (int j = 0; j < arc.Length; j++)
                    {
                        arc[j]["SP4"] = y[j] - yy[j];
                    }

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
                        arc[j].IPP[0] = bb;
                        arc[j].IPP[1] = ll;
                        arc[j].Azimuth = az;
                        arc[j].Elevation = el;
                        ippb[arc.StartIndex + j, p] = bb;
                        ippl[arc.StartIndex + j, p] = ll;

                        double tec = 9.52437 * arc[j]["SP4"];
                        if (tec > 0.5d || tec < -0.5d) tec = 0d;
                        sp4[arc.StartIndex + j, p] = tec;
                    }
                }
            }

            for (int i = 0; i < 24; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int j = i * 120; j < i * 120 + 120; j++)
                {
                    var epoch = oFile.AllEpoch[j];
                    foreach (var prn in epoch.AllSat.Keys)
                    {
                        var sat = epoch[prn];
                        if (sat["SP4"] != 0d)
                        {
                            sb.AppendFormat("{0:0#},{1:f10},{2:f10},{3:f3}\r\n",
                                int.Parse(prn.Substring(1)),
                                sat.IPP[1] * Angle.R2D,
                                sat.IPP[0] * Angle.R2D,
                                sat["SP4"]
                                );
                        }
                    }
                }

                File.WriteAllText(string.Format("{0}.{1:0#}-{2:0#}.tec.txt", oPath, i, i + 1), sb.ToString(), Encoding.UTF8);
            }

            //// 2个小时一幅图
            //for (int i = 0; i < 12; i++)
            //{
            //    List<double> lat = new List<double>();
            //    List<double> lon = new List<double>();
            //    List<double> tec = new List<double>();
            //    for (int j = i * 240; j < i * 240 + 240; j++)
            //    {
            //        if (j >= oFile.AllEpoch.Count)
            //        {
            //            break;
            //        }

            //        foreach (var item in oFile.AllEpoch[j].AllSat)
            //        {
            //            var epoch = oFile.AllEpoch[i].Epoch;
            //            var sunL0 = Coordinate.SunLon(epoch.CommonT.Hour,
            //                epoch.CommonT.Minute, (double)epoch.CommonT.second, 8);
            //            var sat = item.Value;
            //            if (sat["SP4"] != 0 && sat["SP4"] > 0)
            //            {
            //                lat.Add(sat.IPP[0]);
            //                lon.Add(sat.IPP[1] - sunL0);

            //                double vtec = Iono.STEC2VTEC(sat["SP4"], sat.Elevation);
            //                tec.Add(vtec);
            //            }
            //        }
            //    }
            //    var shm = SphericalHamonicIonoModel.CalculateModel(5, 5, lat, lon, tec);
            //    for (int j = i * 240; j < i * 240 + 240; j++)
            //    {
            //        if (j - oFile.AllEpoch.Count >= 0)
            //        {
            //            break;
            //        }

            //        foreach (var item in oFile.AllEpoch[j].AllSat)
            //        {
            //            var epoch = oFile.AllEpoch[i].Epoch;
            //            var sunL0 = Coordinate.SunLon(epoch.CommonT.Hour,
            //                epoch.CommonT.Minute, (double)epoch.CommonT.second, 8);
            //            var sat = item.Value;
            //            if (sat["SP4"] != 0)
            //            {
            //                double sp4Est = shm.Calculate(sat.IPP[0], sat.IPP[1] - sunL0);
            //                double sp4Org = sat["SP4"];
            //                sat["SP4"] -= sp4Est;
            //            }
            //        }
            //    }
            //}


            Write(oPath + ".ipp.b.txt", ippb);
            Write(oPath + ".ipp.l.txt", ippl);
            Write(oPath + ".meas.p4.txt", sp4);
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
