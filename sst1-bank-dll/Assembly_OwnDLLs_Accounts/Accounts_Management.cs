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


    public class Accounts_Management
    {
        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Open(uint[] Depositors, [param: MarshalAs(UnmanagedType.U4)] account_t Type, currency_t CurID, float Balance);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int getOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Close(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int addOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int removeOwners(uint AID, uint[] Depositors);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Freeze(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int Unfreeze(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int IsUnFrozen(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int IsOpen(uint AID);

        [DllImport("../../../Debug/bank_accounts.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern int getType(uint AID, ref int type);

        

        public static int openAccount(uint[] depositors, account_t accountType, currency_t currencyID, float balance)
        {
            uint[] cleanDepositors = new uint[20];
            int AID = 0;

            if (depositors.Length > 20)
            {
                return -1; //To many depositors for Account 
            }

            for (uint i = 0; i < depositors.Length - 1; i++)
            {
                cleanDepositors[i] = depositors[i];
            }

            //Open account via DLL and get AID of created account.
            AID = Open(cleanDepositors, accountType, currencyID, balance);

            if (AID < 0) return -1; //creation of account was not successful

            return AID;
        }

        public static int closeAccount(int AID)
        {
            if (AID <= 0) return -1; //not a valid AID
            if (Close((uint)AID) < 0) return -1; //Account could not be closed because it does not exist.
            return 0;
        }

        public static int getAccountDepositors(int AID,  uint[] depositors)
        {
            if (AID <= 0) return -1; //not a valid AID
            uint[] readDepositors = new uint[20];

            if (depositors.Length < 20) return -2; //Length of provided depositors array is not big enough.
            if (getOwners((uint)AID, readDepositors) < 0) return -1; //Depositors not available because no account with this AID exists.

            //Make Shallow copy of depositors
            Array.Copy(readDepositors, depositors, readDepositors.Length);
            return 0;
        }

        public static int addAccountDepositors(int AID, uint[] depositors)
        {
            if (AID <= 0) return -1; //not a valid AID
            uint[] cleanDepositors = new uint[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (depositors.Length > 20)
            {
                return -1; //To many depositors for Account 
            }

            for (uint i = 0; i < depositors.Length; i++)
            {
                cleanDepositors[i] = depositors[i];
            }

            if (addOwners((uint)AID, cleanDepositors) < 0) return -1; //Owners couldn't be added because Account does not exist.
            return 0;
        }

        public static int removeAccountDepositors(int AID, uint[] depositors)
        {
            if (AID <= 0) return -1; //not a valid AID
            uint[] cleanDepositors = new uint[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            if (depositors.Length > 20)
            {
                return -1; //To many depositors for Account 
            }

            for (uint i = 0; i < depositors.Length; i++)
            {
                cleanDepositors[i] = depositors[i];
            }

            if (removeOwners((uint)AID, cleanDepositors) < 0) return -1; //Owners couldn't be removed because Account does not exist.
            return 0;
        }

        public static int freezeAccount(int AID)
        {
            if (AID <= 0) return -1; //not a valid AID
            if (Freeze((uint)AID) < 0) return -1; //Account could not be freezed because it does not exist.
            return 0;
        }

        public static int unfreezeAccount(int AID)
        {
            if (AID <= 0) return -1; //not a valid AID
            if (Unfreeze((uint)AID) < 0) return -1; //Account could not be unfreezed because it does not exist.
            return 0;
        }

        public static int getAIDsbyCID(int CID, uint[] listOfAIDs)
        {
            if (CID <= 0) return -1; //not a valid AID
            if (listOfAIDs.Length < 99) return -2; //list is not long enough has to be at least uint[100]

            int AID = 1;
            uint cnt = 0;
            uint[] readDeps = new uint[20];

            while ((getAccountDepositors(AID, readDeps) == 0))
            {
                
                if (readDeps.Contains((uint)CID))
                {
                    //Console.WriteLine(AID);
                    listOfAIDs[cnt] = (uint)AID;
                    cnt++;
                }
                
                AID++;
            }
            return 0;
        }

        public static int isAccountUnfrozen(int AID)
        {
            if (AID <= 0) return -1; //not a valid AID
            int result = 0;
            result = IsUnFrozen((uint)AID);

            if(result == 0)
            {
                return 0; //Account is Frozen
            }else if(result == 1)
            {
                return 1; //Account is _not_ Frozen
            }else
            {
                return -1; //Account was not found
            }

        }

        public static int isAccountOpen(int AID)
        {
            if (AID <= 0) return -1; //not a valid AID
            int result = -1;
            result = IsOpen((uint)AID);

            if (result == 1)
            {
                return 1; //Account is Open
            }
            else if(result == 0)
            {
                return 0; //Account is _not_ Open
            }else
            {
                return -1; //Account was not found
            }
            
        }

        public static int getAccountType(int AID, ref account_t type)
        {
            if (AID <= 0) return -1; //not a valid AID
            int temp = -1;
            if(getType((uint)AID,ref temp) != 0) return -1; //Account was not found

            type = (account_t)temp;
            return 0;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(_initEntity());

            int AID;
            int CID = 3;

            uint[] dep = new uint[20] { 5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            uint[] readDeps = new uint[20];
            uint[] addDeps = new uint[] { 10, 11, 12, 13 };
            uint[] removeDeps = new uint[] { 3, 12 };
            uint[] AIDlist = new uint[100];
            account_t type = account_t.SAVING;
            currency_t curr = currency_t.EUR;
            float bal = 25.80F;

            AID = openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            AID = openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            AID = openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            //Read CIDs that are Depositors off Account and write to console
            if (getAccountDepositors(AID, readDeps) == 0)
            {
                Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
                for (uint i = 0; i < readDeps.Length; i++)
                {
                    //when a zero in Depositor Array is reached no further CIDs will be found.
                    if (readDeps[i] == 0)
                    {
                        Console.Write("\n");
                        break;
                    }

                    Console.Write("{0} ", readDeps[i]);
                }
            }

            Console.WriteLine("Add Customers as Depositors");
            Console.WriteLine("ergebnis add: {0}", addAccountDepositors(AID, addDeps));

            //Read CIDs that are Depositors off Account and write to console
            if (getAccountDepositors(AID, readDeps) == 0)
            {
                Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
                for (uint i = 0; i < readDeps.Length; i++)
                {
                    //when a zero in Depositor Array is reached no further CIDs will be found.
                    if (readDeps[i] == 0)
                    {
                        Console.Write("\n");
                        break;
                    }

                    Console.Write("{0} ", readDeps[i]);
                }
            }

            Console.WriteLine("Remove Customers as Depositors");
            removeAccountDepositors(AID, removeDeps);

            //Read CIDs that are Depositors off Account and write to console
            if (getAccountDepositors(AID, readDeps) == 0)
            {
                Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
                for (uint i = 0; i < readDeps.Length; i++)
                {
                    //when a zero in Depositor Array is reached no further CIDs will be found.
                    if (readDeps[i] == 0)
                    {
                        Console.Write("\n");
                        break;
                    }

                    Console.Write("{0} ", readDeps[i]);
                }
            }

            Console.WriteLine("Search for all Accounts where Customer {0} is Depositor", CID);
            getAIDsbyCID(CID, AIDlist);

            for (uint i = 0; i < AIDlist.Length; i++)
            {
                if (AIDlist[i] == 0)
                {
                    Console.Write("\n");
                    break;
                }

                Console.Write("{0} ", AIDlist[i]);

            }

            Console.WriteLine("Is Account {1} Unfrozen? -- {0}", isAccountUnfrozen(AID), AID);
            freezeAccount(AID);
            Console.WriteLine("Is Account {1} Unfrozen ? -- {0}", isAccountUnfrozen(AID), AID);
            unfreezeAccount(AID);
            Console.WriteLine("Is Account {1} Unfrozen ? -- {0}", isAccountUnfrozen(AID), AID);

            Console.WriteLine("Is Account {1} Open ? -- {0}", isAccountOpen(AID), AID);
            closeAccount(AID);
            Console.WriteLine("Is Account {1} Open ? -- {0}", isAccountOpen(AID), AID);

            account_t readType = account_t.CREDIT;
            Console.WriteLine(readType);
            Console.WriteLine(getAccountType(AID, ref readType));
            Console.WriteLine(readType);
        }
    }
}
