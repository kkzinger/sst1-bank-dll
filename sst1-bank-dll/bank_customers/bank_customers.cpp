// bank_customers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_customers.h"


extern "C" BANK_CUSTOMERS_API int Create(char * FirstName, char * LastName, char * Street, char * StreetNr, char * City, char * PostalCode, char * Country)
{
	/*struct holen von entity, pointer darauf returnen (CUSTOMER*)*/
	if ((strlen(FirstName) >= 2) && (strlen(LastName) >= 2) && (strlen(Street) >= 3) && (strlen(StreetNr) >= 1) && (strlen(City) >= 3) && (strlen(PostalCode) >= 4) && (strlen(Country) >= 3))
	{
		//n�chste CID generieren
		//unsigned int newCID = _createCID();
		//Pointer aller Customers holen
		//customer_list* cl = helloWorld(1, newCID, FirstName, LastName, Street, StreetNr, City, PostalCode, Country);
		//aktuellen Customer speichern
		//customer_list::const_iterator iterator;
		//for (iterator = (*cl).begin(); iterator != (*cl).end(); iterator++)
		//{
		//	if((*iterator).CID==newCID)
		//		newCustomer = (*iterator);
		//}
		if(_addCustomer(FirstName, LastName, Street, StreetNr, City, PostalCode, Country) != 0)
			return -2; //Something gone wrong in Entity Component

		return  0;
	}
	return -1; // Data did not meet required format/standards
}


extern "C" BANK_CUSTOMERS_API int Read(unsigned int CID, CUSTOMER* resultCustomer)
{
	if(_getCustomerByCID(CID, resultCustomer) != 0)
		return -2; //Something gone wrong in Entity Component

	return 0;
	
	//gibt ausgelesene Variablen des struct zur�ck
	//return Update(CID, "", "", "", "", "", "", "", 2);
}

extern "C" BANK_CUSTOMERS_API int Update(unsigned int CID, char* FirstName, char* LastName, char* Street, char* StreetNr, char* City, char* PostalCode, char* Country)
{
	CUSTOMER C;
	if(_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component


	if (strlen(FirstName) >= 2)
		C.FirstName = FirstName;

	if (strlen(LastName) >= 2)
		C.LastName = LastName;

	if (strlen(Street) >= 3)
		C.Street = Street;

	if (strlen(StreetNr) >= 1)
		C.StreetNr = StreetNr;

	if (strlen(City) >= 3)
		C.City = City;

	if (strlen(PostalCode) >= 4)
		C.PostalCode = PostalCode;

	if (strlen(Country) >= 3)
		C.Country = Country;


		
		if (_updateCustomer(&C) != 0)
			return -2; //Something in went wrong in Entity Component
		
	return 0;

}

extern "C" BANK_CUSTOMERS_API int Activate(unsigned int CID)
{
	//�ndert die active-Variable auf true, 0 bei success
	
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component
	
	C.Active = 1;

	if (_updateCustomer(&C) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards

	//return Update(CID, "", "", "", "", "", "", "", 1);
}
extern "C" BANK_CUSTOMERS_API int Deactivate(unsigned int CID)
{
	//�ndert die active-Variable auf false, 0 bei success
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component

	C.Active = 0;

	if (_updateCustomer(&C) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards



	//return Update(CID, "", "", "", "", "", "", "", 0);
}

unsigned int _IsActive(unsigned int CID)
{
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component

	if (C.Active)
		return 0;  // Customer is active
	else
		return -1; // Customer is not active
}

