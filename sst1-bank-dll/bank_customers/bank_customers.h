#pragma once

#ifdef BANK_CUSTOMERS_EXPORTS
#define BANK_CUSTOMERS_API __declspec(dllexport) 
#else
#define BANK_CUSTOMERS_API __declspec(dllimport) 
#endif
#include  "../bank_accounts/bank_accounts.h"


//API (C)
//extern "C" BANK_CUSTOMERS_API int helloWorld();
extern "C" BANK_CUSTOMERS_API  Customer*  Create(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
extern "C" BANK_CUSTOMERS_API  Customer*  Read(unsigned int CID);
extern "C" BANK_CUSTOMERS_API Customer* Update(unsigned int CID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country, unsigned int IsActive);
extern "C" BANK_CUSTOMERS_API Customer* Activate(unsigned int CID);
extern "C" BANK_CUSTOMERS_API Customer* Deactivate(unsigned int CID);

//INTERNAL (C++)
unsigned int _IsActive(unsigned int CID);
unsigned int _createCID();