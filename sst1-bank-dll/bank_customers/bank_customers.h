#pragma once

#ifdef BANK_CUSTOMERS_EXPORTS
#define BANK_CUSTOMERS_API __declspec(dllexport) 
#else
#define BANK_CUSTOMERS_API __declspec(dllimport) 
#endif
#include "../bank_entitycomponent/bank_datatypes.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

//API (C)
extern "C" BANK_CUSTOMERS_API int Create(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
extern "C" BANK_CUSTOMERS_API int Read(unsigned int CID, CUSTOMER* resultCustomer);
extern "C" BANK_CUSTOMERS_API int Update(unsigned int CID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country, unsigned int IsActive);
extern "C" BANK_CUSTOMERS_API int Activate(unsigned int CID);
extern "C" BANK_CUSTOMERS_API int Deactivate(unsigned int CID);

//INTERNAL (C++)
unsigned int _IsActive(unsigned int CID);
