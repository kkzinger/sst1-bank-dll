using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assembly_OwnDLLs_Accounts;
using Assembly_OwnDLLs_Customers;
using Assembly_OwnDLLs_AccountActions;


namespace test_Assembly_OwnDLLs_AccountActions
{
    class test_AccountActions
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Customers_Management.InitializeData());

            int AID;
            //int CID = 3;

            uint[] dep = new uint[20] { 5, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            float readBal = 0.0F;
            //uint[] readDeps = new uint[20];
            //uint[] addDeps = new uint[] { 10, 11, 12, 13 };
            //uint[] removeDeps = new uint[] { 3, 12 };
            //uint[] AIDlist = new uint[100];
            account_t type = account_t.SAVING;
            currency_t curr = currency_t.EUR;
            float bal = 1000.00F;

            AID = Accounts_Management.openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            AID = Accounts_Management.openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            AID = Accounts_Management.openAccount(dep, type, curr, bal);
            if (AID > 0) Console.WriteLine("Account created AID-> {0}", AID);

            //Get account balance
            AccountActions_Management.getAccountBalance(AID, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID, readBal);

            //Deposite
            AccountActions_Management.depositToAccount(AID, 100.5F);
            AccountActions_Management.getAccountBalance(AID, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID, readBal);

            //Withdraw
            AccountActions_Management.withdrawFromAccount(AID, 500.5F);
            AccountActions_Management.getAccountBalance(AID, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID, readBal);

            //Transfer
            AccountActions_Management.getAccountBalance(AID, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID, readBal);
            AccountActions_Management.getAccountBalance(AID-1, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID-1, readBal);

            AccountActions_Management.transferBetweenAccounts(AID, AID - 1, 6, 600); //Problem 6 ist nicht berechtigt. transfer geht aber

            AccountActions_Management.getAccountBalance(AID, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID, readBal);
            AccountActions_Management.getAccountBalance(AID - 1, ref readBal);
            Console.WriteLine("Balance of AID {0} is {1}", AID-1, readBal);

            ////Read CIDs that are Depositors off Account and write to console
            //if (getAccountDepositors(AID, readDeps) == 0)
            //{
            //    Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
            //    for (uint i = 0; i < readDeps.Length; i++)
            //    {
            //        //when a zero in Depositor Array is reached no further CIDs will be found.
            //        if (readDeps[i] == 0)
            //        {
            //            Console.Write("\n");
            //            break;
            //        }

            //        Console.Write("{0} ", readDeps[i]);
            //    }
            //}

            //Console.WriteLine("Add Customers as Depositors");
            //Console.WriteLine("ergebnis add: {0}",addAccountDepositors(AID, addDeps));

            ////Read CIDs that are Depositors off Account and write to console
            //if (getAccountDepositors(AID, readDeps) == 0)
            //{
            //    Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
            //    for (uint i = 0; i < readDeps.Length; i++)
            //    {
            //        //when a zero in Depositor Array is reached no further CIDs will be found.
            //        if (readDeps[i] == 0)
            //        {
            //            Console.Write("\n");
            //            break;
            //        }

            //        Console.Write("{0} ", readDeps[i]);
            //    }
            //}

            //Console.WriteLine("Remove Customers as Depositors");
            //removeAccountDepositors(AID, removeDeps);

            ////Read CIDs that are Depositors off Account and write to console
            //if (getAccountDepositors(AID, readDeps) == 0)
            //{
            //    Console.WriteLine("Following Customers are Depositors of Account {0}", AID);
            //    for (uint i = 0; i < readDeps.Length; i++)
            //    {
            //        //when a zero in Depositor Array is reached no further CIDs will be found.
            //        if (readDeps[i] == 0)
            //        {
            //            Console.Write("\n");
            //            break;
            //        }

            //        Console.Write("{0} ", readDeps[i]);
            //    }
            //}

            //Console.WriteLine("Search for all Accounts where Customer {0} is Depositor", CID);
            //getAIDsbyCID(CID, AIDlist);

            //for(uint i = 0;i < AIDlist.Length; i++)
            //{
            //    if (AIDlist[i] == 0)
            //    {
            //        Console.Write("\n");
            //        break;
            //    }

            //    Console.Write("{0} ",AIDlist[i]);

            //}

            //freezeAccount(AID);

            //unfreezeAccount(AID);

            //closeAccount(AID);

        }
    }
}
