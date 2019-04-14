using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class SHPRecord
    {
        /// <summary>
        /// Record Number
        /// </summary>
        public int RecordNum = 0;

        /// <summary>
        /// Content Length
        /// </summary>
        public int ContentLen = 0;

        public IFeature Feature;
    }
}
