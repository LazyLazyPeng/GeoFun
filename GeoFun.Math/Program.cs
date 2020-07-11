using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoFun.MathUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j <=i; j++)
                {
                    Console.WriteLine(string.Format("{0} {1} {2}", i, j, Legendre.lpmv(i, j, System.Math.PI/3d)));
                }
            }
            Console.ReadKey();
        }
    }
}
