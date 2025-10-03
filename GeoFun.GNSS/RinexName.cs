using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GeoFun.GNSS
{
    public class RinexName
    {
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public char FileType { get; set; }

        /// <summary>
        /// 测站名
        /// </summary>
        public string StationName { get; set; }

        /// <summary>
        /// 记录开始的DOY
        /// </summary>
        public int StartDOY { get; set; }

        /// <summary>
        /// 记录结束的DOY
        /// </summary>
        public int EndDOY { get; set; }

        /// <summary>
        /// 在一天中的序列号
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }

        public static RinexName Decode(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;

            // 优先尝试 RINEX3 的启发式解析
            try
            {
                if (Rinex3Name.TryParse(fileName, out var r3) && r3 != null && r3.IsRinex3)
                {
                    var r = new RinexName
                    {
                        Version = null,
                        FileType = string.IsNullOrEmpty(r3.FileType) ? '\0' : r3.FileType[0],
                        StationName = r3.Station,
                        Year = r3.Year ?? 0,
                        StartDOY = r3.Date.HasValue ? r3.Date.Value.DayOfYear : 0,
                        EndDOY = r3.Date.HasValue ? r3.Date.Value.DayOfYear : 0,
                        Sequence = 0
                    };
                    return r;
                }
            }
            catch
            {
                // 忽略 RINEX3 解析异常，回退到 RINEX2 解析
            }

            // 处理文件名（去路径）
            string name = Path.GetFileName(fileName);

            // 去掉压缩后缀（.gz .zip .Z）
            var comp = Regex.Match(name, @"\.(gz|zip|Z)$", RegexOptions.IgnoreCase);
            if (comp.Success) name = name.Substring(0, name.Length - comp.Value.Length);

            // 去掉最后的扩展名（如果有）
            int lastDot = name.LastIndexOf('.');
            string baseName = lastDot >= 0 ? name.Substring(0, lastDot) : name;

            // RINEX2 的约定：末尾 3(startDOY) +3(endDOY) +2(seq) +4(year) = 12 位数字
            if (baseName.Length < 13) return null; // 至少需要版本、类型、站名 + 12 数字

            string tail = baseName.Substring(baseName.Length - 12);
            if (!Regex.IsMatch(tail, @"^\d{12}$")) return null;

            int startDoy = int.Parse(tail.Substring(0, 3));
            int endDoy = int.Parse(tail.Substring(3, 3));
            int seq = int.Parse(tail.Substring(6, 2));
            int year = int.Parse(tail.Substring(8, 4));

            string prefix = baseName.Substring(0, baseName.Length - 12);

            // prefix = Version + FileType(1 char) + StationName
            // 采用从右往左：最后的字母/数字串为 StationName，前一字符为 FileType，其余为 Version
            var m = Regex.Match(prefix, @"^(?<ver>.*?)(?<type>[A-Za-z])(?<station>[A-Za-z0-9\-]{1,9})$");
            string ver = null;
            char ftype = '\0';
            string station = null;
            if (m.Success)
            {
                ver = m.Groups["ver"].Value;
                ftype = m.Groups["type"].Value[0];
                station = m.Groups["station"].Value;
            }
            else
            {
                // 兜底：尽量提取末尾 1-9 个字母数字作为站名
                var sm = Regex.Match(prefix, @"(?<s>[A-Za-z0-9]{1,9})$");
                if (sm.Success)
                {
                    station = sm.Groups["s"].Value;
                    int idx = prefix.Length - station.Length - 1;
                    if (idx >= 0)
                    {
                        ftype = prefix[idx];
                        ver = prefix.Substring(0, idx);
                    }
                    else
                    {
                        ver = string.Empty;
                        ftype = '\0';
                    }
                }
                else
                {
                    // 无法识别前缀
                    return null;
                }
            }

            var rinex2 = new Rinex2Name
            {
                Version = string.IsNullOrEmpty(ver) ? null : ver,
                FileType = ftype,
                StationName = station,
                StartDOY = startDoy,
                EndDOY = endDoy,
                Sequence = seq,
                Year = year
            };

            return rinex2;
        }
    }
}
