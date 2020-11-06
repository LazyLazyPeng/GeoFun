using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace GeoFun.Spatial
{
    public interface IRasterFile
    {
        string Path { get; set; }

        void Write();
        void WriteAs(string Path);
    }
}
