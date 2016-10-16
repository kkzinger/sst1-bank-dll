// test_bank_customers.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_customers.lib;


#include "stdafx.h"
#include <iostream>
#include <list>
#include <string>

#include "../bank_customers/bank_customers.h"
#include "../bank_accounts/bank_accounts.h"
//#include "../bank_entitycomponent/bank_entitycomponent.h" #vorher noch struct aus bank_accounts.h entfernen

using namespace std;
int main()
{
	Create("Tobias", "Mayer", "Str.", "Nr.", "Plz", "Stadt", "AUSTRIA");
	
	Create("T", "M", "Straﬂe", "#", "5202", "Neumarkt", "÷sterreich");
	
	customer_list* cl = helloWorld(false, 0, "", "", "", "", "", "", "");
	printf("\nThere are %i customers in total\n", (*cl).size());
	

	customer_list::const_iterator iterator;
	for (iterator = (*cl).begin(); iterator != (*cl).end(); ++iterator) {
		printf("\n%i %p %s %s %s %s %s %s %s \n\n", (*iterator).CID, &(*iterator), (*iterator).FirstName, (*iterator).LastName, (*iterator).Street, (*iterator).StreetNr, (*iterator).City, (*iterator).PostalCode, (*iterator).Country);

	}

	return 0;
}

