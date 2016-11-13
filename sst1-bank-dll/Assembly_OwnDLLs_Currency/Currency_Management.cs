using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_OwnDLLs_Currency
{
public    class Currency_Management
    {

        public static int addCurrency(string currID, float exchangeRateToEUR)
        {


            return 0;
        }

        public static int changeCurrencyExchangeRate(string currID, float exchangeRateToEUR)
        {


            return 0;
        }

        public static int getCurrencyExchangeRate(string currID, ref float exchangeRate)
        {

            exchangeRate = 1.4242f;

            return 0;
        }


        static void Main(string[] args)
        {
        }
    }
}
