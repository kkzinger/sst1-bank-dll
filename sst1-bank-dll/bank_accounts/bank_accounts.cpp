// bank_accounts.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_accounts.h"
#include <list>
using namespace std;



//extern "C" BANK_ACCOUNTS_API customer_list* helloWorld(unsigned int IsCreated,unsigned int CID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
//{
//	
//	if (IsCreated==1)
//	{
//		customer_list All_Customers_Help;
//		Customer* C = new Customer;
//		All_Customers_Help.push_back(*C);
//		(*All_Customers_Help.end()).CID = CID;
//		(*All_Customers_Help.end()).FirstName = FirstName;
//		(*All_Customers_Help.end()).LastName = LastName;
//		(*All_Customers_Help.end()).Street = Street;
//		(*All_Customers_Help.end()).StreetNr = StreetNr;
//		(*All_Customers_Help.end()).City = City;
//		(*All_Customers_Help.end()).PostalCode = PostalCode;
//		(*All_Customers_Help.end()).Country = Country;
//		(*All_Customers_Help.end()).Active = IsCreated;
//		All_Customers.push_back(*All_Customers_Help.end());
//
//	}
//
//	return &All_Customers;
//}



extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int CID, char* CurID, char* Type)
{
	//struct holen von entity, pointer darauf returnen (Account*)
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID)
{
	//ändert die open-Variable im struct auf false
	return -1;
}

extern "C" BANK_ACCOUNTS_API customer_list* getOwners(unsigned int AID)
{
	//gibt eine Liste an Customer* zurück
	return nullptr;
}

extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int CID)
{
	//fügt der einen Customer* der Liste hinzu
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID)
{
	//entfernt einen Customer* aus der Liste
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID)
{
	//ändert die frozen-Variable im struct auf true
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID)
{
	//ändert die frozen-Variable im struct auf false
	return -1;
}

char* getType(unsigned int AID)
{
	//gibt den Wert der Typ-Variable zurück
	return (char*)AID;
}

bool IsUnFrozen(unsigned int AID)
{
	//gibt den Wert der frozen-Variable zurück
	return false;
}

bool IsOpen(unsigned int AID)
{
	//gibt den Wert der open-Variable zurück
	return false;
}

unsigned int createAID()
{
	//gibt die nächste AID zurück
	return -1;
}

