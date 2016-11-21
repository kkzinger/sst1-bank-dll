// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// bank_account_actions.cpp : Defines the exported functions for the DLL application.

#include "stdafx.h"
#include "bank_account_actions.h"

extern "C" BANK_ACCOUNT_ACTIONS_API int withDraw(unsigned int AID, float amount)
{
	
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	if (A.balance >= amount && A.unfrozen == 1 && A.open == 1)
	{
		A.balance -= amount;
		_updateAccount(&A);
		return 0; 
	}
	else
	{
		return -1; //Amount is to high for the current balance
	}

}
extern "C" BANK_ACCOUNT_ACTIONS_API int deposit(unsigned int AID, float amount)
{
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	A.balance += amount; //Increase Balance at this Account
	_updateAccount(&A);
	return 0;
}
extern "C" BANK_ACCOUNT_ACTIONS_API int transfer(unsigned int FromAID, unsigned int ToAID, unsigned int ordererCID, float amount)
{
	ACCOUNT A,B;
	if (_getAccountByAID(FromAID, &A) != 0)
		return -2; //Something gone wrong in Entity Component
	if (_getAccountByAID(ToAID, &B) != 0)
		return -2; //Something gone wrong in Entity Component

	printf("\n\nIN TRANSFER DLL: A.balance %.2f, amount %.2f\n", A.balance, amount);
	printf("IN TRANSFER DLL: A.unfrozen %i, B.unfrozen %i\n", A.unfrozen, B.unfrozen);
	printf("IN TRANSFER DLL: A.open %i, B.open %i\n", A.open, B.open);
	printf("IN TRANSFER DLL: ordererCID %i, FromAID %i, _isOrdererAllowed %i\n", ordererCID, FromAID, _isOrdererAllowed(ordererCID, FromAID));

	//check if there is enough money on FromAID Account and if no account is frozen/closed and if orderCID is allowed to do transactions
	if ((A.balance >= amount) && (A.unfrozen == 1) && (A.open == 1) && (B.unfrozen == 1) && (B.open == 1) && (_isOrdererAllowed(ordererCID,FromAID) == 0))
	{
		A.balance -= amount;
		B.balance += amount;
		_updateAccount(&A);
		_updateAccount(&B);
		_addTransaction(FromAID, ToAID, ordererCID, amount);
		return 0;
	}
	else
	{
		return -1; //Amount is to high for the current balance or one of the accounts is frozen/closed
	}
}
extern "C" BANK_ACCOUNT_ACTIONS_API int bankAccountStatement(unsigned int AID, TRANSACTION* transactionsList, unsigned int* len)
{
	if (_getTransactions(AID, transactionsList, len) == -1)
	{
		return -1; //transactionList Array was to small. There would be more Transactions.
	}
	return 0;
}
extern "C" BANK_ACCOUNT_ACTIONS_API int balancing(unsigned int AID, float* balance)
{
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	*(balance) = A.balance;
	return 0;
}

int _isOrdererAllowed(unsigned int CID, unsigned int AID)
{
	unsigned int cidList[MAX_CUST_PER_ACCNT];

	if (_getCustomersByAID(AID, cidList) != 0)
		return -2; //Something gone wrong in Entity Component
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		//printf("ORDERER: CID -- %d --- %d\n", cidList[i], CID);
		if (CID == cidList[i]) return 0; //found CID in Accounts Depositors List
	}
	return -1; //CID is not allowed to performe orders with this Account

}