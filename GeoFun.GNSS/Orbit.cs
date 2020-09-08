using MathNet.Numerics.Integration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.Distributions;
using GeoFun.MathUtils;

namespace GeoFun.GNSS
{
    public class Orbit
    {
        public double Interval { get; set; } = 900;

        // 起始时间
        public GPST StartTime
        {
            get
            {
                if (Files is null || Files.Count == 0)
                {
                    return null;
                }

                return Files[0].StartTime;
            }
        }
        public int EpochNum
        {
            get
            {
                if (Epoches is null) return 0;
                return Epoches.Count;
            }
        }
        public string Folder { get; set; }

        public List<SP3File> Files = new List<SP3File>();
        public List<SP3Epoch> Epoches = new List<SP3Epoch>();

        public Orbit(string folder)
        {
            Folder = folder;
        }

        /// <summary>
        /// 读取文件夹下的星历
        /// </summary>
        /// <param name="folder"></param>
        /// <remarks>星历必须连续</remarks>
        public void Read(string folder)
        {
            Folder = folder;
            for (int i = 0; i < Files.Count; i++)
            {
                if (!Files[i].TryRead())
                {
                    throw new Exception("读取文件失败:" + Files[i].Path);
                }
            }

            Epoches.AddRange(Files[0].AllEpoch);
            Interval = Files[0].Interval;
            for (int i = 1; i < Files.Count; i++)
            {
                if (Math.Abs(Interval - Files[i].Interval) > 1e-10)
                {
                    throw new Exception("采样率不一致:" + Files[i].Path);
                }

                Epoches.AddRange(Files[i].AllEpoch);
            }
        }

        public void GetAllSp3Files(string folder = null)
        {
            if (folder == null)
            {
                folder = Folder;
            }
            else
            {
                Folder = folder;
            }

            DirectoryInfo dir = new DirectoryInfo(folder);
            if (!dir.Exists) return;

            var paths = dir.GetFiles("*.sp3");
            if (paths.Length == 0) return;


            foreach (var path in paths)
            {
                Files.Add(new SP3File(path.FullName));
            }

            Files.Sort((left, right) =>
            {
                if ((left.Week == right.Week) && (left.DayOfWeek == right.DayOfWeek))
                {
                    return -1;
                }
                else if (left.Week < right.Week)
                {
                    return 0;
                }
                else if ((left.Week == right.Week)
                      && (left.DayOfWeek < right.DayOfWeek))
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            });

            // 星历不连续
            if (!CheckConsist()) return;
        }

        public bool CheckConsist()
        {
            if (Files.Count == 1) return true;

            int dayNum = Files[0].AllDayNum;
            for (int i = 1; i < Files.Count; i++)
            {
                dayNum++;
                if (dayNum != Files[i].AllDayNum)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 计算卫星位置
        /// </summary>
        /// <param name="t0"></param>
        /// <param name="prn"></param>
        /// <returns></returns>
        public double[] GetSatPos(GPST t0, string prn)
        {
            double[] p = { 0, 0, 0 };

            if (EpochNum <= 0) return p;

            // 00:00:00之前，无法插值
            if (t0 - StartTime + 1e-13 < 0) return p;
            // 00:00:00之后，无法插值
            if (t0 - StartTime > EpochNum * Interval) return p;

            // 10阶插值
            double[] t = new double[10];
            double[] x = new double[10];
            double[] y = new double[10];
            double[] z = new double[10];

            GPST ts = StartTime;

            int index = (int)System.Math.Floor((t0 - ts) / Interval);

            // Console.WriteLine("index:{0}", index);

            // 刚好落在采样点上
            if (Math.Abs(ts - t0 + Interval * index) < 1e-13)
            {
                p[0] = Epoches[index][prn].X;
                p[1] = Epoches[index][prn].Y;
                p[2] = Epoches[index][prn].Z;
                return p;
            }
            else if (Math.Abs(ts - t0 + Interval * index + Interval) < 1e-13)
            {
                p[0] = Epoches[index + 1][prn].X;
                p[1] = Epoches[index + 1][prn].Y;
                p[2] = Epoches[index + 1][prn].Z;
                return p;
            }

            // 在开始的几个历元内
            if (index < 4)
            {
                for (int i = 0; i < 10; i++)
                {
                    x[i] = Epoches[i][prn].X;
                    y[i] = Epoches[i][prn].Y;
                    z[i] = Epoches[i][prn].Z;

                    t[i] = Epoches[i].Epoch.TotalSeconds;
                }
            }
            // 在结束的几个历元内
            else if (EpochNum - index < 6)
            {
                for (int i = 0; i < 10; i++)
                {
                    x[i] = Epoches[EpochNum - 10 + i][prn].X;
                    y[i] = Epoches[EpochNum - 10 + i][prn].Y;
                    z[i] = Epoches[EpochNum - 10 + i][prn].Z;

                    t[i] = Epoches[EpochNum - 10 + i].Epoch.TotalSeconds;
                }
            }
            // 在中间
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    x[i] = Epoches[index - 4 + i][prn].X;
                    y[i] = Epoches[index - 4 + i][prn].Y;
                    z[i] = Epoches[index - 4 + i][prn].Z;

                    t[i] = Epoches[index - 4 + i].Epoch.TotalSeconds;

                }
            }

            p[0] = Interp.GetValueLagrange(10, t, x, t0.TotalSeconds);
            p[1] = Interp.GetValueLagrange(10, t, y, t0.TotalSeconds);
            p[2] = Interp.GetValueLagrange(10, t, z, t0.TotalSeconds);
            return p;

        }

        /// <summary>
        /// 计算卫星位置
        /// </summary>
        /// <param name="oEpo"></param>
        public void GetSatPos(ref OEpoch oEpo)
        {
            if (oEpo is null || oEpo.SatNum == 0) return;

            double pRange = 0d;
            foreach (var sat in oEpo.AllSat.Values)
            {
                pRange = sat["P2"];
                if (pRange == 0d) pRange = sat["P1"];
                if (pRange == 0d) pRange = sat["C1"];
                if (pRange == 0d)
                    continue;

                GPST t0 = new GPST(oEpo.Epoch);
                t0.AddSeconds(-pRange / Common.C0);
                double[] satPos = GetSatPos(t0, sat.SatPRN);
                sat.SatCoor = satPos;
            }
        }
    }
}
