using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoFun.GNSS
{
    /// <summary>
    /// Represents the name of a RINEX (Receiver Independent Exchange Format) file in the RINEX 2 format.
    /// </summary>
    /// <remarks>This class provides functionality to generate a RINEX 2 file name string based on its
    /// properties. The file name is constructed using the RINEX 2 naming convention, which includes details such as the
    /// version, file type, station name, start and end day of year, sequence number, and year.</remarks>
    public class Rinex2Name : RinexName
    {
        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3:D3}{4:D3}{5:D2}{6:D4}", Version, FileType, StationName, StartDOY, EndDOY, Sequence, Year);
        }
    }
}
