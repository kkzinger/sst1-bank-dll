// bank_accounts.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_accounts.h"

extern "C" BANK_ACCOUNTS_API int helloWorld()
{
	return 0;
}

extern "C" BANK_ACCOUNTS_API unsigned int Open(unsigned int CID, char* CurID, char* Type)
{
	//struct holen von entity, pointer darauf returnen (Account*)
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Close(unsigned int AID)
{
	//�ndert die open-Variable im struct auf false
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int getOwners(unsigned int AID)
{
	//gibt eine Liste an Customer* zur�ck
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int addOwners(unsigned int AID, unsigned int CID)
{
	//f�gt der einen Customer* der Liste hinzu
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int removeOwners(unsigned int AID)
{
	//entfernt einen Customer* aus der Liste
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Freeze(unsigned int AID)
{
	//�ndert die frozen-Variable im struct auf true
	return -1;
}

extern "C" BANK_ACCOUNTS_API unsigned int Unfreeze(unsigned int AID)
{
	//�ndert die frozen-Variable im struct auf false
	return -1;
}

char* getType(unsigned int AID)
{
	//gibt den Wert der Typ-Variable zur�ck
	return (char*)AID;
}

bool IsUnFrozen(unsigned int AID)
{
	//gibt den Wert der frozen-Variable zur�ck
	return false;
}

bool IsOpen(unsigned int AID)
{
	//gibt den Wert der open-Variable zur�ck
	return false;
}

unsigned int createAID()
{
	//gibt die n�chste AID zur�ck
	return -1;
}

