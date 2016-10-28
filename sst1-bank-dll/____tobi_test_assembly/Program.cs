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
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst =20)]
        public string FirstName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string LastName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Street;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string StreetNr;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string City;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string PostalCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Country;
         public byte Active;


    }

    class Program
    {
        //[DllImport("../../../Release/bank_currency.dll")]
        //public static extern int bar();
      



        //[DllImport("../../../Release/bank_entitycomponent.dll")]
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        public static extern int _initEntity();

        //   [DllImport("../../../Release/bank_customers.dll",CallingConvention = CallingConvention.Cdecl)]
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Create( String FirstName, String LastName, String Street, String StreetNr, String City, String PostalCode, String Country);
        //     [DllImport("../../../Release/bank_customers.dll")]
      
        [DllImport("../../../Debug/bank_customers.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int Read(uint CID, IntPtr  C);

        static void Main(String[] args)
        {
            IntPtr C = IntPtr.Zero;
            try
            {
                //  http://stackoverflow.com/questions/8741879/pinvoke-how-to-marshal-for-sometype/8745154#8745154
                //4+7*20+1 = 145
                 C = Marshal.AllocHGlobal(145);

                Console.WriteLine(_initEntity());


                string FirstName = "Tobias";
                string LastName = "Mayer";
                string Street = "Salzburger Str.";
                string StreetNr = "17a";
                string City = "Neumarkt";
                string PostalCode = "5202";
                string Country = "AUSTRIA";

                Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
                Console.WriteLine(Read(1, C));

                CUSTOMER C_instance = (CUSTOMER)Marshal.PtrToStructure(C, typeof(CUSTOMER));

                Console.WriteLine(C_instance.CID);
                Console.WriteLine(C_instance.FirstName);

             }
            finally
            {
                if (C != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(C);
                }
            }
            }
    }
}
