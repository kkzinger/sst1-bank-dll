// test_bank_customers.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_customers.lib;


#include "stdafx.h"
#include <iostream>
#include <list>
#include <string>

#include "../bank_customers/bank_customers.h"
//#include "../bank_entitycomponent/bank_entitycomponent.h" #vorher noch struct aus bank_customers.h entfernen

using namespace std;
int main()
{
	typedef list <Customer> customer_list;
	customer_list cl;

	Customer * Customer = Create("Tobias", "Mayer", "Salzburgerstr.", "17a", "Neumarkt am Wallersee", "5202", "AUSTRIA");
	printf("%i: %s %s\n%s %s\n%s %s\n%s", Customer->CID, Customer->FirstName, Customer->LastName, Customer->Street, Customer->StreetNr, Customer->PostalCode, Customer->City, Customer->Country);
    
	cl.push_back(*Customer);

	Customer = Create("T", "M", "Salzburgerstr.", "17a", "Neumarkt am Wallersee", "5202", "AUSTRIA");
	printf("%i: %s %s\n%s %s\n%s %s\n%s", Customer->CID, Customer->FirstName, Customer->LastName, Customer->Street, Customer->StreetNr, Customer->PostalCode, Customer->City, Customer->Country);

	cl.push_back(*Customer);

	for (customer_list::const_iterator ci = cl.begin(); ci != cl.end(); ++ci)
	{
		printf("%i: %s %s\n%s %s\n%s %s\n%s", (*ci).CID, (*ci).FirstName, (*ci).LastName, (*ci).Street, (*ci).StreetNr, (*ci).PostalCode, (*ci).City, (*ci).Country);

	}
		
	
	

	return 0;
}

