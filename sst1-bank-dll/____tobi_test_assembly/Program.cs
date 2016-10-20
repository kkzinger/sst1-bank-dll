using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support

namespace ____tobi_test_assembly
{
    class Program
    {
        [DllImport("../../../Release/bank_currency.dll")]
        public static extern int bar();

        static void Main(string[] args)
        {
            Console.WriteLine(bar());
        }
    }
}
