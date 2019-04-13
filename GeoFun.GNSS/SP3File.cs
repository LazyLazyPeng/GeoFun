using System;
using System.IO;
using System.Collections.Generic;

namespace GeoFun.GNSS
{
    public class SP3File
    {
        public string Path { get; set; }

        ///文件版本
        public string Version
        {
            get
            {
                return Header.Version;
            }
        }

        ///类型 P或V
        public string Type
        {
            get
            {
                return Header.OrbitType;
            }
        }

        ///开始时间
        public GPST StartTime
        {
            get
            {
                return Header.StartGPSTime;
            }
        }

        ///历元数
        public int EpochNum
        {
            get
            {
                return Header.EpochNum;
            }
        }

        ///文件头
        public SP3Header Header;

        public Dictionary<GPST, SP3Epoch> AllEpoch { get; set; } = new Dictionary<GPST, SP3Epoch>();

        public SP3File(string path = "")
        {
            Path = path;
            Header = new SP3Header();
        }

        /// <summary>
        /// 尝试读取数据
        /// </summary>
        /// <returns></returns>
        public bool TryRead()
        {
            if (!File.Exists(Path)) return false;

            string[] lines;
            try
            {
                lines = File.ReadAllLines(Path);
            }
            catch (Exception ex)
            {
                return false;
            }

            if (Header is null) Header = new SP3Header();

            //// Line1
            Header.Version = lines[0].Substring(0,2);
            Header.P_V_Flag = lines[0][2].ToString();

            Header.StartTime = GPST.Decode(lines[0].Substring(3,23));

            Header.EpochNum = Int32.Parse(lines[0].Substring(32, 7).Trim());//读完数据之后会更改此值
            Header.Data_Used = lines[0].Substring(40, 5).Trim();
            Header.Coordinate_Sys = lines[0].Substring(46, 5).Trim();
            Header.OrbitType = lines[0].Substring(52, 3).Trim();
            Header.Agency = lines[0].Substring(56, 4).Trim();

            //// Line2
            int weeks;
            double seconds;
            weeks = Convert.ToInt32(lines[1].Substring(3, 4).Trim());
            seconds = Convert.ToDouble(Math.Floor(Double.Parse(lines[1].Substring(8, 15).Trim())));
            Header.StartGPSTime = new GPST(weeks, seconds);
            Header.Epoch_Interval = Double.Parse(lines[1].Substring(24, 14).Trim());

            //读取卫星PRN号，3-7行
            Header.Num_Sats = Int32.Parse(lines[2].Substring(4, 2).Trim());
            Header.SatPRN = new List<string>();
            for (int ii = 0; ii < 5; ii++)
            {
                int j = 10;
                for (j = 10; j < 59; j = j + 3)
                {
                    if (lines[2+ii].Substring(j - 1, 3).Trim() != "0")
                    {
                        Header.SatPRN.Add(lines[2+ii].Substring(j - 1, 3).Trim());
                    }
                }
            }

            //读取卫星精度，8-12行
            Header.SatAccuracy = new List<string>(); 
            int i = 0;
            for (int ii = 0; ii < 5; ii++)
            {
                int j = 10;
                if (i < Header.Num_Sats)
                {
                    for (j = 10; j < 59; j = j + 3)
                    {
                        Header.SatAccuracy.Add(lines[7+ii].Substring(j - 1, 3).Trim());
                        i = i + 1;
                        if (i >= Header.Num_Sats)
                            break;
                    }
                }
            }

            for (i = 22; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("EOF")) break;

                if(lines[i].StartsWith("*"))
                {
                    SP3Epoch epoch = DecodeEpoch(lines,ref i);

                    AllEpoch.Add(epoch.Epoch,epoch);
                }
            }

            return true;
        }

        /// <summary>
        /// 读取一个历元的数据
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="lineNum"></param>
        /// <returns></returns>
        public SP3Epoch DecodeEpoch(string[] lines,ref int lineNum)
        {
            SP3Epoch epoch = new SP3Epoch();
            epoch.Epoch = GPST.Decode(lines[lineNum].Substring(1));

            Dictionary<string, SP3Sat> allSat = new Dictionary<string, SP3Sat>();

            int i = lineNum+1;
            for(; i < lines.Length; i++)
            {
                if(lines[i].StartsWith("*"))
                {
                    break;
                }

                SP3Sat sat = DecodeSat(lines[i]);

                epoch.AllSat.Add(sat.Prn,sat);
            }
            lineNum = i;

            return epoch;
        }
        
        /// <summary>
        /// 读取一颗卫星的数据
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public SP3Sat DecodeSat(string line)
        {
            SP3Sat sat = new SP3Sat();

            //// 移除多余的空格
            string[] segs = StringHelper.SplitFields(line);

            sat.Type = segs[0][0];
            sat.Prn = segs[0].Substring(1);

            sat.X = double.Parse(segs[1])*1e3;
            sat.Y = double.Parse(segs[2])*1e3;
            sat.Z = double.Parse(segs[3])*1e3;
            sat.C = double.Parse(segs[4]);

            if(segs.Length>=9)
            {
                sat.XBias = int.Parse(segs[5]);
                sat.YBias = int.Parse(segs[6]);
                sat.ZBias = int.Parse(segs[7]);
                sat.CBias = int.Parse(segs[8]);
            }

            return sat;
        }
    }
}