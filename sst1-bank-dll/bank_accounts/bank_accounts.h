#pragma once

#ifdef BANK_ACCOUNTS_EXPORTS
#define BANK_ACCOUNTS_API __declspec(dllexport) 
#else
#define BANK_ACCOUNTS_API __declspec(dllimport) 
#endif


extern "C" BANK_ACCOUNTS_API int helloWorlda();