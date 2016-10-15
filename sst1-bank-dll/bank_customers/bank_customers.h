#pragma once

#ifdef BANK_CUSTOMERS_EXPORTS
#define BANK_CUSTOMERS_API __declspec(dllexport) 
#else
#define BANK_CUSTOMERS_API __declspec(dllimport) 
#endif

//API (C)
extern "C" BANK_CUSTOMERS_API int helloWorld();
extern "C" BANK_CUSTOMERS_API  unsigned int  Create(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
extern "C" BANK_CUSTOMERS_API void Read(unsigned int CID);
extern "C" BANK_CUSTOMERS_API unsigned int Update(unsigned int AID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
extern "C" BANK_CUSTOMERS_API unsigned int Acivate(unsigned int CID);
extern "C" BANK_CUSTOMERS_API unsigned int Deactivate(unsigned int CID);

//INTERNAL (C++)
bool IsActive(unsigned int CID);
unsigned int createCID();