// bank_customers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_customers.h"

extern "C" BANK_CUSTOMERS_API int helloWorld()
{
	return 0;
}

extern "C" BANK_CUSTOMERS_API  unsigned int  Create(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{
	//struct holen von entity
	return 0;
}

extern "C" BANK_CUSTOMERS_API void Read(unsigned int CID)
{
	//gibt ausgelesene Variablen des struct zur�ck
}

extern "C" BANK_CUSTOMERS_API unsigned int Update(unsigned int AID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{
	//�ndert Variablen in struct, gibt 0 f�r success zur�ck
	return -1;
}

extern "C" BANK_CUSTOMERS_API unsigned int Acivate(unsigned int CID)
{
	//�ndert die active-Variable auf true, 0 bei success
	return -1;
}
extern "C" BANK_CUSTOMERS_API unsigned int Deactivate(unsigned int CID)
{
	//�ndert die active-Variable auf false, 0 bei success
	return -1;
}

bool IsActive(unsigned int CID)
{
	//gibt den Wert der active-Variable zur�ck
	return false;
}
unsigned int createCID()
{
	//generiert die n�chste CID
	return -1;
}
