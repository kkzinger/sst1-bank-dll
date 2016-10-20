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
        [DllImport("bank_customers.dll")]
        public static extern int Create

        static void Main(string[] args)
        {
        }
    }
}
