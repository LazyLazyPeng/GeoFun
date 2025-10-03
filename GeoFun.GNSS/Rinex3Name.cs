using System;
using System.IO;
using System.Text.RegularExpressions;

namespace GeoFun.GNSS
{
    /// <summary>
    /// Represents a RINEX file name (supports heuristics for RINEX2 and RINEX3 common naming).
    /// Use TryParse/Parse to obtain a populated instance.
    /// </summary>
    public class Rinex3Name
    {
        // 原始输入
        public string OriginalFileName { get; private set; }
        // 文件名（去目录）
        public string FileName { get; private set; }
        // 主扩展名（不含压缩后缀），例如 "rnx", "21o", "20n", "obs"
        public string Extension { get; private set; }
        // 压缩后缀，例如 "gz", "zip", null 表示未压缩
        public string Compression { get; private set; }
        // 站点标识（如果能解析）
        public string Station { get; private set; }
        // 文件类型字符，例如 "o" (obs), "n" (nav), "d" (dcb/clk/...), 也可能是 "R" 等
        public string FileType { get; private set; }
        // 年（如果能解析）
        public int? Year { get; private set; }
        // 如果可以解析出完整日期，则提供 Date
        public DateTime? Date { get; private set; }
        // 文件是否被认为是 RINEX3（启发式判断）
        public bool IsRinex3 { get; private set; }
        // 是否成功解析到关键字段（站点或类型或年份）
        public bool IsValid { get; private set; }

        // 可扩展的解析模式：如果需要可以公开以添加自定义规则（不在此示例中暴露修改）
        private static readonly Regex[] Patterns = new[]
        {
            // STATION_YYYYMMDD_serial_type.ext or STATION-YYYYMMDD-type.ext
            new Regex(@"^(?<station>[A-Za-z0-9]{1,9})[_\-](?<date>\d{8})[_\-]?(?<serial>\d+)?[_\-]?(?<type>[A-Za-z])?$", RegexOptions.Compiled),
            // STATION_YYYYDDD_serial_type.ext (year + day-of-year)
            new Regex(@"^(?<station>[A-Za-z0-9]{1,9})[_\-](?<yeardoy>\d{7})[_\-]?(?<serial>\d+)?[_\-]?(?<type>[A-Za-z])?$", RegexOptions.Compiled),
            // STATIONYYDDD.type  (old compact format)
            new Regex(@"^(?<station>[A-Za-z0-9]{1,9})(?<yyddd>\d{5})$", RegexOptions.Compiled),
            // STATION.type where type char is just before extension: e.g., "ABCD001.O" or "ABCD2021o"
            new Regex(@"^(?<station>[A-Za-z0-9]{1,9}).*(?<type>[onmdkOONMDK])$", RegexOptions.Compiled)
        };

        private Rinex3Name() { }

        public static Rinex3Name Parse(string pathOrFileName)
        {
            if (TryParse(pathOrFileName, out var result))
                return result;
            throw new FormatException("无法解析 RINEX 文件名: " + pathOrFileName);
        }

        public static bool TryParse(string pathOrFileName, out Rinex3Name result)
        {
            result = new Rinex3Name();
            if (string.IsNullOrWhiteSpace(pathOrFileName))
            {
                result.IsValid = false;
                return false;
            }

            result.OriginalFileName = pathOrFileName;
            result.FileName = Path.GetFileName(pathOrFileName);

            // 分离压缩后缀（例如 .gz .zip .Z）
            string name = result.FileName;
            result.Compression = null;
            var compMatch = Regex.Match(name, @"\.(gz|zip|Z)$", RegexOptions.IgnoreCase);
            if (compMatch.Success)
            {
                result.Compression = compMatch.Groups[1].Value.ToLowerInvariant();
                name = name.Substring(0, name.Length - compMatch.Value.Length);
            }

            // 分离主扩展名（最后一个 '.' 之后）
            result.Extension = null;
            int lastDot = name.LastIndexOf('.');
            string baseName = name;
            if (lastDot >= 0)
            {
                result.Extension = name.Substring(lastDot + 1);
                baseName = name.Substring(0, lastDot);
            }

            // 规范化 extension 到小写用于检测
            string extLower = result.Extension?.ToLowerInvariant() ?? string.Empty;

            // 检测是否为 RINEX3 的常见扩展形式：例如 "21o" / "20n" 等
            var yearTypeMatch = Regex.Match(extLower, @"^(?<yy>\d{2})(?<t>[a-z])$");
            if (yearTypeMatch.Success)
            {
                int yy = int.Parse(yearTypeMatch.Groups["yy"].Value);
                result.Year = 2000 + yy;
                result.FileType = yearTypeMatch.Groups["t"].Value;
                // RINEX3 常用两位年份+类型表示，所以当匹配到此格式时标记为 RINEX3
                result.IsRinex3 = true;
            }
            else
            {
                // 扩展名 rnx 常见于 RINEX 2/3，无法由扩展名单独区分
                result.IsRinex3 = false;
            }

            // 尝试按多个模式解析 baseName
            bool matched = false;
            foreach (var rx in Patterns)
            {
                var m = rx.Match(baseName);
                if (!m.Success) continue;

                if (m.Groups["station"]?.Success == true)
                    result.Station = m.Groups["station"].Value;

                if (m.Groups["date"]?.Success == true)
                {
                    // YYYYMMDD
                    string d = m.Groups["date"].Value;
                    if (DateTime.TryParseExact(d, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dt))
                    {
                        result.Date = dt;
                        result.Year = dt.Year;
                    }
                }
                else if (m.Groups["yeardoy"]?.Success == true)
                {
                    // YYYYDDD
                    string yd = m.Groups["yeardoy"].Value;
                    if (yd.Length == 7)
                    {
                        if (int.TryParse(yd.Substring(0, 4), out int y) && int.TryParse(yd.Substring(4, 3), out int doy))
                        {
                            try
                            {
                                result.Date = new DateTime(y, 1, 1).AddDays(doy - 1);
                                result.Year = y;
                            }
                            catch { /* ignore invalid */ }
                        }
                    }
                }
                else if (m.Groups["yyddd"]?.Success == true)
                {
                    // YYDDD
                    string yyddd = m.Groups["yyddd"].Value;
                    if (yyddd.Length == 5)
                    {
                        if (int.TryParse(yyddd.Substring(0, 2), out int yy2) && int.TryParse(yyddd.Substring(2, 3), out int doy2))
                        {
                            int y = 2000 + yy2;
                            try
                            {
                                result.Date = new DateTime(y, 1, 1).AddDays(doy2 - 1);
                                result.Year = y;
                            }
                            catch { }
                        }
                    }
                }

                if (m.Groups["type"]?.Success == true)
                    result.FileType = m.Groups["type"].Value.ToLowerInvariant();

                matched = true;
                break;
            }

            // 如果未通过模式解析到 FileType，试着从 baseName 最末尾的单个字母推断（常见约定）
            if (string.IsNullOrEmpty(result.FileType))
            {
                var tmatch = Regex.Match(baseName, @"(?<t>[onmdk])$", RegexOptions.IgnoreCase);
                if (tmatch.Success)
                {
                    result.FileType = tmatch.Groups["t"].Value.ToLowerInvariant();
                }
            }

            // 如果扩展为两位年+type 且之前未解析到 station，从 baseName 中尝试提取站点常见前缀（前 1-9 个字母/数字）
            if (result.IsRinex3 && string.IsNullOrEmpty(result.Station))
            {
                var stMatch = Regex.Match(baseName, @"^(?<s>[A-Za-z0-9]{1,9})");
                if (stMatch.Success)
                    result.Station = stMatch.Groups["s"].Value;
            }

            // 标记有效性：至少应该包含 文件类型 或 站点 或 年份 中的一个
            result.IsValid = !string.IsNullOrEmpty(result.FileType) || !string.IsNullOrEmpty(result.Station) || result.Year.HasValue;

            // 进一步启发式：如果扩展为 "rnx" 且文件名中有四位年份或两位年+type片段，尝试判断是否为 RINEX3（保守）
            if (!result.IsRinex3 && extLower == "rnx")
            {
                // 如果文件名包含 "3.0" 或 "_03" 等常见版本标记，也可认为是 RINEX3（简单检查）
                if (Regex.IsMatch(baseName, @"\b3\.0\b") || Regex.IsMatch(baseName, @"_?3[_\-\.]"))
                    result.IsRinex3 = true;
            }

            result.Extension = result.Extension ?? string.Empty;
            return result.IsValid;
        }

        public override string ToString()
        {
            return $"{FileName} (Station={Station ?? "-"}, Year={Year?.ToString() ?? "-"}, Date={Date?.ToString("yyyy-MM-dd") ?? "-"}, Type={FileType ?? "-"}, Ext={Extension}{(Compression != null ? "." + Compression : "")}, RINEX3={IsRinex3}, Valid={IsValid})";
        }
    }
}
