// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// bank_entitycomponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_entitycomponent.h"

extern "C" BANK_ENTITYCOMPONENT_API int _getAccountsByCID(unsigned int CID, unsigned int* aidList)
{
	//check if aidList has correct size
	//if (sizeof(aidList) / sizeof(aidList[0]) < MAX_ACCNTS_PER_CUST)
	//{
	//	return (sizeof(aidList) / sizeof(unsigned int)); //provided array of aid is to small
	//}
	//clean aidList 
	for(int i = 0; i < MAX_ACCNTS_PER_CUST; i++)
	{
		aidList[i] = 0;
	}


	unsigned int aidListPos = 0;

	//search in all accounts (depositor list) for the CID. If found save AID to aidList
	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
		{
			if (CID == it->depositors[i])
			{
				aidList[aidListPos] = it->AID;
				aidListPos++;
			}
		}
	}

	if (aidListPos == 0) return -2; //No Accounts connected to this CID were found

	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _getCustomersByAID(unsigned int AID, unsigned int* cidList)
{
	//check if cidList has correct size
	//if (sizeof(cidList) / sizeof(unsigned int) < MAX_CUST_PER_ACCNT)
	//	return -1; //provided array of cid is to small
	
	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		//printf("getcustom by aid %d",it->AID);
		if (it->AID == AID)
		{
			for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
				cidList[i] = it->depositors[i];
			return 0; //Found Account and return depositor list
		}
	}

	return -1; //Did not found Account
}

extern "C" BANK_ENTITYCOMPONENT_API int _getCustomerByCID(unsigned int CID, CUSTOMER* customer)
{
	customer_list::iterator it;
	for (it = allCustomers->begin(); it != allCustomers->end(); it++)
	{
		if (it->CID == CID)
		{
			*customer = *it;

			return 0;
		}
	}

	return -1;
	
}

extern "C" BANK_ENTITYCOMPONENT_API int _getAccountByAID(unsigned int AID, ACCOUNT* account)
{
	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		if (it->AID == AID)
		{
			*account = *it;
			return 0;
		}
	}

	return -1;
}

extern "C" BANK_ENTITYCOMPONENT_API char* _getTime()
{
	//TODO
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(/*char * message*/)
{
	//Document doc;
	//char* json = "{ \"hello\": \"world\",\"t\" : true ,\"f\" : false }";
	//doc.Parse(json);

	//_writeJsonToFile("json.txt", &doc);

	//Document foo;
	//_readJsonFromFile("json.txt", &foo);

	//StringBuffer buffer;
	//Writer<StringBuffer> writer(buffer);
	//foo.Accept(writer);

	//const char* output = buffer.GetString();
	//printf("Read Json:\n%s", output);
	_writeCustomers();
	_readCustomers();
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char * message)
{
	//TODO
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API char* _readJournal(char * FromTime, char * ToTime, char * Filter, char * DebugLevel)
{
	//TODO
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _initEntity()
{
	allCustomers = new customer_list;
	allAccounts = new account_list; 
	allTransactions = new transaction_list;
	transactionSequenceNumber = 0;
	//read customers, accounts, transactions, transactionsSequenceNumber from file

	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API customer_list* _getCustomerListPtr()
{
	return allCustomers;
}

// add new customer to allCustomers list
extern "C" BANK_ENTITYCOMPONENT_API int _addCustomer(char FirstName[], char LastName[], char Street[], char StreetNr[],  char City[], char PostalCode[], char Country [])
{

	CUSTOMER* C = new CUSTOMER;

	C->CID = _createCID();
	strcpy_s(C->FirstName, sizeof(C->FirstName), FirstName);
	strcpy_s(C->LastName, sizeof(C->LastName), LastName);
	strcpy_s(C->Street, sizeof(C->Street), Street);
	strcpy_s(C->StreetNr, sizeof(C->StreetNr), StreetNr);
	strcpy_s(C->City, sizeof(C->City), City);
	strcpy_s(C->PostalCode, sizeof(C->PostalCode), PostalCode);
	strcpy_s(C->Country, sizeof(C->Country), Country);
	C->Active = 1;

	allCustomers->push_back(*C);

	return C->CID;
}


// update customer data through copy of the data of provided CUSTOMER struct
extern "C" BANK_ENTITYCOMPONENT_API int _updateCustomer(CUSTOMER* changedCustomer)
{
	

	customer_list::iterator it;
	for (it = allCustomers->begin(); it != allCustomers->end(); it++)
	{
		if (it->CID == changedCustomer->CID)
		{
			strcpy_s(it->FirstName, sizeof(it->FirstName), changedCustomer->FirstName);
			strcpy_s(it->LastName, sizeof(it->LastName), changedCustomer->LastName);
			strcpy_s(it->Street, sizeof(it->Street), changedCustomer->Street);
			strcpy_s(it->StreetNr, sizeof(it->StreetNr), changedCustomer->StreetNr);
			strcpy_s(it->City, sizeof(it->City), changedCustomer->City);
			strcpy_s(it->PostalCode, sizeof(it->PostalCode), changedCustomer->PostalCode);
			strcpy_s(it->Country, sizeof(it->Country), changedCustomer->Country);
			it->Active = changedCustomer->Active;
			return 0;
		}
	}
	return -2; //CID not found in list
}

extern "C" BANK_ENTITYCOMPONENT_API int _addAccount(account_t type, currency_t currency, float balance, unsigned int* depositors)
{

	ACCOUNT* A = new ACCOUNT;

	A->AID = _createAID();
	A->type = type;
	A->currency = currency;
	A->balance = balance;
	
	for (int i = 0; i < MAX_CUST_PER_ACCNT; i++)
	{
		//printf("ENTITY add CID -- %d\n", depositors[i]);
		A->depositors[i] = depositors[i];
	}
	A->unfrozen = 1;
	A->open = 1;

	allAccounts->push_back(*A);

	return A->AID;
}

extern "C" BANK_ENTITYCOMPONENT_API int _updateAccount(ACCOUNT* changedAccount)
{
	//if (sizeof(changedAccount->depositors) / sizeof(changedAccount->depositors[0]) > MAX_CUST_PER_ACCNT)
	//	return -1; //provided array of cid is to big

	account_list::iterator it;
	for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	{
		if (it->AID == changedAccount->AID)
		{
			it->type = changedAccount->type;
			it->currency = changedAccount->currency;
			it->balance = changedAccount->balance;

			for (int i = 0; i < sizeof(it->depositors) / sizeof(it->depositors[0]); i++)
				it->depositors[i] = changedAccount->depositors[i];

			it->unfrozen = changedAccount->unfrozen;
			it->open = changedAccount->open;

			return 0;
		}
	}
	
	return -2; //AID not found in list
}

//create new Customer ID
unsigned int _createCID()
{
	//generiert die nächste CID
	if (!allCustomers->empty())

		return allCustomers->back().CID + 1;
	else
		return 1;

}

//create new Account ID
unsigned int _createAID()
{
	//generiert die nächste AID
	if (!allAccounts->empty())

		return allAccounts->back().AID + 1;
	else
		return 1;

}

//Writes Rapid Json Documents into File
int _writeJsonToFile(char* file, Document* json)
{

	FILE* fp;
	errno_t err;
	if((err = fopen_s(&fp,file, "wb")) != 0)
		return -1; // Fail on open file

	char writeBuf[65536];
	FileWriteStream os(fp, writeBuf, sizeof(writeBuf));
	Writer<FileWriteStream> writer(os);
	json->Accept(writer);
	
	fclose(fp);
	return 0;
}

//Reads Json from file into a Rapid Json document
int _readJsonFromFile(char* file, Document* json)
{
	
	FILE* fp;
	errno_t err;
	if ((err = fopen_s(&fp, file, "rb")) != 0)
		return -1; //fail on open file

	char readBuffer[65536];
	FileReadStream is(fp, readBuffer, sizeof(readBuffer));
	json->ParseStream(is);
	fclose(fp);
	return 0;
}

int _writeCustomers()
{
	
	//Create DOM Tree of allCustomers
	Document d;
	d.SetObject();
	//Array that holds Customer Objects
	Value arr(kArrayType);
	
	//iterate over allCustomers List and generate Rapidjson Customer Objects which are placed in array
	customer_list::iterator it;
	for (it = allCustomers->begin(); it != allCustomers->end(); it++)
	{
		Value customer(kObjectType);
		customer.AddMember("CID", it->CID, d.GetAllocator());
		customer.AddMember("FirstName",Value().SetString(it->FirstName,d.GetAllocator()), d.GetAllocator());
		customer.AddMember("LastName", Value().SetString(it->LastName, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("Street", Value().SetString(it->Street, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("StreetNr", Value().SetString(it->StreetNr, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("City", Value().SetString(it->City, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("PostalCode", Value().SetString(it->PostalCode, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("Country", Value().SetString(it->Country, d.GetAllocator()), d.GetAllocator());
		customer.AddMember("Active", it->Active, d.GetAllocator());
		arr.PushBack(customer, d.GetAllocator());
	}
	//add array with all Customer Objects to Document
	d.AddMember("data", arr, d.GetAllocator());

	_writeJsonToFile("allCustomers.txt", &d);
	return 0;
}

int _writeAccounts()
{
	////Create DOM Tree of allAccounts
	//Document d;
	//d.SetObject();
	////Array that holds Account Objects
	//Value arr(kArrayType);

	////iterate over allCustomers List and generate Rapidjson Customer Objects which are placed in array
	//account_list::iterator it;
	//for (it = allAccounts->begin(); it != allAccounts->end(); it++)
	//{
	//	Value customer(kObjectType);
	//	customer.AddMember("AID", it->AID, d.GetAllocator());
	//	customer.AddMember("Balance", Value().SetFloat(it->balance), d.GetAllocator());
	// WIP	
	//	customer.AddMember("Type", Value().Set SetString(it->type, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("Street", Value().SetString(it->Street, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("StreetNr", Value().SetString(it->StreetNr, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("City", Value().SetString(it->City, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("PostalCode", Value().SetString(it->PostalCode, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("Country", Value().SetString(it->Country, d.GetAllocator()), d.GetAllocator());
	//	customer.AddMember("Active", it->Active, d.GetAllocator());
	//	arr.PushBack(customer, d.GetAllocator());
	//}
	////add array with all Customer Objects to Document
	//d.AddMember("data", arr, d.GetAllocator());

	//_writeJsonToFile("allCustomers.txt", &d);
	//return 0;
	return 0;
}

int _readCustomers()
{
	Document d;

	_readJsonFromFile("allCustomers.txt", &d);

	for (Value::ConstValueIterator itr = d["data"].Begin(); itr != d["data"].End(); ++itr)
	{
		
		
		for (Value::ConstMemberIterator itrObject = itr->MemberBegin(); itrObject != itr->MemberEnd(); ++itrObject)
		{
			
			//WIP
			//if (itrObject->value.IsInt()) continue;
			printf("entry -- ");
		}

	}
	return 0;
}

int _readAccounts()
{
	//TODO
	return 0;
}

extern "C" BANK_ENTITYCOMPONENT_API int _addTransaction(unsigned int sourceAID, unsigned int destinationAID, unsigned int ordererCID, float amount)
{
	TRANSACTION* T = new TRANSACTION;

	T->timestamp = _createTimestamp();
	T->sourceAID = sourceAID;
	T->destinationAID = destinationAID;
	T->ordererCID = ordererCID;
	T->amount = amount;
	
	allTransactions->push_back(*T);

	return 0;

}

unsigned long _createTimestamp()
{
	transactionSequenceNumber += 1;
	
	return transactionSequenceNumber;
}

extern "C" BANK_ENTITYCOMPONENT_API int _getTransactions(unsigned int AID, TRANSACTION* transactionList, unsigned int* len)
{
	unsigned int acc = 0;
	unsigned int cnt = 0;

	transaction_list::iterator it;

	for (it = allTransactions->begin(); it != allTransactions->end(); it++)
	{
		cnt+=1;
	}

	//When len 0 is given to function return number of Transactions in allTransactions. 
	//This provides a way for calling function to determine how large the array transactionList has to be.

	//if ((*len) == 0)
	//{
		*len = cnt;
		return 0;
	//}

	for (it = allTransactions->begin(); it != allTransactions->end(); it++)
	{
		//only search for Transactions where AID is present
		if (it->destinationAID == AID || it->sourceAID == AID)
		{
			if (acc == *len)
				return -1; //Transaction list is full but there would be more transactions

			transactionList[acc].timestamp = it->timestamp;
			transactionList[acc].amount = it->amount;
			transactionList[acc].destinationAID = it->destinationAID;
			transactionList[acc].sourceAID = it->sourceAID;
			transactionList[acc].ordererCID = it->ordererCID;
			acc += 1;
		}
		
	}

	return 0;
}