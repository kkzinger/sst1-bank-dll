#pragma once

#ifdef BANK_ENTITYCOMPONENT_EXPORTS
#define  BANK_ENTITYCOMPONENT_API __declspec(dllexport) 
#else
#define  BANK_ENTITYCOMPONENT_API __declspec(dllimport) 
#endif

#include "bank_datatypes.h"

// Lists of Customer/Account Structs, that represents all available Customers 
//TODO make singleton so that only one allCustomers/allAccounts exists
static customer_list* allCustomers;
static account_list* allAccounts;

//internal functions
extern "C" BANK_ENTITYCOMPONENT_API int _initEntity();

extern "C" BANK_ENTITYCOMPONENT_API int _getAccountsByCID(unsigned int CID, unsigned int* aidList);
extern "C" BANK_ENTITYCOMPONENT_API int _getCustomersByAID(unsigned int AID, unsigned int* cidList);
extern "C" BANK_ENTITYCOMPONENT_API int _getCustomerByCID(unsigned int CID, CUSTOMER* customer);
extern "C" BANK_ENTITYCOMPONENT_API int _getAccountByAID(unsigned int AID, ACCOUNT* account);

extern "C" BANK_ENTITYCOMPONENT_API int _addCustomer(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
extern "C" BANK_ENTITYCOMPONENT_API int _updateCustomer(CUSTOMER* changedCustomer);

extern "C" BANK_ENTITYCOMPONENT_API int _addAccount(account_t type, currency_t currency, float balance, unsigned int* depositors);
extern "C" BANK_ENTITYCOMPONENT_API int _updateAccount(ACCOUNT* changedAccount);



extern "C" BANK_ENTITYCOMPONENT_API char* _getTime();
extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(char* message);
extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char* message);
extern "C" BANK_ENTITYCOMPONENT_API char* _readJournal(char* FromTime, char* ToTime, char* Filter, char* DebugLevel);


//DLL intern functions
unsigned int _createCID();
unsigned int _createAID();
extern "C" BANK_ENTITYCOMPONENT_API customer_list* _getCustomerListPtr();
