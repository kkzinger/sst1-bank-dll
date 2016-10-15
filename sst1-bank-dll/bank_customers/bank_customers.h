#pragma once

#ifdef BANK_CUSTOMERS_EXPORTS
#define BANK_CUSTOMERS_API __declspec(dllexport) 
#else
#define BANK_CUSTOMERS_API __declspec(dllimport) 
#endif


extern "C" BANK_CUSTOMERS_API int helloWorld();


