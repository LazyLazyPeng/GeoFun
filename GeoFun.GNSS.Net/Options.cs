using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Codeaddicts.libArgument;
//using Codeaddicts.libArgument.Attributes;

namespace GeoFun.GNSS.Net
{
    public sealed class Options
    {
        public Options()
        {
        }

        [Argument("-o", "--output")]
        public string output = "";

        [Argument("-l","--list")]
        public string list = "product";
    }
}
