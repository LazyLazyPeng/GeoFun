using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIon
{
    class Program
    {
        static void Main(string[] args)
        {
            Case case1 = new Case(@"E:\Data\Typhoon\feiyan\gnss");
            case1.SearchObsFiles();
            case1.GetStationDOY();
            case1.Download();
            case1.ReadFiles();
        }
    }
}
