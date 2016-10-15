// test_bank_customers.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_customers.lib;


#include "stdafx.h"
#include <iostream>
#include <string>

#include "../bank_customers/bank_customers.h"
using namespace std;
int main()
{
	Customer * Customer = Create("Tobias", "Mayer", "Salzburgerstr.", "17a", "Neumarkt am Wallersee", "5202", "AUSTRIA");
	printf("%i: %s %s\n%s %s\n%s %s\n%s", Customer->CID, Customer->FirstName, Customer->LastName, Customer->Street, Customer->StreetNr, Customer->PostalCode, Customer->City, Customer->Country);
    
	Customer = Create("Gerold", "Katzinger", "Salzburgerstr.", "17a", "Neumarkt am Wallersee", "5202", "AUSTRIA");
	printf("%i: %s %s\n%s %s\n%s %s\n%s", Customer->CID, Customer->FirstName, Customer->LastName, Customer->Street, Customer->StreetNr, Customer->PostalCode, Customer->City, Customer->Country);

	return 0;
}

