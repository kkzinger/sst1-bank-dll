using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
namespace ____tobi_test_assembly
{
    // Customer Data
    [StructLayout(LayoutKind.Sequential)]
    public class CUSTOMER
    {
        public uint CID;
        public String FirstName;
        public String LastName;
        public String Street;
        public String StreetNr;
        public String City;
        public String PostalCode;
        public String Country;
        public Byte Active;
    };

    class Program
    {
        //[DllImport("../../../Release/bank_currency.dll")]
        //public static extern int bar();

        [DllImport("../../../Release/bank_entitycomponent.dll")]
        public static extern int _initEntity();

        [DllImport("../../../Release/bank_customers.dll")]
        public static extern int Create(StringBuilder FirstName, StringBuilder LastName, StringBuilder Street, StringBuilder StreetNr, StringBuilder City, StringBuilder PostalCode, StringBuilder Country);
        [DllImport("../../../Release/bank_customers.dll")]
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
