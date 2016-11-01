// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// bank_accounts.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_accounts.h"
#include "..\bank_entitycomponent\bank_datatypes.h"
#include "..\bank_entitycomponent\bank_entitycomponent.h"
#include <list>
using namespace std;

extern "C" BANK_ACCOUNTS_API int Open(unsigned int* Depositors, account_t Type, currency_t CurID, float Balance)
{

	if (Balance < 0)
		return -1;//no negative balance supported

	unsigned int _Depositors[MAX_CUST_PER_ACCNT] = {};
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
		_Depositors[i] = Depositors[i];

	int AID = _addAccount(Type, CurID, Balance, _Depositors);
		if (AID < 0)
			return -2; //Something gone wrong in Entity Component

		return AID;
	
}

extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID)
{
	//ändert die open-Variable im struct auf false
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	A.open = 0;

	if (_updateAccount(&A) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards

}

unsigned int getOwners(unsigned int AID, unsigned int* Depositors)
{
	//gibt eine Array an CID zurück
	if (sizeof(Depositors) / sizeof(Depositors[0]) > MAX_CUST_PER_ACCNT)
		return -1; //provided array of cid is to big

	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
		Depositors[i] = A.depositors[i];

	return 0;
}

extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int* Depositors)
{
	//fügt CIDs dem Array hinzu
	//if (sizeof(Depositors) / sizeof(Depositors[0]) > MAX_CUST_PER_ACCNT)
		//return -1; //provided array of cid is to big

	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	unsigned int free_customers_of_account = 0;
	unsigned int customers_to_add= 0;


	//get number of customers that can be added
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (A.depositors[i] == 0)
			free_customers_of_account++;
	}
	
	if (free_customers_of_account == 0)
		return -1; //reached max. amount of customers per account

	//get number of customers that should be added
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (Depositors[i] == 0)
			customers_to_add++; 
	}

	for (int i = 0; i < customers_to_add; i++)
	{
		for (int j = 0; j < MAX_CUST_PER_ACCNT; j++)
		{
			if (A.depositors[j] == 0)
			{
				A.depositors[j] = Depositors[i];
				i++;
			}
		}
	}

	if (_updateAccount(&A) != 0)
		return -2; //Something in went wrong in Entity Component

	return 0;
}

extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID, unsigned int* Depositors)
{

	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	unsigned int customers_to_remove = MAX_CUST_PER_ACCNT;
	unsigned int temp[20] = { 0 };

	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (A.depositors[i] != 0)
		{
			for (int j = 0; j < MAX_CUST_PER_ACCNT; j++)
			{
				if (A.depositors[i] == Depositors[j])
				{
					A.depositors[i] = 0; //Found Depositor to delete. Deletion is made through setting to zero
					break;
				}
			}
		}
	}

	//clean up array to have the CIDs at beginning of Array
	int j = 0;
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		//Collect all CIDs of depositors Array for temporary saving and ordering
		if (A.depositors[i] != 0)
		{
			temp[j] = A.depositors[i];
			j++;
		}
	}
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		A.depositors[i] = temp[i];
	}
	
	if (_updateAccount(&A) != 0)
		return -2; //Something in went wrong in Entity Component

	return 0;
}

extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID)
{
	//ändert die frozen-Variable im struct auf true
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	A.unfrozen = 0;

	if (_updateAccount(&A) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;

}

extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID)
{
	//ändert die frozen-Variable im struct auf false
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	A.unfrozen = 1;

	if (_updateAccount(&A) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards
}

int getType(unsigned int AID, account_t* type)
{
	//gibt den Wert der Typ-Variable zurück
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	type = &A.type;

	return 0;
}

unsigned int IsUnFrozen(unsigned int AID)
{
	//gibt den Wert der unfrozen-Variable zurück
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	if (A.unfrozen)
		return 1;  // Account is unfrozen
	else
		return 0; // Account is frozen
}

unsigned int IsOpen(unsigned int AID)
{
	//gibt den Wert der open-Variable zurück
	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	if (A.open)
		return 1;  // Account is open
	else
		return 0; // Account is closed
}



