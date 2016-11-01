using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembly_OwnDLLs_Customers;

namespace BankApplication
{
    class BankApplication
    {
        static void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press 'c' for the Customer Management section, press 'a' for the Account Management section...");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.KeyChar == 'c')
            {
                DisplayCUSTOMERManagementSection();
            }
            else if (info.KeyChar == 'a')
            {
                DisplayACCOUNTRManagementSection();
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
                Console.WriteLine("Press 'c' to get to the Create CUSTOMER Section, press 'b' to get back to the Main Menu");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == 'c')
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if(info.KeyChar == 'b')
                {
                    DisplayMainMenu();
                }
             }
            else
            {
                Console.WriteLine(number_of_customers+" customers found!");
                Console.WriteLine("Press 'b' to get back to the Main Menu, press 'c' to get to the Create CUSTOMER Section, press 'm' to get to the Modify CUSTOMER Section, press 'd' to get to the Deactivate CUSTOMER Section, press 'r' to get to the Re-activate CUSTOMER Section ");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == 'b')
                {
                    DisplayMainMenu();
                }
                else if (info.KeyChar == 'c')
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if(info.KeyChar == 'm')
                {
                    DisplayModifyCUSTOMERSection(0);
                }
                else if(info.KeyChar=='d')
                {
                    DisplayDeactivateCUSTOMERSection();
                }
                else if (info.KeyChar == 'r')
                {
                    DisplayReactivateCUSTOMERSection();
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
                Console.WriteLine("City (5): " + CustomerToModify.City);
                Console.WriteLine("Postal Code (6): " + CustomerToModify.PostalCode);
                Console.WriteLine("Country (7): " + CustomerToModify.Country);
                Console.WriteLine("Press '0' to leave the Modify CUSTOMER Section, press the corresponding number to modify the values:");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == '0')
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if (info.KeyChar == '1')
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
                else if (info.KeyChar == '2')
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
                else if (info.KeyChar == '3')
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
                else if (info.KeyChar == '4')
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
                else if (info.KeyChar == '5')
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
                else if (info.KeyChar == '6')
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
                else if (info.KeyChar == '7')
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
            }          
           
        }

        static void DisplayDeactivateCUSTOMERSection()
        {
            uint CID = 0;
            string CID_str = "t";


                while (!uint.TryParse(CID_str, out CID))
                {

                    Console.WriteLine("Insert the ID of the CUSTOMER you want to deactivate (press '0' to cancel):");
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
                Console.WriteLine("Press '0' to leave the Deactivate CUSTOMER Section, press '1' to deactivate the CUSTOMER:");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == '0')
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if(info.KeyChar=='1')
                {
                    Customers_Management.deactivateCustomer(ref CustomerToDeactivate);
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

                Console.WriteLine("Insert the ID of the CUSTOMER you want to deactivate (press '0' to cancel):");
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
                Console.WriteLine("Press '0' to leave the Deactivate CUSTOMER Section, press '1' to re.activate the CUSTOMER:");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == '0')
                {
                    DisplayCUSTOMERManagementSection();
                }
                else if (info.KeyChar == '1')
                {
                    Customers_Management.activateCustomer(ref CustomerToDeactivate);
                    DisplayReactivateCUSTOMERSection();
                }
            }
        }
        static void DisplayACCOUNTRManagementSection()
        {
            Console.WriteLine();
            Console.WriteLine("ACCOUNT Management Section:");
            int number_of_customers = Customers_Management.listAllCustomers();
            if (number_of_customers == -1)
            {
                Console.WriteLine("No customers found, create a customer first!");
                Console.WriteLine("Press 'c' to get to the Create CUSTOMER Section, press 'b' to get back to the Main Menu");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.KeyChar == 'c')
                {
                    DisplayCreateCUSTOMERSection();
                }
                else if (info.KeyChar == 'b')
                {
                    DisplayMainMenu();
                }
            }
            else
            {

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
