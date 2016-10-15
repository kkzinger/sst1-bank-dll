// bank_customers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_customers.h"


extern "C" BANK_CUSTOMERS_API int helloWorld()
{
	return 0;
}




extern "C" BANK_CUSTOMERS_API  Customer*  Create(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{
	//struct holen von entity, pointer darauf returnen (Customer*)
	
	Customer Customer = {};
	Customer.CID = _createCID();
	strcpy_s(Customer.FirstName, strlen(FirstName)+1, FirstName);
	strcpy_s(Customer.LastName, strlen(LastName) + 1, LastName);
	strcpy_s(Customer.Street, strlen(Street) + 1, Street);
	strcpy_s(Customer.StreetNr, strlen(StreetNr) + 1, StreetNr);
	strcpy_s(Customer.City, strlen(City) + 1, City);
	strcpy_s(Customer.PostalCode, strlen(PostalCode) + 1, PostalCode);
	strcpy_s(Customer.Country, strlen(Country) + 1, Country);
	Customer.Active = Activate(Customer.CID)==0;

	return &Customer;
}

extern "C" BANK_CUSTOMERS_API void Read(unsigned int CID)
{
	//gibt ausgelesene Variablen des struct zurück
}

extern "C" BANK_CUSTOMERS_API unsigned int Update(unsigned int AID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{
	//ändert Variablen in struct, gibt 0 für success zurück
	return -1;
}

extern "C" BANK_CUSTOMERS_API unsigned int Activate(unsigned int CID)
{
	//ändert die active-Variable auf true, 0 bei success
	return 0;
}
extern "C" BANK_CUSTOMERS_API unsigned int Deactivate(unsigned int CID)
{
	//ändert die active-Variable auf false, 0 bei success
	return -1;
}

bool _IsActive(unsigned int CID)
{
	//gibt den Wert der active-Variable zurück
	return false;
}
unsigned int _createCID()
{
	//generiert die nächste CID
	return 222;
}
