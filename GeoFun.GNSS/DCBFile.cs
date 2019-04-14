using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class DCBFile
    {
        public string Path { get; set; }
        public Dictionary<string, double> DCBList = new Dictionary<string, double>();
        public Dictionary<string, double> DCBBias = new Dictionary<string, double>();

        public int Year { get; set; }
        public int Month { get; set; }

        public DCBFile(string path)
        {
            Path = path;
        }

        public bool TryRead()
        {
            if (!File.Exists(Path)) return false;

            string[] lines = File.ReadAllLines(Path);

            int startLineNum = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("***"))
                {
                    startLineNum = i + 1;
                    break;
                }
            }

            if (startLineNum < 0) return false;

            double dcb, dcbBias;
            for (int i = startLineNum; i < lines.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] segs = StringHelper.SplitFields(lines[i]);
                if (segs.Length > 2)
                {
                    if(double.TryParse(segs[1],out dcb))
                    {
                        DCBList.Add(segs[0], dcb);
                    }

                    if(double.TryParse(segs[2],out dcbBias))
                    {
                        DCBBias.Add(segs[0], dcbBias);
                    }
                }
            }

            return true;
        }

        public static DCBFile Read(string path)
        {
            DCBFile dcbFile = new DCBFile(path);

            dcbFile.TryRead();
            return dcbFile;
        }
    }
}
