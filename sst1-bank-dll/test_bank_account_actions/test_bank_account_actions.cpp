// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// test_bank_account_actions.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"
#include "../bank_account_actions/bank_account_actions.h"
#include "../bank_accounts/bank_accounts.h"
#include "../bank_customers/bank_customers.h"

int main()
{
	_initEntity();
	Create("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	Create("Tobi", "Mayor", "Ignaz-Harrer-Str", "17a", "Salzburg", "5020", "AUSTRIA");

	unsigned int cid_1[1] = { 1 };
	unsigned int cid_2[1] = { 2 };

	Open(cid_1, SAVING, USD, 1000);
	Open(cid_2, SAVING, USD, 3000);
	float balance = 0.0f;
	withDraw(1, 500);
	balancing(1, &balance);
	printf("Balance: %.2f\n", balance);
	deposit(1, 600);
	printf("Balance: %.2f\n", balancing(1, &balance));
	withDraw(1, 1500);
	printf("Balance: %.2f\n", balancing(1, &balance));

	printf("\n--- Transfer 500 from 1 to 2 ---\n\n");
	printf("Balance 1: %.2f\n", balancing(1, &balance));
	printf("Balance 2: %.2f\n", balancing(2, &balance));
	printf("--- Transfer ---\n");
	transfer(1, 2, 1, 500);
	printf("Balance 1: %.2f\n", balancing(1, &balance));
	printf("Balance 2: %.2f\n", balancing(2, &balance));
	printf("\n--------------------------------\n\n");

	transfer(1, 2, 1, 50);
	transfer(1, 2, 1, 40);
	transfer(1, 2, 1, 60);
	transfer(1, 2, 1, 20);
	transfer(2, 1, 2, 90);

	unsigned int len = 50;
	TRANSACTION foo[50] = {};
	bankAccountStatement(1, foo, len);

	printf("\n--- Bank Account Statement for AID 1 ---\n");
	for (unsigned int i = 0; i < len; i++)
	{
		if (foo[i].timestamp == 0) continue;
		printf("%lu -- from: %d-- to: %d -- orderCID: %d -- %.2f\n", foo[i].timestamp, foo[i].sourceAID, foo[i].destinationAID, foo[i].ordererCID, foo[i].amount);
	}

	
	return 0;
}

