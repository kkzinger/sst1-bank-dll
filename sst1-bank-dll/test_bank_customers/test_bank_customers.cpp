// test_bank_customers.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_customers.lib;


#include "stdafx.h"
#include <iostream>
#include <list>
#include <string>

#include "../bank_customers/bank_customers.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

//using namespace std;
int main()
{
	CUSTOMER C;
	_initEntity();
	Create("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	Create( "T", "M", "Straﬂe", "#", "5202", "Neumarkt", "÷sterreich");
	
	//customer_list* cl = helloWorld(0, 0, "", "", "", "", "", "", "");
	//printf("\n %i customers in total\n", (*cl).size());
	
	if (Read(1, &C) == 0)
	{
		printf("\n%u %s %s %s %s %s %s %s %u \n\n", C.CID, C.FirstName, C.LastName, C.Street, C.StreetNr, C.City, C.PostalCode, C.Country, C.Active);
	}

	if (Update(1, "Tobias", "Huber", "Foobar Str.", "17a", "Ried im Innkreis", "4910", "AUSTRIA") != 0)
		return -1;
	
	if (Read(1, &C) == 0)
	{
		printf("\n%u %s %s %s %s %s %s %s %u \n\n", C.CID, C.FirstName, C.LastName, C.Street, C.StreetNr, C.City, C.PostalCode, C.Country, C.Active);
	}
	
	printf("CID %u\n", C.CID);
	printf("Active? %u\n", C.Active);
	
	Activate(C.CID);
	if (Read(1, &C) != 0)
		return -1;
	
	printf("Active? %u\n", C.Active);
	
	Deactivate(C.CID);
	if (Read(1, &C) != 0)
		return -1;
	
	printf("Active? %u\n", C.Active);


	return 0;
}

