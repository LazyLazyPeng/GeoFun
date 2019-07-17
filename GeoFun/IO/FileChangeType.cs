using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.IO
{
    public enum FileChangeType
    {
        NewFolder,
        NewFile,
        Change,
        Delete,
        Rename,
        Unknown
    }
}
