using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class FileName
    {
        public static bool ParseRinex2(string oName,out string stationName, out int doy, out int year)
        {
            if(oName.Length!=12)
            {
                throw (new ArgumentException("文件名不正确，请检查:"+oName));
            }

            stationName = oName.Substring(0,4);
            doy = int.Parse(oName.Substring(4, 3));
            year = int.Parse(oName.Substring(9, 2));

            if (year < 50) year += 2000;
            else year += 1900;

            return true;
        }
    }
}
