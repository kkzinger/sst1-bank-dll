// sst1-ws16-lb -- bank dlls
// Authors: Tobias Mayer      
//         Gerold Katzinger

// bank_customers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "bank_customers.h"


extern "C" BANK_CUSTOMERS_API int Create(const char* FirstName, const char* LastName, const char* Street, const char* StreetNr, const char* City, const char* PostalCode, const char* Country)
{
	/*struct holen von entity, pointer darauf returnen (CUSTOMER*)*/
	if ((strlen(FirstName) >= 2) && (strlen(LastName) >= 2) && (strlen(Street) >= 3) && (strlen(StreetNr) >= 1) && (strlen(City) >= 3) && (strlen(PostalCode) >= 4) && (strlen(Country) >= 3))
	{
		
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
	
	//gibt ausgelesene Variablen des struct zurück

}

extern "C" BANK_CUSTOMERS_API int Update(unsigned int CID, const char* FirstName, const char* LastName, const char* Street, const char* StreetNr, const char* City, const char* PostalCode, const char* Country)
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
	//ändert die active-Variable auf true, 0 bei success
	
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component
	
	C.Active = 1;

	if (_updateCustomer(&C) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards

}
extern "C" BANK_CUSTOMERS_API int Deactivate(unsigned int CID)
{
	//ändert die active-Variable auf false, 0 bei success
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component

	C.Active = 0;

	if (_updateCustomer(&C) != 0)
		return -2; //Something in went wrong in Entity Component
	return 0;


	return -1; // Data did not meet required format/standards




}

unsigned int _IsActive(unsigned int CID)
{
	CUSTOMER C;
	if (_getCustomerByCID(CID, &C) != 0)
		return -2; //Something gone wrong in Entity Component

	if (C.Active)
		return 1;  // Customer is active
	else
		return 0; // Customer is not active
}

