using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembly_OwnDLLs_Customers;
using Assembly_OwnDLLs_Accounts;
using Assembly_OwnDLLs_AccountActions;

namespace BankApplication
{
    class BankApplication
    {
        static void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Enter 'customer' for the Customer Management section, enter 'account' for the Account Management section...");
            string cmd = Console.ReadLine();
            if (cmd == "customer")
            {
                DisplayCUSTOMERManagementSection();
            }
            else if (cmd == "account")
            {
                DisplayACCOUNTManagementSection();
            }
            else
            {
                Console.WriteLine("Command not found!");
                DisplayMainMenu();
            }
         }

        static void DisplayCUSTOMERManagementSection()
        {
            Console.WriteLine();
            Console.WriteLine("CUSTOMER Management Section:");
            int number_of_customers = Customers_Management.listAllCustomers();
            if (number_of_customers==-1)
            {
                Console.WriteLine("No customers found!");
                Console.WriteLine("Enter 'create' to get to the Create CUSTOMER Section, enter 'back' to get back to the Main Menu");
                string cmd = Console.ReadLine();
                if (cmd == "create")
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if(cmd == "back")
                {
                    DisplayMainMenu();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayCUSTOMERManagementSection();
                }
            }
            else
            {
                Console.WriteLine(number_of_customers+" customers found!");
                Console.WriteLine("Enter 'back' to get back to the Main Menu, enter 'create' to get to the Create CUSTOMER Section, enter 'modify' to get to the Modify CUSTOMER Section, enter 'deactivate' to get to the Deactivate CUSTOMER Section, enter 'reactivate' to get to the Re-activate CUSTOMER Section ");
                string cmd = Console.ReadLine();
                if (cmd == "back")
                {
                    DisplayMainMenu();
                }
                else if (cmd == "create")
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if(cmd == "modify")
                {
                    DisplayModifyCUSTOMERSection(0);
                }
                else if(cmd == "deactivate")
                {
                    DisplayDeactivateCUSTOMERSection();
                }
                else if (cmd == "reactivate")
                {
                    DisplayReactivateCUSTOMERSection();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayCUSTOMERManagementSection();
                }
            }
        }

        static void DisplayCreateCUSTOMERSection()
        {
            string FirstName = "";
            string LastName = "";
            string Street = "";
            string StreetNr = "";
            string PostalCode = "";
            string City = "";
            string Country = "";
            while (FirstName.Length < 2)
            {
                Console.WriteLine("Insert the First Name of the CUSTOMER (at least 2 characters):");
                FirstName = Console.ReadLine();
            }
            while (LastName.Length < 2)
            {
                Console.WriteLine("Insert the Last Name of the CUSTOMER (at least 2 characters):");
                LastName = Console.ReadLine();
            }
            while (Street.Length < 3)
            {
                Console.WriteLine("Insert the Street Name of the CUSTOMER (at least 3 characters):");
                Street = Console.ReadLine();
            }
            while (StreetNr.Length < 1)
            {
                Console.WriteLine("Insert the Street Number of the CUSTOMER (at least 1 character):");
                StreetNr = Console.ReadLine();
            }
            while (PostalCode.Length < 4)
            {
                Console.WriteLine("Insert the Postal Code of the CUSTOMER (at least 4 characters):");
                PostalCode = Console.ReadLine();
            }
            while (City.Length < 4)
            {
                Console.WriteLine("Insert the City of the CUSTOMER (at least 4 characters):");
                City = Console.ReadLine();
            }
            while (Country.Length < 3)
            {
                Console.WriteLine("Insert the Country of the CUSTOMER (at least 3 characters):");
                Country = Console.ReadLine();
            }

            CUSTOMER C = Customers_Management.createCustomer(FirstName, LastName, Street, StreetNr, City, PostalCode, Country);
            Console.WriteLine("The CUSTOMER...");
            Console.WriteLine("ID: " + C.CID);
            Console.WriteLine("First Name: " + C.FirstName);
            Console.WriteLine("Last Name: " + C.LastName);
            Console.WriteLine("Street Name: " + C.Street);
            Console.WriteLine("Street Number: " + C.StreetNr);
            Console.WriteLine("City: " + C.City);
            Console.WriteLine("Postal Code: " + C.PostalCode);
            Console.WriteLine("Country: " + C.Country);
            Console.WriteLine("...has been created!");
            DisplayCUSTOMERManagementSection();
        }


        static void DisplayModifyCUSTOMERSection(uint _CID)
        {
            uint CID=_CID;
            string CID_str = "t";
            
            if (CID == 0)
            {
                while (!uint.TryParse(CID_str, out CID))
                {
                    
                    Console.WriteLine("Insert the ID of the CUSTOMER you want to modify:");
                    CID_str = Console.ReadLine();
                   

                }
            }
            else
            {
                CID_str = CID.ToString();
            }
            
            CID = uint.Parse(CID_str);
            CUSTOMER CustomerToModify = Customers_Management.getCustomerByCID(CID);
            if ((CustomerToModify.CID == 0)||(CustomerToModify.Active==0))
            {
                Console.WriteLine("No (active) CUSTOMER found with the ID " + CID);
                DisplayModifyCUSTOMERSection(0);
            }
            else
            {
                Console.WriteLine("The CUSTOMER with the ID " + CID + " has the following data:");
                Console.WriteLine("First Name (1): " + CustomerToModify.FirstName);
                Console.WriteLine("Last Name (2): " + CustomerToModify.LastName);
                Console.WriteLine("Street Name (3): " + CustomerToModify.Street);
                Console.WriteLine("Street Number (4): " + CustomerToModify.StreetNr);
                Console.WriteLine("Postal Code (5): " + CustomerToModify.PostalCode);
                Console.WriteLine("City (6): " + CustomerToModify.City);
                Console.WriteLine("Country (7): " + CustomerToModify.Country);
                Console.WriteLine("Enter 'leave' to leave the Modify CUSTOMER Section, enter the corresponding number to modify the values:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if (cmd == "1")
                {
                    string FirstName = "";
                     while (FirstName.Length < 2)
                    {
                        Console.WriteLine("Insert the First Name of the CUSTOMER (at least 2 characters):");
                        FirstName = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, FirstName);

                    DisplayModifyCUSTOMERSection(CID);
                 

                }
                else if (cmd == "2")
                {
                     string LastName = "";
             
                    while (LastName.Length < 2)
                    {
                        Console.WriteLine("Insert the Last Name of the CUSTOMER (at least 2 characters):");
                        LastName = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", LastName);

                    DisplayModifyCUSTOMERSection(CID);

                }
                else if (cmd == "3")
                {
                   
                    string Street = "";
                 
                    while (Street.Length < 3)
                    {
                        Console.WriteLine("Insert the Street Name of the CUSTOMER (at least 3 characters):");
                        Street = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", "", Street);

                    DisplayModifyCUSTOMERSection(CID);

                }
                else if (cmd == "4")
                {

                    string StreetNr = "";
                  
                    while (StreetNr.Length < 1)
                    {
                        Console.WriteLine("Insert the Street Number of the CUSTOMER (at least 1 character):");
                        StreetNr = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", "", "", StreetNr);

                    DisplayModifyCUSTOMERSection(CID);

                }
                else if (cmd == "5")
                {

                    string PostalCode = "";
                    
               
                    while (PostalCode.Length < 4)
                    {
                        Console.WriteLine("Insert the Postal Code of the CUSTOMER (at least 4 characters):");
                        PostalCode = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", "", "", "","", PostalCode);

                    DisplayModifyCUSTOMERSection(CID);

                }
                else if (cmd == "6")
                {
   
                    string City = "";
                  
                    while (City.Length < 4)
                    {
                        Console.WriteLine("Insert the City of the CUSTOMER (at least 4 characters):");
                        City = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", "", "", "", City);

                    DisplayModifyCUSTOMERSection(CID);
                }
                else if (cmd == "7")
                {

                    string Country = "";
              
                    while (Country.Length < 3)
                    {
                        Console.WriteLine("Insert the Country of the CUSTOMER (at least 3 characters):");
                        Country = Console.ReadLine();
                    }
                    Customers_Management.updateCustomer(ref CustomerToModify, "", "", "", "", "","",Country);

                    DisplayModifyCUSTOMERSection(CID);
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayModifyCUSTOMERSection(CID);
                }
            }          
           
        }

        static void DisplayDeactivateCUSTOMERSection()
        {
            uint CID = 0;
            string CID_str = "t";


                while (!uint.TryParse(CID_str, out CID))
                {

                    Console.WriteLine("Insert the ID of the CUSTOMER you want to deactivate (enter '0' to cancel):");
                    CID_str = Console.ReadLine();
                    if (CID_str == "0")
                    {

                        DisplayCUSTOMERManagementSection();
                    }
            }
            CID = uint.Parse(CID_str);
            CUSTOMER CustomerToDeactivate = Customers_Management.getCustomerByCID(CID);
            if ((CustomerToDeactivate.CID == 0) || (CustomerToDeactivate.Active == 0))
            {
                Console.WriteLine("No (active) CUSTOMER found with the ID " + CID);        
                    DisplayDeactivateCUSTOMERSection();
               
            }
            else
            {
                Console.WriteLine("The CUSTOMER with the ID " + CID + " has the following data:");
                Console.WriteLine("First Name: " + CustomerToDeactivate.FirstName);
                Console.WriteLine("Last Name: " + CustomerToDeactivate.LastName);
                Console.WriteLine("Street Name: " + CustomerToDeactivate.Street);
                Console.WriteLine("Street Number: " + CustomerToDeactivate.StreetNr);
                Console.WriteLine("City: " + CustomerToDeactivate.City);
                Console.WriteLine("Postal Code: " + CustomerToDeactivate.PostalCode);
                Console.WriteLine("Country: " + CustomerToDeactivate.Country);
                Console.WriteLine("Enter 'leave' to leave the Deactivate CUSTOMER Section, enter '1' to deactivate the CUSTOMER:");
                string cmd = Console.ReadLine();
                if (cmd == "leave")
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if (cmd == "1")
                {
                    Customers_Management.deactivateCustomer(ref CustomerToDeactivate);
                    DisplayDeactivateCUSTOMERSection();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayDeactivateCUSTOMERSection();
                }
              }
        }

        static void DisplayReactivateCUSTOMERSection()
        {
            uint CID = 0;
            string CID_str = "t";


            while (!uint.TryParse(CID_str, out CID))
            {

                Console.WriteLine("Insert the ID of the CUSTOMER you want to re-activate (enter '0' to cancel):");
                CID_str = Console.ReadLine();
                if (CID_str == "0")
                {
                  
                    DisplayCUSTOMERManagementSection();
                }

            }
            CID = uint.Parse(CID_str);
            CUSTOMER CustomerToDeactivate = Customers_Management.getCustomerByCID(CID);
            if ((CustomerToDeactivate.CID == 0) || (CustomerToDeactivate.Active == 1))
            {
                Console.WriteLine("No (deactivated) CUSTOMER found with the ID " + CID);
                DisplayReactivateCUSTOMERSection();
            }
            else
            {
                Console.WriteLine("The CUSTOMER with the ID " + CID + " has the following data:");
                Console.WriteLine("First Name: " + CustomerToDeactivate.FirstName);
                Console.WriteLine("Last Name: " + CustomerToDeactivate.LastName);
                Console.WriteLine("Street Name: " + CustomerToDeactivate.Street);
                Console.WriteLine("Street Number: " + CustomerToDeactivate.StreetNr);
                Console.WriteLine("City: " + CustomerToDeactivate.City);
                Console.WriteLine("Postal Code: " + CustomerToDeactivate.PostalCode);
                Console.WriteLine("Country: " + CustomerToDeactivate.Country);
                Console.WriteLine("Enter 'leave' to leave the Deactivate CUSTOMER Section, enter '1' to re.activate the CUSTOMER:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if (cmd == "1")
                {
                    Customers_Management.activateCustomer(ref CustomerToDeactivate);
                    DisplayReactivateCUSTOMERSection();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayReactivateCUSTOMERSection();
                }
            }
        }
        static void DisplayACCOUNTManagementSection()
        {
            Console.WriteLine();
            Console.WriteLine("ACCOUNT Management Section:");
            int number_of_customers = Customers_Management.listAllCustomers();
            if (number_of_customers == -1)
            {
                Console.WriteLine("No customers found, create a customer first!");
                Console.WriteLine("Enter 'create' to get to the Create CUSTOMER Section, enter 'back' to get back to the Main Menu");
                string cmd = Console.ReadLine();
                if (cmd == "create")
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if (cmd == "back")
                {
                    DisplayMainMenu();
                }
            }
            else
            {
                
                int number_of_accounts_transable = 0;
                int number_of_frozen_accounts = 0;
                Accounts_Management.countTransferableAIDs(ref number_of_accounts_transable);
                Accounts_Management.countFrozenAIDs(ref number_of_frozen_accounts);
                if((number_of_accounts_transable == 0)&&(number_of_frozen_accounts==0))
                {
                    Console.WriteLine("No accounts found!");
               
                    Console.WriteLine("Enter 'open' to get to the Open ACCOUNT Section, enter 'back' to get back to the Main Menu");
                    string cmd = Console.ReadLine();
                    if (cmd == "open")
                    {
                        DisplayOpenACCOUNTSection();
                    }
                    else if (cmd == "back")
                    {
                        DisplayMainMenu();
                    }
                }
                else
                {
                    int accounts = number_of_accounts_transable + number_of_frozen_accounts;
                    Console.WriteLine(accounts + " accounts found!");
                    Console.WriteLine("Enter 'back' to get back to the Main Menu, enter 'open' to get to the Open ACCOUNT Section, enter 'freeze' to get to the Freeze ACCOUNT Section, enter 'unfreeze' to get to the Unfreeze ACCOUNT Section, enter 'c' to get to the Close ACCOUNT Section, enter 'transfer' to get to the Transfer ACCOUNT Section ");
                    string cmd = Console.ReadLine();
                    if (cmd == "back")
                    {
                        DisplayMainMenu();
                    }
                    else if (cmd == "open")
                    {
                        DisplayOpenACCOUNTSection();
                    }
                    else if(cmd=="freeze")
                    {
                        DisplayFreezeACCOUNTSection();
                    }
                    else if (cmd == "unfreeze")
                    {
                        DisplayUnfreezeACCOUNTSection();
                    }
                    else if (cmd == "close")
                    {
                        DisplayCloseACCOUNTSection();
                    }
                    else if(cmd=="transfer")
                    {
                        DisplayTransferACCOUNTSection();
                    }

                }

            }
        }

        static void DisplayOpenACCOUNTSection()
        {
            uint CID = 0;
            string CID_str = "t";

            while (!uint.TryParse(CID_str, out CID))
            {

                Console.WriteLine("Insert the ID of the CUSTOMER you want to open an account with:");
                CID_str = Console.ReadLine();

            }
            CID = uint.Parse(CID_str);
            CUSTOMER CustomerAsDepositor = Customers_Management.getCustomerByCID(CID);
            if ((CustomerAsDepositor.CID == 0) || (CustomerAsDepositor.Active == 0))
            {
                Console.WriteLine("No (active) CUSTOMER found with the ID " + CID);
                DisplayACCOUNTManagementSection();
            }
            uint[] depositor = new uint[20] { CID, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            Console.WriteLine("Enter '1' to open an SAVING account, '2' for CREDIT:");
            account_t type = account_t.CREDIT;
            string cmd = Console.ReadLine();
            if (cmd == "1")
            {
                type = account_t.SAVING;
            }
            else if (cmd == "2")
            {
                type = account_t.CREDIT;
            }

            Console.WriteLine("Enter '1' to use an EUR, '2' for USD, '3' for GBP and '4' for JPY:");
            currency_t currency = currency_t.EUR;
            cmd = Console.ReadLine();
            if (cmd == "1")
            {
                currency = currency_t.EUR;
            }
            else if (cmd == "2")
            {
                currency = currency_t.USD;
            }
            else if (cmd == "3")
            {
                currency = currency_t.GBP;
            }
            else if (cmd == "4")
            {
                currency = currency_t.JPY;
            }

            float balance = -1.0f;
            string bal_str = "t";
            while (balance < 0)
            {
                while (!float.TryParse(bal_str, out balance))
                {
                    Console.WriteLine("Type in the balance you want to open the account with (at least 0):");
                    bal_str = Console.ReadLine();
                }

                balance = float.Parse(bal_str);
            }

            int AID = Accounts_Management.openAccount(depositor, type, currency, balance);
            Console.WriteLine("Account " + AID + " was opened:");
            account_t type_ = account_t.CREDIT;
            Accounts_Management.getAccountType(AID, ref type_);
            Console.WriteLine("Type: " + type_);
            float balance_ = 0.0f;
            AccountActions_Management.getAccountBalance(AID, ref balance_);
            Console.WriteLine("Balance: " + balance_);
            uint[] deps = new uint[20];
            int c = 0;
            Accounts_Management.getAccountDepositors(AID,  deps);
            Console.WriteLine("Depositors:");
            while (deps[c] != 0)
                {
                    CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + C.CID);
                    Console.WriteLine("First Name: " + C.FirstName);
                    Console.WriteLine("Last Name: " + C.LastName);
                    Console.WriteLine("Street Name: " + C.Street);
                    Console.WriteLine("Street Number: " + C.StreetNr);
                    Console.WriteLine("City: " + C.City);
                    Console.WriteLine("Postal Code: " + C.PostalCode);
                    Console.WriteLine("Country: " + C.Country);
                    Console.WriteLine("---------------------------------");
                    c++;
                }

            DisplayACCOUNTManagementSection();
           

            
        }
        static void DisplayFreezeACCOUNTSection()
        {
            int AID = 0;
            string AID_str = "t";


            while (!int.TryParse(AID_str, out AID))
            {

                Console.WriteLine("Insert the ID of the ACCOUNT you want to freeze (enter '0' to cancel):");
                AID_str = Console.ReadLine();
                if (AID_str == "0")
                {

                    DisplayACCOUNTManagementSection();
                }
            }
            AID = int.Parse(AID_str);
            if((Accounts_Management.isAccountOpen(AID)==-1)|| (Accounts_Management.isAccountUnfrozen(AID) == 0))
            {
                Console.WriteLine("No (open/unfrozen) ACCOUNT found with the ID " + AID);
                DisplayFreezeACCOUNTSection();
            }
            else
            {
                Console.WriteLine("The ACCOUNT with the ID " + AID + " has the following data:");
                account_t type_ = account_t.CREDIT;
                Accounts_Management.getAccountType(AID, ref type_);
                Console.WriteLine("Type: " + type_);
                float balance_ = 0.0f;
                AccountActions_Management.getAccountBalance(AID, ref balance_);
                Console.WriteLine("Balance: " + balance_);
                uint[] deps = new uint[20];
                int c = 0;
                Accounts_Management.getAccountDepositors(AID, deps);
                Console.WriteLine("Depositors:");
                while (deps[c] != 0)
                {
                    CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + C.CID);
                    Console.WriteLine("First Name: " + C.FirstName);
                    Console.WriteLine("Last Name: " + C.LastName);
                    Console.WriteLine("Street Name: " + C.Street);
                    Console.WriteLine("Street Number: " + C.StreetNr);
                    Console.WriteLine("City: " + C.City);
                    Console.WriteLine("Postal Code: " + C.PostalCode);
                    Console.WriteLine("Country: " + C.Country);
                    Console.WriteLine("---------------------------------");
                    c++;
                }
                Console.WriteLine("Enter 'leave' to leave the Freeze ACCOUNT Section, enter '1' to freeze the ACCOUNT:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "1")
                {
                    Accounts_Management.freezeAccount(AID);
                    DisplayFreezeACCOUNTSection();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayFreezeACCOUNTSection();
                }
            }


        }
        static void DisplayUnfreezeACCOUNTSection()
        {
            int AID = 0;
            string AID_str = "t";


            while (!int.TryParse(AID_str, out AID))
            {

                Console.WriteLine("Insert the ID of the ACCOUNT you want to unfreeze (enter '0' to cancel):");
                AID_str = Console.ReadLine();
                if (AID_str == "0")
                {

                    DisplayACCOUNTManagementSection();
                }
            }
            AID = int.Parse(AID_str);
            if ((Accounts_Management.isAccountOpen(AID) == -1) || (Accounts_Management.isAccountUnfrozen(AID) == 1))
            {
                Console.WriteLine("No (open/frozen) ACCOUNT found with the ID " + AID);
                DisplayUnfreezeACCOUNTSection();
            }
            else
            {
                Console.WriteLine("The ACCOUNT with the ID " + AID + " has the following data:");
                account_t type_ = account_t.CREDIT;
                Accounts_Management.getAccountType(AID, ref type_);
                Console.WriteLine("Type: " + type_);
                float balance_ = 0.0f;
                AccountActions_Management.getAccountBalance(AID, ref balance_);
                Console.WriteLine("Balance: " + balance_);
                uint[] deps = new uint[20];
                int c = 0;
                Accounts_Management.getAccountDepositors(AID, deps);
                Console.WriteLine("Depositors:");
                while (deps[c] != 0)
                {
                    CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + C.CID);
                    Console.WriteLine("First Name: " + C.FirstName);
                    Console.WriteLine("Last Name: " + C.LastName);
                    Console.WriteLine("Street Name: " + C.Street);
                    Console.WriteLine("Street Number: " + C.StreetNr);
                    Console.WriteLine("City: " + C.City);
                    Console.WriteLine("Postal Code: " + C.PostalCode);
                    Console.WriteLine("Country: " + C.Country);
                    Console.WriteLine("---------------------------------");
                    c++;
                }
                Console.WriteLine("Enter 'leave' to leave the Unfreeze ACCOUNT Section, enter '1' to unfreeze the ACCOUNT:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "1")
                {
                    Accounts_Management.unfreezeAccount(AID);
                    DisplayUnfreezeACCOUNTSection();
                }
                else
                {
                    Console.WriteLine("Command not found!");
                    DisplayUnfreezeACCOUNTSection();
                }
            }
          }
        static void DisplayCloseACCOUNTSection()
        {
            int AID = 0;
            string AID_str = "t";


            while (!int.TryParse(AID_str, out AID))
            {

                Console.WriteLine("Insert the ID of the ACCOUNT you want to close (= PERMANENT!!!!!!) (enter '0' to cancel):");
                AID_str = Console.ReadLine();
                if (AID_str == "0")
                {

                    DisplayACCOUNTManagementSection();
                }
            }
            AID = int.Parse(AID_str);
            if ((Accounts_Management.isAccountOpen(AID) == -1))
            {
                Console.WriteLine("No (open) ACCOUNT found with the ID " + AID);
                DisplayCloseACCOUNTSection();
            }
            else
            {
                Console.WriteLine("The ACCOUNT with the ID " + AID + " has the following data:");
                account_t type_ = account_t.CREDIT;
                Accounts_Management.getAccountType(AID, ref type_);
                Console.WriteLine("Type: " + type_);
                float balance_ = 0.0f;
                AccountActions_Management.getAccountBalance(AID, ref balance_);
                Console.WriteLine("Balance: " + balance_);
                uint[] deps = new uint[20];
                int c = 0;
                Accounts_Management.getAccountDepositors(AID, deps);
                Console.WriteLine("Depositors:");
                while (deps[c] != 0)
                {
                    CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + C.CID);
                    Console.WriteLine("First Name: " + C.FirstName);
                    Console.WriteLine("Last Name: " + C.LastName);
                    Console.WriteLine("Street Name: " + C.Street);
                    Console.WriteLine("Street Number: " + C.StreetNr);
                    Console.WriteLine("City: " + C.City);
                    Console.WriteLine("Postal Code: " + C.PostalCode);
                    Console.WriteLine("Country: " + C.Country);
                    Console.WriteLine("---------------------------------");
                    c++;
                }
                Console.WriteLine("Enter 'leave' to leave the Close ACCOUNT Section, enter '1' to close the ACCOUNT (=PERMANENT!!!!!!):");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "1")
                {
                    Accounts_Management.closeAccount(AID);
                    DisplayCloseACCOUNTSection();
                }
            }

        }
        static void DisplayTransferACCOUNTSection()
        {
            int number_of_accounts_transable = 0;
            Accounts_Management.countTransferableAIDs(ref number_of_accounts_transable);
            if (number_of_accounts_transable <= 0)
            {
                Console.WriteLine("No (open/unfrozen) ACCOUNTS found! Enter 'leave' to leave the Transfer ACCOUNT Section, enter 'open' to get to the Open ACCOUNT Section:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "open")
                {
                    DisplayOpenACCOUNTSection();
                }
            }
            else if (number_of_accounts_transable <2)
            {
                Console.WriteLine("There are at least 2 accounts needed to issue transfers! Enter 'leave' to leave the Transfer ACCOUNT Section, enter 'open' to get to the Open ACCOUNT Section:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "open")
                {
                    DisplayOpenACCOUNTSection();
                }
            }
            else
            {
                int srcAID = 0;
                string srcAID_str = "t";


                while (!int.TryParse(srcAID_str, out srcAID))
                {

                    Console.WriteLine("Insert the ID of the ACCOUNT you want to transfer money FROM (enter '0' to cancel):");
                    srcAID_str = Console.ReadLine();
                    if (srcAID_str == "0")
                    {

                        DisplayACCOUNTManagementSection();
                    }
                }
                srcAID = int.Parse(srcAID_str);
                if ((Accounts_Management.isAccountOpen(srcAID) == -1) || (Accounts_Management.isAccountUnfrozen(srcAID) == 0))
                {
                    Console.WriteLine("No (open/unfrozen) ACCOUNT found with the ID " + srcAID);
                    DisplayTransferACCOUNTSection();
                }

                Console.WriteLine("The ACCOUNT with the ID " + srcAID + " has the following data:");
                account_t type_ = account_t.CREDIT;
                Accounts_Management.getAccountType(srcAID, ref type_);
                Console.WriteLine("Type: " + type_);
                float balance_ = 0.0f;
                AccountActions_Management.getAccountBalance(srcAID, ref balance_);
                Console.WriteLine("Balance: " + balance_);
                uint[] deps = new uint[20];
                int c = 0;
                Accounts_Management.getAccountDepositors(srcAID, deps);
                Console.WriteLine("Depositors:");
                while (deps[c] != 0)
                {
                    CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("ID: " + C.CID);
                    Console.WriteLine("First Name: " + C.FirstName);
                    Console.WriteLine("Last Name: " + C.LastName);
                    Console.WriteLine("Street Name: " + C.Street);
                    Console.WriteLine("Street Number: " + C.StreetNr);
                    Console.WriteLine("City: " + C.City);
                    Console.WriteLine("Postal Code: " + C.PostalCode);
                    Console.WriteLine("Country: " + C.Country);
                    Console.WriteLine("---------------------------------");
                    c++;
                }
                Console.WriteLine("Enter 'leave' to leave the Transfer ACCOUNT Section, enter '1' to go on, enter '2' to start over:");
                string cmd = Console.ReadLine();
                if (cmd =="leave")
                {
                    DisplayACCOUNTManagementSection();
                }
                else if (cmd == "1")
                {
                    int destAID = 0;
                    string destAID_str = "t";


                    while (!int.TryParse(destAID_str, out destAID))
                    {

                        Console.WriteLine("Insert the ID of the ACCOUNT you want to transfer money TO (enter '0' to cancel):");
                        destAID_str = Console.ReadLine();
                        if (destAID_str == "0")
                        {

                            DisplayACCOUNTManagementSection();
                        }
                    }
                    destAID = int.Parse(destAID_str);
                    if ((Accounts_Management.isAccountOpen(destAID) == -1) || (Accounts_Management.isAccountUnfrozen(destAID) == 0))
                    {
                        Console.WriteLine("No (open/unfrozen) ACCOUNT found with the ID " + destAID);
                        DisplayTransferACCOUNTSection();
                    }
                    Console.WriteLine("The ACCOUNT with the ID " + destAID + " has the following data:");
                     type_ = account_t.CREDIT;
                    Accounts_Management.getAccountType(destAID, ref type_);
                    Console.WriteLine("Type: " + type_);
                     balance_ = 0.0f;
                    AccountActions_Management.getAccountBalance(destAID, ref balance_);
                    Console.WriteLine("Balance: " + balance_);
                    c = 0;
                    Accounts_Management.getAccountDepositors(destAID, deps);
                    Console.WriteLine("Depositors:");
                    while (deps[c] != 0)
                    {
                        CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                        Console.WriteLine("---------------------------------");
                        Console.WriteLine("ID: " + C.CID);
                        Console.WriteLine("First Name: " + C.FirstName);
                        Console.WriteLine("Last Name: " + C.LastName);
                        Console.WriteLine("Street Name: " + C.Street);
                        Console.WriteLine("Street Number: " + C.StreetNr);
                        Console.WriteLine("City: " + C.City);
                        Console.WriteLine("Postal Code: " + C.PostalCode);
                        Console.WriteLine("Country: " + C.Country);
                        Console.WriteLine("---------------------------------");
                        c++;
                    }
                    Console.WriteLine("Enter 'leave' to leave the Transfer ACCOUNT Section, enter '1' to go on, enter '2' to start over:");
                    cmd = Console.ReadLine();
                    if (cmd =="leave")
                    {
                        DisplayACCOUNTManagementSection();
                    }
                    else if (cmd == "1")
                    {
                        float amount =-1.00f;
                        string amount_str = "t";
                        float max_amount = 0;
                        AccountActions_Management.getAccountBalance(srcAID, ref max_amount);
                        while ((amount <= 0)||(amount>max_amount))
                        {
                            while (!float.TryParse(amount_str, out amount))
                            {

                                Console.WriteLine("Insert the amount (greater 0 and equal to or less than "+max_amount+") you want to transfer from ACCOUNT " + srcAID + " to ACCOUNT " + destAID + " (enter '0' to cancel):");
                                amount_str = Console.ReadLine();
                                if (amount_str == "0")
                                {

                                    DisplayACCOUNTManagementSection();
                                }
                            }
                            amount = float.Parse(amount_str);
                            if (amount > max_amount)
                            {
                                amount_str = "t";
                                Console.WriteLine("Amount was too big!");
                            }
                            else if(amount<0)
                            {
                                amount_str = "t";
                                Console.WriteLine("Amount was too small!");
                            }
                       }

                        int orderedCID = 0;
                        int ok_transfer = -1;
                        while (ok_transfer == -1)
                        {
                            uint CID = 0;
                            string CID_str = "t";

                            if (CID == 0)
                            {
                                while (!uint.TryParse(CID_str, out CID))
                                {

                                    Console.WriteLine("Insert the ID of the CUSTOMER you want to use as Orderer:");
                                    CID_str = Console.ReadLine();


                                }
                            }
                            else
                            {
                                CID_str = CID.ToString();
                            }

                            CID = uint.Parse(CID_str);
                            CUSTOMER CustomerToModify = Customers_Management.getCustomerByCID(CID);
                            if ((CustomerToModify.CID == 0) || (CustomerToModify.Active == 0))
                            {
                                Console.WriteLine("No (active) CUSTOMER found with the ID " + CID);
                                continue;
                            }
                            orderedCID =(int) CID;
                            ok_transfer =  AccountActions_Management.transferBetweenAccounts(srcAID, destAID, orderedCID, amount);

                            Console.WriteLine("The ACCOUNT (FROM) with the ID " + srcAID + " has the following data:");
                            type_ = account_t.CREDIT;
                            Accounts_Management.getAccountType(srcAID, ref type_);
                            Console.WriteLine("Type: " + type_);
                            balance_ = 0.0f;
                            AccountActions_Management.getAccountBalance(srcAID, ref balance_);
                            Console.WriteLine("Balance: " + balance_);
                            c = 0;
                            Accounts_Management.getAccountDepositors(srcAID, deps);
                            Console.WriteLine("Depositors:");
                            while (deps[c] != 0)
                            {
                                CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                                Console.WriteLine("---------------------------------");
                                Console.WriteLine("ID: " + C.CID);
                                Console.WriteLine("First Name: " + C.FirstName);
                                Console.WriteLine("Last Name: " + C.LastName);
                                Console.WriteLine("Street Name: " + C.Street);
                                Console.WriteLine("Street Number: " + C.StreetNr);
                                Console.WriteLine("City: " + C.City);
                                Console.WriteLine("Postal Code: " + C.PostalCode);
                                Console.WriteLine("Country: " + C.Country);
                                Console.WriteLine("---------------------------------");
                                c++;
                            }

                            Console.WriteLine("The ACCOUNT (TO) with the ID " + destAID + " has the following data:");
                            type_ = account_t.CREDIT;
                            Accounts_Management.getAccountType(destAID, ref type_);
                            Console.WriteLine("Type: " + type_);
                            balance_ = 0.0f;
                            AccountActions_Management.getAccountBalance(destAID, ref balance_);
                            Console.WriteLine("Balance: " + balance_);
                            c = 0;
                            Accounts_Management.getAccountDepositors(destAID, deps);
                            Console.WriteLine("Depositors:");
                            while (deps[c] != 0)
                            {
                                CUSTOMER C = Customers_Management.getCustomerByCID(deps[c]);
                                Console.WriteLine("---------------------------------");
                                Console.WriteLine("ID: " + C.CID);
                                Console.WriteLine("First Name: " + C.FirstName);
                                Console.WriteLine("Last Name: " + C.LastName);
                                Console.WriteLine("Street Name: " + C.Street);
                                Console.WriteLine("Street Number: " + C.StreetNr);
                                Console.WriteLine("City: " + C.City);
                                Console.WriteLine("Postal Code: " + C.PostalCode);
                                Console.WriteLine("Country: " + C.Country);
                                Console.WriteLine("---------------------------------");
                                c++;
                            }
                        }
                        DisplayTransferACCOUNTSection();

                    }
                    else if (cmd == "2")
                    {
                        DisplayTransferACCOUNTSection();
                    }

                }
                else if(cmd== "2")
                {
                    DisplayTransferACCOUNTSection();
                }

            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME to the Management Application of the 'Wosnotso EasyBank'");
            //necessary, or everything fails
            Customers_Management.InitializeData();
            DisplayMainMenu();
            
        }
    }
}
