using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support

namespace Assembly_OwnDLLs_Accounts
{

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

   
    class Accounts_Management
    {
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Open(uint[] Depositors, account_t Type, currency_t CurID, float Balance);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int getOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint Close(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint addOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint removeOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint Freeze(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint Unfreeze(uint AID);

        static void Main(string[] args)
        {

            IntPtr A = IntPtr.Zero;
            try
            {
                //  http://stackoverflow.com/questions/8741879/pinvoke-how-to-marshal-for-sometype/8745154#8745154

                //4*4+20+2*2 = 40
                A = Marshal.AllocHGlobal(40);

                Console.WriteLine(_initEntity());

                uint[] dep = new uint[20] { 5,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
                Console.WriteLine(dep[0]);
                account_t type = account_t.SAVING;
                currency_t curr = currency_t.EUR;
                float bal = 25.80F;

                Console.WriteLine(Open(dep, type, curr, bal));
                Console.WriteLine(Open(dep, type, curr, bal));
                Console.WriteLine(Open(dep, type, curr, bal));
                uint[] readDeps = new uint[20];
                Console.WriteLine(getOwners(1, readDeps));
                Console.WriteLine(readDeps[0]);
                Console.WriteLine(Freeze(1));
                Console.WriteLine(Unfreeze(1));
                Console.WriteLine(Close(1));
                uint[] newDeps = new uint[5] { 10,11,12,13,14 };
                Console.WriteLine(addOwners(2, newDeps));
                uint[] delDeps = new uint[2] { 12, 3 };

                Console.WriteLine(getOwners(2, readDeps));
                Console.WriteLine(readDeps[5]);

                Console.WriteLine("--------");
                //string FirstName = "Tobias";
                //string LastName = "Mayer";
                //string Street = "Salzburger Str.";
                //string StreetNr = "17a";
                //string City = "Neumarkt";
                //string PostalCode = "5202";
                //string Country = "AUSTRIA";

                //Console.WriteLine(Create(FirstName, LastName, Street, StreetNr, City, PostalCode, Country));
                //Console.WriteLine(Read(1, C));

                //CUSTOMER C_instance = (CUSTOMER)Marshal.PtrToStructure(C, typeof(CUSTOMER));

                //Console.WriteLine(C_instance.CID);
                //Console.WriteLine(C_instance.FirstName);
                //Console.WriteLine(C_instance.LastName);
                //Console.WriteLine(C_instance.Street);
                //Console.WriteLine(C_instance.StreetNr);
                //Console.WriteLine(C_instance.City);
                //Console.WriteLine(C_instance.PostalCode);
                //Console.WriteLine(C_instance.Country);

                //Console.WriteLine(Update(C_instance.CID, "", "", "", "", "", "", "ÖSTERREICH"));
                //Console.WriteLine(Read(1, C));
                //C_instance = (CUSTOMER)Marshal.PtrToStructure(C, typeof(CUSTOMER));
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
                if (A != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(A);
                }
            }
        }
    }
}
