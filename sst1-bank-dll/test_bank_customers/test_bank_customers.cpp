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
	Customer* C = Create("Tobias", "Mayer", "Str.", "Nr.", "Plz", "Stadt", "AUSTRIA");
	printf("%i %p\n", (*C).CID, C);
		
	
	

	return 0;
}

