using GeoFun.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.GNSS
{
    public class DCBHelper
    {
        public List<DCBFile> Files = new List<DCBFile>();

        public DCBFile GetDCB(int year, int month,string type="p1p2")
        {
            for(int i =0; i <Files.Count; i++)
            {
                DCBFile file = Files[i];
                if (file.Type != type) continue;
                if (file.Year != year) continue;
                if (file.Month != month) continue;

                return file;
            }

            return null;
        }
    }
}
