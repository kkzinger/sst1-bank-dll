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

	withDraw(1, 500);
	printf("Balance: %.2f\n", balancing(1));
	deposit(1, 600);
	printf("Balance: %.2f\n", balancing(1));
	withDraw(1, 1500);
	printf("Balance: %.2f\n", balancing(1));

	printf("\n--- Transfer 500 from 1 to 2 ---\n\n");
	printf("Balance 1: %.2f\n", balancing(1));
	printf("Balance 2: %.2f\n", balancing(2));
	printf("--- Transfer ---\n");
	transfer(1, 2, 1, 500);
	printf("Balance 1: %.2f\n", balancing(1));
	printf("Balance 2: %.2f\n", balancing(2));
	printf("\n--------------------------------\n\n");
	
	return 0;
}

