// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

#pragma once

#include <list>

#define MAX_CUST_PER_ACCNT 20
#define MAX_ACCNTS_PER_CUST 100

using namespace std;

typedef enum {SAVING,CREDIT} account_t;
typedef enum {EUR,USD,GBP,JPY} currency_t;

// Account Data
struct ACCOUNT
{
	unsigned int AID;
	account_t type;
	currency_t currency;
	float balance;
	unsigned int depositors[20];
	unsigned char unfrozen;
	unsigned char open;
};
typedef list <ACCOUNT> account_list;

// Customer Data
struct CUSTOMER
{
	unsigned int CID;
	char FirstName [20];
	char  LastName [20];
	char  Street [20];
	char  StreetNr [20];
	char  City [20];
	char  PostalCode [20];
	char  Country [20];
	unsigned char Active;
};
typedef list <CUSTOMER> customer_list;

// Transaction
struct TRANSACTION
{
	unsigned long timestamp;
	unsigned int sourceAID;
	unsigned int destinationAID;
	unsigned int ordererCID;
	float amount;
	
};
typedef list <TRANSACTION> transaction_list;