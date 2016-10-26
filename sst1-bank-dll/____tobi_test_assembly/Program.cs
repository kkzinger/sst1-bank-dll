using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
namespace ____tobi_test_assembly
{
    // Customer Data
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CUSTOMER
    {

        public uint CID;
        [MarshalAs(UnmanagedType.LPStr)] public String FirstName;
        [MarshalAs(UnmanagedType.LPStr)] public String LastName;
        [MarshalAs(UnmanagedType.LPStr)] public String Street;
        [MarshalAs(UnmanagedType.LPStr)] public String StreetNr;
        [MarshalAs(UnmanagedType.LPStr)] public String City;
        [MarshalAs(UnmanagedType.LPStr)] public String PostalCode;
        [MarshalAs(UnmanagedType.LPStr)] public String Country;
         public Byte Active;


    }

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
        public static extern int Create( String FirstName, String LastName, String Street, String StreetNr, String City, String PostalCode, String Country);
        //     [DllImport("../../../Release/bank_customers.dll")]
      
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl/*, CharSet =CharSet.Ansi*/)]
        internal static extern int Read(uint CID, [In, Out]  ref CUSTOMER C);

        static void Main(String[] args)
        {
  
            Console.WriteLine(_initEntity());
            CUSTOMER C = new CUSTOMER();

            String  FirstName =   "Tobias";
            String  LastName =   "Mayer";
            String  Street =   "Salzburger Str.";
            String  StreetNr =   "17a";
            String  City =   "Neumarkt";
            String  PostalCode =   "5202";
            String  Country =   "AUSTRIA";

            Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
            Console.WriteLine(Read(1,   ref C));
            Console.WriteLine(C.CID);
            Console.WriteLine(C.FirstName);
           
        }
    }
}
