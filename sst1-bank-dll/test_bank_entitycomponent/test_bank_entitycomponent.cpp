// test_bank_entitycomponent.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../bank_entitycomponent/bank_entitycomponent.h"


int main()
{
	_initEntity();
	_addCustomer("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	_addCustomer("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	_addCustomer("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
	_addCustomer("Tobias", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA");

	CUSTOMER C = {2, "Tobi", "Mayer", "Salzburger Str.", "17a", "Neumarkt", "5202", "AUSTRIA" ,0};
	_updateCustomer(&C);
	printf("Pointer to allCustomers: %p", _getCustomerListPtr());

	customer_list::const_iterator it;
	customer_list* cl = _getCustomerListPtr();
	for (it = cl->begin(); it != cl->end(); it++)
	{
		printf("\n%i %p %s %s %s %s %s %s %s %i \n\n", it->CID, it, it->FirstName, it->LastName, it->Street, it->StreetNr, it->City, it->PostalCode, it->Country, it->Active);

	}
	printf("Pointer to allCustomers: %p", _getCustomerListPtr());

	CUSTOMER  ptr;
	_getCustomerByCID(2,&ptr);
	printf("\n%i %s %s %s %s %s %s %s %i \n\n", ptr.CID, ptr.FirstName, ptr.LastName, ptr.Street, ptr.StreetNr, ptr.City, ptr.PostalCode, ptr.Country, ptr.Active);

	printf("Timestamp for Transaction Log: %s", _createTimestamp());

	_writeLog();

}

