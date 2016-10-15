#pragma once

#ifdef BANK_ENTITYCOMPONENT_EXPORTS
#define  BANK_ENTITYCOMPONENT_API __declspec(dllexport) 
#else
#define  BANK_ENTITYCOMPONENT_API __declspec(dllimport) 
#endif

struct ACCOUNT
{
	unsigned int aid;
};

struct CUSTOMER
{
	unsigned int cid;
};


extern "C" BANK_ENTITYCOMPONENT_API CUSTOMER* getAccountsByCID(unsigned int cid);
extern "C" BANK_ENTITYCOMPONENT_API ACCOUNT* getCustomersByAID(unsigned int aid);
extern "C" BANK_ENTITYCOMPONENT_API CUSTOMER getCustomerByCID(unsigned int cid);
extern "C" BANK_ENTITYCOMPONENT_API ACCOUNT getAccountByAID(unsigned int aid);

//internal functions
extern "C" BANK_ENTITYCOMPONENT_API char* _getTime();
extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(char* message);
extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char* message);
extern "C" BANK_ENTITYCOMPONENT_API char* _readJournal(char* FromTime, char* ToTime, char* Filter, char* DebugLevel);