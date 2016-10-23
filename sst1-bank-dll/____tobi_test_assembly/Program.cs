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
    public struct CUSTOMER
    {

        public uint CID;
        public string FirstName;
        public string LastName;
        public string Street;
        public string StreetNr;
        public string City;
        public string PostalCode;
        public string Country;
        public byte Active;


    };

    class Program
    {
        //[DllImport("../../../Release/bank_currency.dll")]
        //public static extern int bar();

        CUSTOMER C = new CUSTOMER();

        //[DllImport("../../../Release/bank_entitycomponent.dll")]
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        public static extern int _initEntity();

        //   [DllImport("../../../Release/bank_customers.dll",CallingConvention = CallingConvention.Cdecl)]
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Create( string FirstName, string LastName, string Street, string StreetNr, string City, string PostalCode, string Country);
        //     [DllImport("../../../Release/bank_customers.dll")]
      
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl/*, CharSet =CharSet.Ansi*/)]
        public static extern int Read(uint CID,  ref CUSTOMER C);

        static void Main(string[] args)
        {
  
            Console.WriteLine(_initEntity());
            CUSTOMER C = new CUSTOMER();

            string  FirstName =   "Tobias";
            string  LastName =   "Mayer";
            string  Street =   "Salzburger Str.";
            string  StreetNr =   "17a";
            string  City =   "Neumarkt";
            string  PostalCode =   "5202";
            string  Country =   "AUSTRIA";

            Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
            Console.WriteLine(Read(1,  ref C));
            //Console.WriteLine(C.CID);
            //Console.WriteLine(C.FirstName);
           
        }
    }
}
