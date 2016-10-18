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
	unsigned int* depositors;
	unsigned char unfrozen;
	unsigned char open;
};
typedef list <ACCOUNT> account_list;

// Customer Data
struct CUSTOMER
{
	unsigned int CID;
	char* FirstName;
	char* LastName;
	char* Street;
	char* StreetNr;
	char* City;
	char* PostalCode;
	char* Country;
	unsigned char Active;
};
typedef list <CUSTOMER> customer_list;