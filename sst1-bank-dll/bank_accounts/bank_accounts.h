// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

#pragma once

#ifdef BANK_ACCOUNTS_EXPORTS
#define BANK_ACCOUNTS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNTS_API __declspec(dllimport) 
#endif

#include "../bank_entitycomponent/bank_datatypes.h"

//API (C)
extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int* Depositors, account_t Type, currency_t CurID, float Balance );
extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int getOwners(unsigned int AID, unsigned int* Depositors);
extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int* Depositors);
extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID, unsigned int* Depositors);
extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID);
extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID);
//INTERNAL (C++)
int getType(unsigned int AID, account_t* type);
unsigned int IsUnFrozen(unsigned int AID);
unsigned int IsOpen(unsigned int AID);
