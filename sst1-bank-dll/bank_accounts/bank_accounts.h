#pragma once

#ifdef BANK_ACCOUNTS_EXPORTS
#define BANK_ACCOUNTS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNTS_API __declspec(dllimport) 
#endif

#include "../bank_entitycomponent/bank_datatypes.h"

//extern "C" BANK_ACCOUNTS_API customer_list* helloWorld(unsigned int IsCreated, unsigned int CID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
//API (C)
extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int CID, char* CurID, char* Type);
extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID);
extern "C" BANK_ACCOUNTS_API customer_list* getOwners(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int CID);
extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID);
//INTERNAL (C++)
char* getType(unsigned int AID);
bool IsUnFrozen(unsigned int AID);
bool IsOpen(unsigned int AID);
unsigned int createAID();