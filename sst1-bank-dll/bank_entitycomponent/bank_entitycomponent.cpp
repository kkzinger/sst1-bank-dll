// bank_entitycomponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_entitycomponent.h"
#include "../include/rapidjson/document.h" // rapidjson's DOM-style API
#include "../include/rapidjson/prettywriter.h" // for stringify JSON

extern "C" BANK_ENTITYCOMPONENT_API int _getAccountsByCID(unsigned int CID, unsigned int* aidList)
{
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _getCustomersByAID(unsigned int AID, unsigned int* cidList)
{
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _getCustomerByCID(unsigned int CID, CUSTOMER* customer)
{
	customer_list::iterator it;
	for (it = allCustomers->begin(); it != allCustomers->end(); it++)
	{
		if (it->CID == CID)
		{
			*customer = *it;
			return 0;
		}
	}
	
}

extern "C" BANK_ENTITYCOMPONENT_API int _getAccountByAID(unsigned int AID, ACCOUNT* account)
{
	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		if (it->AID == AID)
		{
			*account = *it;
			return 0;
		}
	}

	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API char* _getTime()
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(char * message)
{
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char * message)
{
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API char* _readJournal(char * FromTime, char * ToTime, char * Filter, char * DebugLevel)
{
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _initEntity()
{
	allCustomers = new customer_list;
	allAccounts = new account_list;
	//read customers, accunts from file

	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API customer_list* _getCustomerListPtr()
{
	return allCustomers;
}

// add new customer to allCustomers list
extern "C" BANK_ENTITYCOMPONENT_API int _addCustomer(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{

	CUSTOMER* C = new CUSTOMER;

	C->CID = _createCID();
	C->FirstName = FirstName;
	C->LastName = LastName;
	C->Street = Street;
	C->StreetNr = StreetNr;
	C->City = City;
	C->PostalCode = PostalCode;
	C->Country = Country;
	C->Active = 1;

	allCustomers->push_back(*C);

	return 0;
}


// update customer data through copy of the data of provided CUSTOMER struct
extern "C" BANK_ENTITYCOMPONENT_API int _updateCustomer(CUSTOMER* changedCustomer)
{

	customer_list::iterator it;
	for (it = allCustomers->begin(); it != allCustomers->end(); it++)
	{
		if (it->CID == changedCustomer->CID)
		{
			it->FirstName = changedCustomer->FirstName;
			it->LastName = changedCustomer->LastName;
			it->Street = changedCustomer->Street;
			it->StreetNr = changedCustomer->StreetNr;
			it->City = changedCustomer->City;
			it->PostalCode = changedCustomer->PostalCode;
			it->Country = changedCustomer->Country;
			it->Active = changedCustomer->Active;

			return 0;
		}
	}
	return -2; //CID not found in list
}

extern "C" BANK_ENTITYCOMPONENT_API int _addAccount(account_t type, currency_t currency, float balance, unsigned int* depositors)
{
	if (sizeof(depositors) / sizeof(depositors[0]) > MAX_CUST_PER_ACCNT)
		return -1; //provided array of cid is to big

	ACCOUNT* A = new ACCOUNT;

	A->AID = _createAID();
	A->type = type;
	A->currency = currency;
	A->balance = balance;
	A->depositors = depositors;
	A->Active = 1;

	allAccounts->push_back(*A);

	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _updateAccount(ACCOUNT* changedAccount)
{
	if (sizeof(changedAccount->depositors) / sizeof(changedAccount->depositors[0]) > MAX_CUST_PER_ACCNT)
		return -1; //provided array of cid is to big

	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		if (it->AID == changedAccount->AID)
		{
			it->type = changedAccount->type;
			it->currency = changedAccount->currency;
			it->balance = changedAccount->balance;
			it->depositors = changedAccount->depositors;
			it->Active = changedAccount->Active;

			return 0;
		}
	}
	
	return -2; //AID not found in list
}


unsigned int _createCID()
{
	//generiert die nächste CID
	if (!allCustomers->empty())

		return allCustomers->back().CID + 1;
	else
		return 1;

}

unsigned int _createAID()
{
	//generiert die nächste AID
	if (!allAccounts->empty())

		return allAccounts->back().AID + 1;
	else
		return 1;

}
