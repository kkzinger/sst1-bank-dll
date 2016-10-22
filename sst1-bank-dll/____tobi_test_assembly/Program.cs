using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
namespace ____tobi_test_assembly
{
    // Customer Data
    [StructLayout(LayoutKind.Sequential, CharSet =CharSet.Ansi)]
    struct CUSTOMER
    {
         uint CID;
         String FirstName;
         String LastName;
         String Street;
         String StreetNr;
         String City;
         String PostalCode;
         String Country;
         Byte Active;
    };

    class Program
    {
        //[DllImport("../../../Release/bank_currency.dll")]
        //public static extern int bar();

        //[DllImport("../../../Release/bank_entitycomponent.dll")]
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        public static extern int _initEntity();

        //   [DllImport("../../../Release/bank_customers.dll",CallingConvention = CallingConvention.Cdecl)]
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Create(StringBuilder FirstName, StringBuilder LastName, StringBuilder Street, StringBuilder StreetNr, StringBuilder City, StringBuilder PostalCode, StringBuilder Country);
        //     [DllImport("../../../Release/bank_customers.dll")]
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet =CharSet.Ansi)]
        public static extern int Read(uint CID, CUSTOMER resultCustomer);

        static void Main(string[] args)
        {
            Console.WriteLine(_initEntity());
            CUSTOMER C = new CUSTOMER();
            

            StringBuilder FirstName = new StringBuilder("Tobias");
            StringBuilder LastName = new StringBuilder("Mayer");
            StringBuilder Street = new StringBuilder("Salzburger Str.");
            StringBuilder StreetNr = new StringBuilder("17a");
            StringBuilder City = new StringBuilder("Neumarkt");
            StringBuilder PostalCode = new StringBuilder("5202");
            StringBuilder Country = new StringBuilder("AUSTRIA");

            Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
            Console.WriteLine(Read(1,  C));
        }
    }
}
