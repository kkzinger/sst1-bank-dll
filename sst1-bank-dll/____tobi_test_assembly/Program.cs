using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
namespace ____tobi_test_assembly
{
    //account_t enum
    public enum account_t
    {
        SAVING,
        CREDIT
    }

    public enum currency_t
    {
        EUR,
        USD,
        GBP,
        JPY
    }
    
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

    // Account Data
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ACCOUNT
    {

        public uint AID;
        public account_t type;
        public currency_t currency;
        public float balance;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public uint[] depositors;
        public byte unfrozen;
        public byte open;
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

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Open(uint[] Depositors, account_t Type, currency_t CurID, float Balance);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int getOwners(uint AID, uint[] Depositors);

        static void Main(string[] args)
        {
            IntPtr C = IntPtr.Zero;
            IntPtr A = IntPtr.Zero;
            try
            {
                //  http://stackoverflow.com/questions/8741879/pinvoke-how-to-marshal-for-sometype/8745154#8745154
                //4+7*20+1 = 145
                 C = Marshal.AllocHGlobal(145);

                //4*4+20+2*2 = 40
                A = Marshal.AllocHGlobal(40);

                Console.WriteLine(_initEntity());

                uint[] dep = new uint[] { 5 };
                account_t type = account_t.SAVING;
                currency_t curr = currency_t.EUR;
                float bal = 25.80F;

                Console.WriteLine(Open(dep, type, curr, bal));
                uint[] readDeps = new uint[20];
                Console.WriteLine(getOwners(0, readDeps));
                Console.WriteLine(readDeps);

                Console.WriteLine("--------");
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
                Console.WriteLine(Read(1, C));
                C_instance = (CUSTOMER)Marshal.PtrToStructure(C, typeof(CUSTOMER));
                Console.WriteLine(C_instance.CID);
                Console.WriteLine(C_instance.FirstName);
                Console.WriteLine(C_instance.LastName);
                Console.WriteLine(C_instance.Street);
                Console.WriteLine(C_instance.StreetNr);
                Console.WriteLine(C_instance.City);
                Console.WriteLine(C_instance.PostalCode);
                Console.WriteLine(C_instance.Country);

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
