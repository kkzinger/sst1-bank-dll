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
            if(Customers_Management.listAllCustomers()==-1)
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
            Console.WriteLine("Street Number: " + C.CID);
            Console.WriteLine("City: " + C.City);
            Console.WriteLine("Postal Code: " + C.PostalCode);
            Console.WriteLine("Country: " + C.Country);
            Console.WriteLine("...has been created!");

        }

        static void DisplayACCOUNTRManagementSection()
        {
            Console.WriteLine();
            Console.WriteLine("ACCOUNT Management Section:");
        }


        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME to the Management Application of the 'Wosnatso EasyBank'");
            Customers_Management.InitializeData();
            DisplayMainMenu();




        }
    }
}
