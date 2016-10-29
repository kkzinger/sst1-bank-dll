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
     
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();

     
        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Create(string FirstName, string LastName, string Street, string StreetNr, string City, string PostalCode, string Country);
      
      
        [DllImport("../../../Debug/bank_customers.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int Read(uint CID, IntPtr  C);

        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Update(uint CID, string FirstName, string LastName, string Street, string StreetNr, string City, string PostalCode, string Country);

        static void Main(string[] args)
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
                Console.WriteLine(C_instance.LastName);
                Console.WriteLine(C_instance.Street);
                Console.WriteLine(C_instance.StreetNr);
                Console.WriteLine(C_instance.City);
                Console.WriteLine(C_instance.PostalCode);
                Console.WriteLine(C_instance.Country);

                Console.WriteLine(Update(C_instance.CID, "", "", "", "", "", "", "ÖSTERREICH"));

                //Console.WriteLine(C_instance.CID);
                //Console.WriteLine(C_instance.FirstName);
                //Console.WriteLine(C_instance.LastName);
                //Console.WriteLine(C_instance.Street);
                //Console.WriteLine(C_instance.StreetNr);
                //Console.WriteLine(C_instance.City);
                //Console.WriteLine(C_instance.PostalCode);
                //Console.WriteLine(C_instance.Country);

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
