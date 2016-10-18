#pragma once
#ifdef BANK_ACCOUNT_ACTIONS_EXPORTS
#define BANK_ACCOUNT_ACTIONS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNT_ACTIONS_API __declspec(dllimport) 
#endif
#include "../bank_entitycomponent/bank_datatypes.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

extern "C" BANK_ACCOUNT_ACTIONS_API int foo();
