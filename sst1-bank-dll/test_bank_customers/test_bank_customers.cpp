// test_bank_customers.cpp : Defines the entry point for the console application.
//$(SolutionDir)\Release\bank_customers.lib;


#include "stdafx.h"
#include <iostream>
#include <string>

#include "../bank_customers/bank_customers.h"
using namespace std;
int main()
{
	Create("Tobias", "Mayer", "Str.", "17a", "Neumarkt", "5202", "AUSTRIA");
    return 0;
}

