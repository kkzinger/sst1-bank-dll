// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// test_bank_accounts.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"
#include "../bank_entitycomponent/bank_datatypes.h"
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

	unsigned int list[MAX_ACCNTS_PER_CUST] = {};
	_getAccountsByCID(1, list);

	printf("\n--- Account List of CID 1 ---\n");
	for(int i = 0; i < MAX_ACCNTS_PER_CUST; i++)
	{
		if (list[i] != 0)
		{
			printf("AID: %d\n", list[i]);
		}
	}
	printf("\n-----------------------------\n");

	//FAIL because there is no "Read" function to fetch Account Struct of entity component
	/*printf("AID %u\n", A.AID);
	printf("Open? %u\n", A.open);

	printf("AID %u\n", A.AID);
	printf("Unfrozen? %u\n", A.unfrozen);
	*/


	return 0;
}

