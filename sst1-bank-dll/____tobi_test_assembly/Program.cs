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
        //[DllImport("../../../Release/bank_currency.dll")]
        //public static extern int bar();

        [DllImport("../../../Release/bank_entitycomponent.dll")]
        public static extern int _initEntity();

        [DllImport("../../../Release/bank_customers.dll")]
        public static extern int Create(StringBuilder FirstName, StringBuilder LastName, StringBuilder Street, StringBuilder StreetNr, StringBuilder City, StringBuilder PostalCode, StringBuilder Country);


        static void Main(string[] args)
        {
            Console.WriteLine(_initEntity());

            

            StringBuilder FirstName = new StringBuilder("Tobias");
            StringBuilder LastName = new StringBuilder("Mayer");
            StringBuilder Street = new StringBuilder("Salzburger Str.");
            StringBuilder StreetNr = new StringBuilder("17a");
            StringBuilder City = new StringBuilder("Neumarkt");
            StringBuilder PostalCode = new StringBuilder("5202");
            StringBuilder Country = new StringBuilder("AUSTRIA");

            Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
        }
    }
}
