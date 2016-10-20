#pragma once

#ifdef BANK_ENTITYCOMPONENT_EXPORTS
#define  BANK_ENTITYCOMPONENT_API __declspec(dllexport) 
#else
#define  BANK_ENTITYCOMPONENT_API __declspec(dllimport) 
#endif

#include <stdio.h>
#include <string.h>
#include <time.h>


#include "bank_datatypes.h"
#include "../include/rapidjson/document.h" // rapidjson's DOM-style API
#include "../include/rapidjson/filewritestream.h" // rapidjson's Stream Writer
#include "../include/rapidjson/filereadstream.h"
#include "../include/rapidjson/writer.h"

using namespace rapidjson;
using namespace std;

// Lists of Customer/Account Structs, that represents all available Customers 
//TODO make singleton so that only one allCustomers/allAccounts exists
static customer_list* allCustomers;
static account_list* allAccounts;
static transaction_list* allTransactions;
static unsigned long transactionSequenceNumber;

//internal functions
//Initialize entity_component. Must not be called twice (no Singleton at this time implemented)
extern "C" BANK_ENTITYCOMPONENT_API int _initEntity();

//Search function, provides a list of AIDs for one given CID
extern "C" BANK_ENTITYCOMPONENT_API int _getAccountsByCID(unsigned int CID, unsigned int* aidList);
//Search function, provides a list of CIDs for one given AID
extern "C" BANK_ENTITYCOMPONENT_API int _getCustomersByAID(unsigned int AID, unsigned int* cidList);
//Search function, provides data for CUSTOMER struct for given CID
extern "C" BANK_ENTITYCOMPONENT_API int _getCustomerByCID(unsigned int CID, CUSTOMER* customer);
//Search function, provides data for ACCOUNT struct for given AID
extern "C" BANK_ENTITYCOMPONENT_API int _getAccountByAID(unsigned int AID, ACCOUNT* account);

//Add new Customer to allCustomers list. 
extern "C" BANK_ENTITYCOMPONENT_API int _addCustomer(char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country);
//Update a Customer with given CUSTOMER Struct. The calling entity should get the Customer through _getCustomerByCID, 
//all changes should be applieded on the retrieved struct and this struct should be given back to _updateCustomer
extern "C" BANK_ENTITYCOMPONENT_API int _updateCustomer(CUSTOMER* changedCustomer);

//Add new Account struct to allAccounts list
extern "C" BANK_ENTITYCOMPONENT_API int _addAccount(account_t type, currency_t currency, float balance, unsigned int* depositors);
//Update a Account with given ACCOUNT Struct. The calling entity should get the Account through _getAccountByAID, 
//all changes should be applieded on the retrieved struct and this struct should be given back to _updateAccount
extern "C" BANK_ENTITYCOMPONENT_API int _updateAccount(ACCOUNT* changedAccount);

//Add a Transaction Struct to allTransactions list. This provides functionality to store informations about a transfer between Accounts.
extern "C" BANK_ENTITYCOMPONENT_API int _addTransaction(unsigned int sourceAID, unsigned int destinationAID, unsigned int ordererCID, float amount);
//Search function, provides data for ACCOUNT struct for given AID
extern "C" BANK_ENTITYCOMPONENT_API int _getTransactions(unsigned int AID, TRANSACTION* transactionList, unsigned int len);

//not in use
extern "C" BANK_ENTITYCOMPONENT_API char* _getTime();
//not in use
extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(/*char* message*/);
//not in use
extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char* message);
//not in use
extern "C" BANK_ENTITYCOMPONENT_API char* _readJournal(char* FromTime, char* ToTime, char* Filter, char* DebugLevel);


//DLL intern functions
unsigned int _createCID();
unsigned int _createAID();
//provides Timestamp/Sequence Number for Transactions
extern "C" BANK_ENTITYCOMPONENT_API unsigned long _createTimestamp();
//returns Pointer to allCustomers list (is used for testing) should not be used in production because it allows datamanipulation without data checking
extern "C" BANK_ENTITYCOMPONENT_API customer_list* _getCustomerListPtr();

//Writes a Powerjson Document to given Filename
int _writeJsonToFile(char* file, Document* json);
//Reads a Powerjson Document from given Filename
int _readJsonFromFile(char* file, Document* json);

//Creates a Json object of allCustomers and writes it to File
int _writeCustomers();
//Creates a Json object of allAccounts and writes it to File
int _writeAccounts();
//Reads Json from File and fills allCustomers list
int _readCustomers();
//Reads Json from File and fills allAccounts lsit
int _readAccounts();