using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun
{
    public class ArrayHelper
    {
        public static int TakeIntLittle(ref byte[] buffer, int start, int len)
        {
            byte[] temp = new byte[len];

            Array.ConstrainedCopy(buffer, start, temp, 0, len);

            return BitConverter.ToInt32(temp,0);
        }
    }
}
