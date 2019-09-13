using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;

namespace GeoFun.GNSS
{
    public class OFile
    {
        public int Year { get; set; }
        public int DayOfYear { get; set; }
        public int DOY { get; set; }

        /// <summary>
        /// �ļ�·��
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// �۲⿪ʼʱ��
        /// </summary>
        public GPST StartTime;

        /// <summary>
        /// �۲����ʱ��
        /// </summary>
        public GPST EndTime;

        /// <summary>
        /// ��һ����Ԫ������
        /// </summary>
        public int StartIndex = -1;

        /// <summary>
        /// ���һ����Ԫ������
        /// </summary>
        public int EndIndex = -1;

        /// <summary>
        /// �ļ�ͷ
        /// </summary>
        public OHeader Header;

        /// <summary>
        /// ������Ԫ�Ĺ۲�����
        /// </summary>
        public List<OEpoch> AllEpoch = new List<OEpoch>();

        public OFile(string path)
        {
            Path = path;
        }

        public bool TryRead()
        {
            StreamReader sr = new StreamReader(Path);
            Header = new OHeader();
            //Header.StartIndex = AllEpoch.Count;
            string line = sr.ReadLine();  //һ������
            //��ȡͷ
            while (line != "" && line != null)
            {
                string flag = line.Substring(60).Trim();
                if (flag == "END OF HEADER") break;
                switch (flag)
                {
                    case "RINEX VERSION / TYPE":
                        Header.version = Convert.ToDouble(line.Substring(0, 9).Trim());
                        Header.systemType = line.Substring(40, 1).Trim();
                        break;
                    case "MARKER NAME":
                        Header.markName = line.Substring(0, 4).Trim();
                        break;
                    case "MARKER NUMBER":
                        Header.markNumber = line.Substring(0, 20).Trim();
                        break;
                    case "OBSERVER / AGENCY":
                        Header.observerName = line.Substring(0, 20).Trim();
                        Header.observerAgencyName = line.Substring(20, 40).Trim();
                        break;
                    case "REC # / TYPE / VERS":
                        Header.rcvNumber = line.Substring(0, 20).Trim();
                        Header.rcvType = line.Substring(20, 20).Trim();
                        Header.rcvVer = line.Substring(40, 20).Trim();
                        break;
                    case "ANT # / TYPE":
                        Header.antNumber = line.Substring(0, 20).Trim();
                        Header.antType = line.Substring(20, 20).Trim();
                        break;
                    case "APPROX POSITION XYZ":
                        Header.approxPos = new Coor3();
                        Header.approxPos.X = Convert.ToDouble(line.Substring(0, 14).Trim());
                        Header.approxPos.Y = Convert.ToDouble(line.Substring(14, 14).Trim());
                        Header.approxPos.Z = Convert.ToDouble(line.Substring(28, 14).Trim());
                        break;
                    case "ANTENNA: DELTA H/E/N":
                        Header.antDelta = new Coor3();
                        Header.antDelta.U = Convert.ToDouble(line.Substring(0, 14).Trim());
                        Header.antDelta.E = Convert.ToDouble(line.Substring(14, 14).Trim());
                        Header.antDelta.N = Convert.ToDouble(line.Substring(28, 14).Trim());
                        break;
                    case "WAVELENGTH FACT L1/2":
                        Header.waveLenFactorL1 = Convert.ToInt32(line.Substring(0, 6).Trim());
                        Header.waveLenFactorL2 = Convert.ToInt32(line.Substring(6, 6).Trim());
                        break;
                    case "# / TYPES OF OBSERV":
                        Header.obsTypeNum = Convert.ToInt32(line.Substring(0, 6).Trim());
                        Header.obsTypeList = new List<string>();
                        for (int i = 0; i < Header.obsTypeNum; i++)
                        {
                            Header.obsTypeList.Add(line.Substring(6 + 6 * i, 6).Trim());
                        }
                        break;
                    case "INTERVAL":
                        Header.interval = double.Parse(line.Substring(0, 11).Trim());
                        break;
                    case "TIME OF FIRST OBS":
                        break;

                }
                line = sr.ReadLine();
            }//ͷ�Ѷ�ȡ��

            //��ȡ�۲�ֵ
            line = sr.ReadLine();
            //obsdata_allepoch = new List<OBSDATA_EPOCH>();//ȫ�ֱ�����ʼ��
            OEpoch epoch = new OEpoch();//
            epoch.AllSat = new Dictionary<string, OSat>();
            epoch.PRNList = new List<string>();
            while (line != "" && line != null)
            {
                string flag = "";
                if (line.Length > 60)
                {
                    flag = line.Substring(60).Trim();
                }

                //// 60���Ժ��������COMMENT,����Ϊ��ע����
                if (flag.Contains("COMMENT"))
                {
                    line = sr.ReadLine();
                    continue;
                }

                try
                {
                    epoch = new OEpoch();

                    //// ��ȡ��Ԫ
                    GPST epochT = GPST.Decode(line.Substring(0, 26));
                    if (epochT is null)
                    {
                        line = sr.ReadLine();
                        continue;
                    }
                    epoch.Epoch = epochT;

                    //// ��Ԫ��ʶ
                    int epoFlag = 0;
                    if (int.TryParse(line.Substring(26, 3), out epoFlag))
                    {
                        epoch.Flag = epoFlag;
                    }

                    //// �۲⵽�����ǵ�����
                    int prnNum = int.Parse(line.Substring(29, 3).Trim());

                    //// С��12�����ǣ�ֻ���һ��
                    if (prnNum <= 12)
                    {
                        for (int j = 0; j < prnNum; j++)
                        {
                            epoch.PRNList.Add(line.Substring(32 + j * 3, 3).Trim().Replace(' ', '0'));
                        }
                        if (line.Split('.').Length == 3)
                        {
                            epoch.ClockBias = double.Parse(line.Substring(68, 12).Trim());
                        }
                    }

                    //// ����12�����ǣ���Ҫ�������������
                    else
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            epoch.PRNList.Add(line.Substring(32 + j * 3, 3).Trim().Replace(' ', '0'));
                        }
                        if (line.Split('.').Length == 3)
                        {
                            epoch.ClockBias = double.Parse(line.Substring(68, 12).Trim());
                        }

                        line = sr.ReadLine();
                        for (int j = 0; j < prnNum - 12; j++)
                        {
                            epoch.PRNList.Add(line.Substring(32 + j * 3, 3).Trim().Replace(' ', '0'));
                        }
                    }

                    for (int satI = 0; satI < epoch.PRNList.Count; satI++)
                    {
                        OSat oSat = new OSat();
                        oSat.StationName = Header.markName;
                        oSat.SatData = new Dictionary<string, double>();
                        oSat.LLI = new Dictionary<string, int>();
                        oSat.SignalStrength = new Dictionary<string, int>();
                        oSat.Epoch = epoch.Epoch;
                        oSat.SatPRN = epoch.PRNList[satI].ToString();

                        double obsValue = 0d;
                        int lli = -1;
                        int singalStrength = -1;
                        int lineNum = (int)Math.Ceiling(Header.obsTypeNum / 5d);
                        int obsNumPerLine = 5;
                        int obsIndex = 0;
                        for (int i = 0; i < lineNum; i++)
                        {
                            line = ReadLine(sr);
                            obsNumPerLine = (Header.obsTypeNum - i * 5) > 5 ? 5 : Header.obsTypeNum - i * 5;
                            for (int j = 0; j < obsNumPerLine; j++)
                            {
                                obsIndex = i * 5 + j;

                                //// ��ʼ���۲�ֵ
                                lli = -1;
                                singalStrength = -1;
                                oSat.SatData.Add(Header.obsTypeList[i * 5 + j], 0d);
                                oSat.LLI[Header.obsTypeList[obsIndex]] = lli;
                                oSat.SignalStrength[Header.obsTypeList[obsIndex]] = singalStrength;
                                try
                                {
                                    if (double.TryParse(line.Substring(j * 16, 14), out obsValue))
                                    {
                                        oSat.SatData[Header.obsTypeList[obsIndex]] = obsValue;
                                    }

                                    if (int.TryParse(line.Substring(j * 16 + 14, 1), out lli))
                                    {
                                        oSat.LLI[Header.obsTypeList[obsIndex]] = lli;
                                    }

                                    if (int.TryParse(line.Substring(j * 16 + 15, 1), out singalStrength))
                                    {
                                        oSat.SignalStrength[Header.obsTypeList[obsIndex]] = singalStrength;
                                    }
                                }
                                catch (Exception ex01)
                                {
                                    continue;
                                }
                            }
                        }

                        epoch.AllSat.Add(oSat.SatPRN, oSat);
                    }
                    AllEpoch.Add(epoch);
                }
                catch (Exception ex)
                {
                    return false;
                }

                line = sr.ReadLine();
            }

            sr.Close();

            return true;
        }
        public string ReadLine(StreamReader sr)
        {
            string line = sr.ReadLine();
            if (line.Length < 80)
            {
                line = line.PadRight(80);
            }

            return line;
        }

        public static OFile Read(string path)
        {
            OFile file = new OFile(path);

            if(file.TryRead())
            {
                return file;
            }
            else
            {
                return null;
            }
        }
    }
}