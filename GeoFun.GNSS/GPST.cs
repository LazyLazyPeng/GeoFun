using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class GPST
    {
        public DateTime UTC { get; private set; }
        public int Weeks;
        public double Seconds;

        public double JD = 0d;
        public double MJD = 0d;
    }
}
