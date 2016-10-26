// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

#pragma once

#ifdef BANK_CUSTOMERS_EXPORTS
#define BANK_CUSTOMERS_API __declspec(dllexport) 
#else
#define BANK_CUSTOMERS_API __declspec(dllimport) 
#endif
#include "../bank_entitycomponent/bank_datatypes.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

//API (C)
extern "C" BANK_CUSTOMERS_API int Create(const char* FirstName, const char* LastName, const char* Street, const char* StreetNr,  const char* City, const char* PostalCode, const char* Country);
//Get data of Customer with provided CID for provided CUSTOMER struct 
extern "C" BANK_CUSTOMERS_API int Read(unsigned int CID, CUSTOMER* resultCustomer);
extern "C" BANK_CUSTOMERS_API int Update(unsigned int CID, const char* FirstName, const char* LastName, const char* Street, const char* StreetNr,  const char* City, const char* PostalCode, const char* Country);
extern "C" BANK_CUSTOMERS_API int Activate(unsigned int CID);
extern "C" BANK_CUSTOMERS_API int Deactivate(unsigned int CID);

//INTERNAL (C++)
//test if CID is active
unsigned int _IsActive(unsigned int CID);
