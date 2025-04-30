using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoFun;

namespace GIon
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            //double b, l, h;
            //Coordinate.XYZ2BLH(new double[] { -3530185.5598, 4118797.3133, 3344036.9115 },out b,out l,out h,Ellipsoid.ELLIP_CGCS2000);
            //Case case1 = new Case(@"E:\Data\Typhoon\feiyan\gnss");
            //case1.SearchObsFiles();
            //case1.GetStationDOY();
            //case1.Download();
            //case1.ReadFiles();

            //IonoHelper.Calculate(@"E:\Data\Typhoon\Case\ramasun\gnss\qion1971.140");
            Application.EnableVisualStyles();
            Application.Run(new FrmMain());
            // Test if input arguments were supplied.
            //if (args.Length == 0)
            //{
            //    Console.WriteLine("Please enter a numeric argument.");
            //    Console.WriteLine("Usage: Factorial <num>");
            //    return 1;
            //}

            //// Try to convert the input arguments to numbers. This will throw
            //// an exception if the argument is not a number.
            //// num = int.Parse(args[0]);
            //int num;
            //bool test = int.TryParse(args[0], out num);
            //if (!test)
            //{
            //    Console.WriteLine("Please enter a numeric argument.");
            //    Console.WriteLine("Usage: Factorial <num>");
            //    return 1;
            //}

            //// Calculate factorial.
            //long result = Functions.Factorial(num);

            //// Print result.
            //if (result == -1)
            //    Console.WriteLine("Input must be >= 0 and <= 20.");
            //else
            //    Console.WriteLine($"The Factorial of {num} is {result}.");

            //return 0;
        }
    }

    public class Functions
    {
        public static long Factorial(int n)
        {
            // Test for invalid input.
            if ((n < 0) || (n > 20))
            {
                return -1;
            }

            // Calculate the factorial iteratively rather than recursively.
            long tempResult = 1;
            for (int i = 1; i <= n; i++)
            {
                tempResult *= i;
            }
            return tempResult;
        }
    }
}
