using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support


//using TransactionList = System.Collections.Generic.List<Assembly_OwnDLLs_AccountActions.TRANSACTION>;

namespace Assembly_OwnDLLs_AccountActions
{
    // Transaction
    [StructLayout(LayoutKind.Sequential)]
    public struct TRANSACTION
    {
        public ulong timestamp;
        public uint sourceAID;
        public uint destinationAID;
        public uint ordererCID;
        public float amount; 

    };


    public class AccountActions_Management
    {
        [DllImport("../../../Debug/bank_account_actions.dll", CallingConvention = CallingConvention.Cdecl, SetLastError =true)]
        internal static extern int withDraw(uint AID, float amount);

        [DllImport("../../../Debug/bank_account_actions.dll")]
        internal static extern int deposit(uint AID, float amount);

        [DllImport("../../../Debug/bank_account_actions.dll")]
        //transfer money between Accounts. ordereCID has to be depositor of FromAID Account.
        internal static extern int transfer(uint FromAID, uint ToAID, uint ordererCID, float amount);

        [DllImport("../../../Debug/bank_account_actions.dll")]
        //Get a list of TRANSACTION structs which have AID eiter in FromAID or in ToAID. len has to be the length of the transactionsList Array (for TRANSACTIONS* transactionList[5] len should be 5)
        internal static extern int bankAccountStatement(uint AID, ref TRANSACTION transactionList,ref uint len); //---> anpassen!

        [DllImport("../../../Debug/bank_account_actions.dll")]
        //Get actual balance of AID.
        internal static extern int balancing(uint AID, ref float balance);

        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();


        public static int withdrawFromAccount(int AID, float amount)
        {
            if (AID <= 0) return -1; //not a valid AID
            return withDraw((uint)AID, amount);
        }

        public static int depositToAccount(int AID, float amount)
        {
            if (AID <= 0) return -1; //not a valid AID
            return deposit((uint)AID, amount);
        }

        public static int transferBetweenAccounts(int sourceAID, int destinationAID, int ordererCID, float amount)
        {
            if (sourceAID <= 0) return -1; //not a valid AID
            if (destinationAID <= 0) return -1; //not a valid AID
            if (ordererCID <= 0) return -1; //not a valid CID
            
            return transfer((uint)sourceAID, (uint)destinationAID, (uint)ordererCID, amount);
        }

        public static int getAccountBalance(int AID, ref float balance)
        {
            if (AID <= 0) return -1; //not a valid AID
            float temp = 0.0F;

            if (balancing((uint)AID, ref temp) < 0) return -1; //Account was not found

            balance = temp;
            return 0;
        }

        public static int getTransactionsByAID(int AID, ref List<TRANSACTION> transactionList)
        {
            uint length = 1;
            TRANSACTION temp = new TRANSACTION();


            //Determine size of Array that is needed for store all Transaction
            if(bankAccountStatement((uint)AID,ref temp,ref length) != 0)
            {
                return -1; //Something in Entity Dll gone terribly wrong
            }

            
            return (int)length;
        }


        static void Main(string[] args)
        {
            
        }
    }
}
