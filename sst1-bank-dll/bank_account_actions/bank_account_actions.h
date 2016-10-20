#pragma once
#ifdef BANK_ACCOUNT_ACTIONS_EXPORTS
#define BANK_ACCOUNT_ACTIONS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNT_ACTIONS_API __declspec(dllimport) 
#endif
#include "../bank_entitycomponent/bank_datatypes.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

//API (C)
extern "C" BANK_ACCOUNT_ACTIONS_API int withDraw(unsigned int AID, float amount);
extern "C" BANK_ACCOUNT_ACTIONS_API int deposit(unsigned int AID, float amount);
extern "C" BANK_ACCOUNT_ACTIONS_API int transfer(unsigned int FromAID, unsigned int ToAID, unsigned int ordererCID, float amount);
extern "C" BANK_ACCOUNT_ACTIONS_API int bankAccountStatement(unsigned int AID, TRANSACTION* transactionsList);
extern "C" BANK_ACCOUNT_ACTIONS_API float balancing(unsigned int AID);

//INTERNAL (C++)
int _isOrdererAllowed(unsigned int CID, unsigned int AID);