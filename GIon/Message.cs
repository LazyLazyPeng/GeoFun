using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIon
{
    public class Message
    {
        public static void Error(string mess)
        {
            Console.WriteLine(mess);
        }

        public static void Warning(string mess)
        {
            Console.WriteLine(mess);
        }
    }
}
