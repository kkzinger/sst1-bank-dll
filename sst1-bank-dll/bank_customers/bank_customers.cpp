// bank_customers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_customers.h"


//extern "C" BANK_CUSTOMERS_API int helloWorld()
//{
//	return 0;
//}
extern Customer newCustomer = {};

extern "C" BANK_CUSTOMERS_API Customer * Create(char * FirstName, char * LastName, char * Street, char * StreetNr, char * City, char * PostalCode, char * Country)
{
	/*struct holen von entity, pointer darauf returnen (Customer*)*/
	unsigned int newCID = _createCID();
	customer_list* cl = helloWorld(true, newCID, FirstName, LastName, Street, StreetNr, City, PostalCode, Country);

    newCustomer =  (*((*cl).end()));

	return  &newCustomer;
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

extern "C" BANK_CUSTOMERS_API unsigned int Activate(unsigned int CID)
{
	//�ndert die active-Variable auf true, 0 bei success
	return 0;
}
extern "C" BANK_CUSTOMERS_API unsigned int Deactivate(unsigned int CID)
{
	//�ndert die active-Variable auf false, 0 bei success
	return -1;
}

bool _IsActive(unsigned int CID)
{
	//gibt den Wert der active-Variable zur�ck
	return false;
}
unsigned int _createCID()
{
	//generiert die n�chste CID
	customer_list* cl = helloWorld(false,0, "", "", "", "", "", "", "");
	


	return ++(*(*cl).end()).CID;

}
