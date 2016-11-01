using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support


namespace Assembly_OwnDLLs_Customers
{
    // Customer Data
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct CUSTOMER
    {

        public uint CID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
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

   public class Customers_Management
    {
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();


        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern uint Create(string FirstName, string LastName, string Street, string StreetNr, string City, string PostalCode, string Country);


        [DllImport("../../../Debug/bank_customers.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int Read(uint CID, IntPtr C);

        [DllImport("../../../Debug/bank_customers.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Update(uint CID, string FirstName, string LastName, string Street, string StreetNr, string City, string PostalCode, string Country);
        
        [DllImport("../../../Debug/bank_customers.dll")]
        internal static extern int Activate(uint CID);

        [DllImport("../../../Debug/bank_customers.dll")]
        internal static extern int Deactivate(uint CID);

        public static int InitializeData()
        {
            return _initEntity();

        }

        public static CUSTOMER createCustomer(string _FirstName, string _LastName, string _Street, string _StreetNr, string _City, string _PostalCode, string _Country)
        {
            IntPtr C = IntPtr.Zero;
            try
            {
                
                //  http://stackoverflow.com/questions/8741879/pinvoke-how-to-marshal-for-sometype/8745154#8745154
                //4+7*20+1 = 145
                C = Marshal.AllocHGlobal(145);
                uint ID = Create(_FirstName, _LastName, _Street, _StreetNr, _City, _PostalCode, _Country);
                Read(ID, C);
                CUSTOMER C_instance = (CUSTOMER)Marshal.PtrToStructure(C, typeof(CUSTOMER));
                return C_instance;
            }
            finally
            {
                if (C != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(C);
                }
            }
        }

        public static void updateCustomer(ref CUSTOMER CustomerToUpdate, string _FirstName="", string _LastName="", string _Street="", string _StreetNr="", string _City="", string _PostalCode="", string _Country="")
        {
            Update(CustomerToUpdate.CID, _FirstName, _LastName, _Street, _StreetNr, _City, _PostalCode, _Country);
            //4+7*20+1 = 145
            IntPtr updatedCustomer = Marshal.AllocHGlobal(145);
            Read(CustomerToUpdate.CID, updatedCustomer);
            CustomerToUpdate = (CUSTOMER)Marshal.PtrToStructure(updatedCustomer, typeof(CUSTOMER));
        }

        public static void activateCustomer(ref CUSTOMER CustomerToActivate)
        {
            Activate(CustomerToActivate.CID);
            //4+7*20+1 = 145
            IntPtr updatedCustomer = Marshal.AllocHGlobal(145);
            Read(CustomerToActivate.CID, updatedCustomer);
            CustomerToActivate = (CUSTOMER)Marshal.PtrToStructure(updatedCustomer, typeof(CUSTOMER));
        }

        public static void deactivateCustomer(ref CUSTOMER CustomerToActivate)
        {
            Deactivate(CustomerToActivate.CID);
            //4+7*20+1 = 145
            IntPtr updatedCustomer = Marshal.AllocHGlobal(145);
            Read(CustomerToActivate.CID, updatedCustomer);
            CustomerToActivate = (CUSTOMER)Marshal.PtrToStructure(updatedCustomer, typeof(CUSTOMER));
        }

        public static int listAllCustomers()
        {
            uint CID = 0;
            IntPtr DummyCustomerPtr = Marshal.AllocHGlobal(145);
            CUSTOMER DummyCustomer = (CUSTOMER)Marshal.PtrToStructure(DummyCustomerPtr, typeof(CUSTOMER));
            while (Read(CID, DummyCustomerPtr)==0)
            {
                CID++;
                DummyCustomer = (CUSTOMER)Marshal.PtrToStructure(DummyCustomerPtr, typeof(CUSTOMER));
                Console.WriteLine(DummyCustomer.CID);
                Console.WriteLine(DummyCustomer.FirstName);
                Console.WriteLine(DummyCustomer.LastName);
                Console.WriteLine(DummyCustomer.Street);
                Console.WriteLine(DummyCustomer.StreetNr);
                Console.WriteLine(DummyCustomer.PostalCode);
                Console.WriteLine(DummyCustomer.City);
                Console.WriteLine(DummyCustomer.Country);
            }
            Marshal.FreeHGlobal(DummyCustomerPtr);
            if (CID == 0) return -1;
            return 0;
        }



        static void Main(string[] args)
        {
            InitializeData();
            CUSTOMER Kunde = createCustomer("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
            Console.WriteLine(Kunde.LastName);
            updateCustomer(ref Kunde, "", "Huber");
            Console.WriteLine(Kunde.LastName);
            Console.WriteLine(Kunde.Active);
            deactivateCustomer(ref Kunde);
            Console.WriteLine(Kunde.Active);
            activateCustomer(ref Kunde);
            Console.WriteLine(Kunde.Active);

        }
    }
}
