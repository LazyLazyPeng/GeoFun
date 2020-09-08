using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class FileName
    {
        public static bool ParseRinex2(string oName, out string stationName, out int doy, out int year)
        {
            if (oName.Length != 12)
            {
                throw (new ArgumentException("文件名不正确，请检查:" + oName));
            }

            stationName = oName.Substring(0, 4);
            doy = int.Parse(oName.Substring(4, 3));
            year = int.Parse(oName.Substring(9, 2));

            if (year < 50) year += 2000;
            else year += 1900;

            return true;
        }

        public static bool ParseSP3Name(string name, out string centerName, out int doy, out int day)
        {
            centerName = "";
            doy = 0;
            day = 0;
            if (string.IsNullOrWhiteSpace(name)) return false;

            // 去掉后缀
            string nameNoExt = name;
            if (nameNoExt.EndsWith(".sp3")) nameNoExt = nameNoExt.Substring(0, nameNoExt.Length - 4);

            centerName = nameNoExt.Substring(0, 3);
            if (!int.TryParse(nameNoExt.Substring(3, 4), out doy)) return false;
            if (!int.TryParse(nameNoExt.Substring(7, 1), out day)) return false;
            return true;
        }
    }
}
