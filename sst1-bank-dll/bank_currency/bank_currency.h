// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

#pragma once
#ifdef BANK_CURRENCY_EXPORTS
#define BANK_CURRENCY_API __declspec(dllexport) 
#else
#define BANK_CURRENCY_API __declspec(dllimport) 
#endif
#include "../bank_entitycomponent/bank_datatypes.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"

extern "C" BANK_CURRENCY_API int bar();