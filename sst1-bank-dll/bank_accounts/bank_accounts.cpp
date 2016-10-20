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

extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int* Depositors, account_t Type, currency_t CurID, float Balance)
{
	if (sizeof(Depositors) / sizeof(Depositors[0]) > MAX_CUST_PER_ACCNT) //TODO Need other verification. sizeof is not suitable to control length of arrays that are obtained by pointer.
		return -1; //provided array of cid is to big

	if (Balance < 0)
		return -1;//no negative balance supported

	unsigned int _Depositors[MAX_CUST_PER_ACCNT] = {};
	for (int i = 0; i < sizeof(Depositors) / sizeof(Depositors[0]); i++)
		_Depositors[i] = Depositors[i];


		if (_addAccount(Type, CurID, Balance,  _Depositors) != 0)
			return -2; //Something gone wrong in Entity Component

		return  0;
	
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

	Depositors = A.depositors;

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

	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (A.depositors[i] = 0)
			free_customers_of_account++;
	}
	
	if (free_customers_of_account = 0)
		return -1; //reached max. amount of customers per account

	unsigned int customers_to_add = sizeof(Depositors) / sizeof(Depositors[0]);

	if(free_customers_of_account<customers_to_add)
		return -1; //too many new customers

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
	//entfernt CIDs aus dem Array
	if (sizeof(Depositors) / sizeof(Depositors[0]) > MAX_CUST_PER_ACCNT)
		return -1; //provided array of cid is to big

	ACCOUNT A;
	if (_getAccountByAID(AID, &A) != 0)
		return -2; //Something gone wrong in Entity Component

	unsigned int customers_to_remove = sizeof(Depositors) / sizeof(Depositors[0]);
	unsigned int free_customers = 0;
	unsigned int removed_customers = 0;
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (A.depositors[i] == 0)
			free_customers++;
	}

	if ((MAX_CUST_PER_ACCNT - free_customers) < customers_to_remove)
		return -1;//too few customers

	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		if (A.depositors[i] == 0)
		{
			if (removed_customers < customers_to_remove)
			{
				A.depositors[i] = Depositors[removed_customers];
				removed_customers++;
			}
			else
				break;
		}
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


	return -1; // Data did not meet required format/standards

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



