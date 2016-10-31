using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support

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
    class AccountActions_Management
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
        internal static extern int bankAccountStatement(uint AID, ref TRANSACTION transactionsList, uint len);

        [DllImport("../../../Debug/bank_account_actions.dll")]
        //Get actual balance of AID.
        internal static extern float balancing(uint AID);







        static void Main(string[] args)
        {
            withDraw(1, 1.1232656f);



        }
    }
}
