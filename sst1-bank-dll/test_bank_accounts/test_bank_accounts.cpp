// test_bank_accounts.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_accounts.lib;

#include "stdafx.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"
#include "../bank_accounts/bank_accounts.h"

int main()
{
	ACCOUNT A;
	_initEntity();
	unsigned int CustID[1] = { 1 };
	Open(CustID, CREDIT, EUR, 0);

	ACCOUNT B;
	unsigned int CustIDs[2];
	CustIDs[0] = 1;
	CustIDs[1] = 2;

	Open(CustIDs, SAVING, USD, 1000);



	printf("AID %u\n", A.AID);
	printf("Open? %u\n", A.open);

	printf("AID %u\n", A.AID);
	printf("Unfrozen? %u\n", A.unfrozen);



	return 0;
}

