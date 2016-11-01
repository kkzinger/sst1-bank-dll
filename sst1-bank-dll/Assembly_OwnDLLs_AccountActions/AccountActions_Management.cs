using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support


using TransactionList = System.Collections.Generic.List<Assembly_OwnDLLs_AccountActions.TRANSACTION>;

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
        internal static extern int bankAccountStatement(uint AID, ref TRANSACTION transactionsListBegin, uint len); //---> anpassen!

        [DllImport("../../../Debug/bank_account_actions.dll")]
        //Get actual balance of AID.
        internal static extern float balancing(uint AID);

        [DllImport("../../../Debug/bank_entitycomponent.dll")]
        internal static extern int _initEntity();


        static int withDrawFromAccount(uint _AID, float _amount)
        {
            return withDraw(_AID, _amount);
        }

        static int depositToAccount(uint _AID, float _amount)
        {
            return deposit(_AID, _amount);
        }

        static int transferBetweenAccounts(uint _SrcAccountID, uint _DestAccountID, uint _OrdererCID, float _amount)
        {
            return transfer(_SrcAccountID, _DestAccountID, _OrdererCID, _amount);
        }

       


        static void Main(string[] args)
        {
            //_initEntity();
            //withDraw(1, 1.1232656f);
            //deposit(1, 1.1232656f);
            //transfer(1, 2, 1, 1.1232656f);
            //TransactionList tl = new TransactionList();
            //TRANSACTION t1 = new TRANSACTION();
            //TRANSACTION t2 = new TRANSACTION();
            //tl.Add(t1);
            //tl.Add(t2);
            //TRANSACTION thelp = tl.First();//TTTTTTTTTTTTEEEEEEEEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSSSSSTTTTTTTTTTTTTEEEEEEEEEEEEEEEEEEEEN
            //bankAccountStatement(1, ref  thelp, (uint) tl.Count());
            //balancing(1);

        }
    }
}
