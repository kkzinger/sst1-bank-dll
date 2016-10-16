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
	Customer* C = Create("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	
	Create( "T", "M", "Straﬂe", "#", "5202", "Neumarkt", "÷sterreich");
	
	customer_list* cl = helloWorld(0, 0, "", "", "", "", "", "", "");
	printf("\n %i customers in total\n", (*cl).size());
	

	customer_list::const_iterator iterator;
	for (iterator = (*cl).begin(); iterator != (*cl).end(); ++iterator)
	{
		printf("\n%i %p %s %s %s %s %s %s %s \n\n", (*iterator).CID, &(*iterator), (*iterator).FirstName, (*iterator).LastName, (*iterator).Street, (*iterator).StreetNr, (*iterator).City, (*iterator).PostalCode, (*iterator).Country);

	}

	C= Update(1, "Tobias", "Mayer", "Str.", "17a", "Stadt", "Plz", "AUSTRIA", 2);
	C= Update(1, "", "Huber", "", "", "", "", "", 2);
	printf("\n %i customers in total\n", (*cl).size());
	for (iterator = (*cl).begin(); iterator != (*cl).end(); ++iterator)
	{
		printf("\n%i %p %s %s %s %s %s %s %s \n\n", (*iterator).CID, &(*iterator), (*iterator).FirstName, (*iterator).LastName, (*iterator).Street, (*iterator).StreetNr, (*iterator).City, (*iterator).PostalCode, (*iterator).Country);

	}
	cout <<"CID "<< C->CID << endl;
	cout << "Active? " << C->Active << endl;
	C= Deactivate(C->CID);
	cout <<"Active? "<< C->Active << endl;
	C = Activate(C->CID);
	cout << "Active? " << C->Active << endl;

	C = Read(C->CID);
	printf("\nREAD: %i %p %s %s %s %s %s %s %s \n\n", C->CID, &C, C->FirstName, C->LastName, C->Street, C->StreetNr, C->City, C->PostalCode, C->Country);


	return 0;
}

