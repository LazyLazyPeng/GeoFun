using System;
using System.Collections.Generic;
using System.Text;

namespace GeoFun.Spatial
{
    public class ShapeNull:IFeature
    {
        public bool HasM { get; set; } = false;

        public int Data = 0;

        /// <summary>
        /// Size in bytes
        /// </summary>
        public int Size
        {
            get
            {
                return 4;
            }
        }
    }
}
