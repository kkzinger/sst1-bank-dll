// bank_entitycomponent.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_entitycomponent.h"

extern "C" BANK_ENTITYCOMPONENT_API CUSTOMER * getAccountsByCID(unsigned int cid)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API ACCOUNT * getCustomersByAID(unsigned int aid)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API CUSTOMER getCustomerByCID(unsigned int cid)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API ACCOUNT getAccountByAID(unsigned int aid)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API char * _getTime()
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeLog(char * message)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API int _writeJournal(char * message)
{
	return nullptr;
}

extern "C" BANK_ENTITYCOMPONENT_API char * _readJournal(char * FromTime, char * ToTime, char * Filter, char * DebugLevel)
{
	return nullptr;
}
