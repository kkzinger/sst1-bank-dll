#pragma once

#ifdef BANK_ACCOUNTS_EXPORTS
#define BANK_ACCOUNTS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNTS_API __declspec(dllimport) 
#endif


extern "C" BANK_ACCOUNTS_API int helloWorld();
//API (C)
extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int CID, char* CurID, char* Type);
extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int getOwners(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int CID);
extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID);
//INTERNAL (C++)
char* getType(unsigned int AID);
bool IsUnFrozen(unsigned int AID);
bool IsOpen(unsigned int AID);
unsigned int createAID();