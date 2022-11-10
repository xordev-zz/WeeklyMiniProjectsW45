using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Country
    {
        public string CountryName { private set; get; }
        public string CurrencyCode { private set; get; }

        public Country(string countryName, string currencyCode)
        {
            CountryName = countryName;
            CurrencyCode = currencyCode;
        }

    }

    internal class Countries
    {
        private Dictionary<string, Country> _Countries;

        public Countries(Dictionary<string, Country> countries)
        {
            _Countries = countries;
        }

        public bool GetCountryName(string countryCode, out string countryName)
        {
            if (_Countries.TryGetValue(countryCode, out var country))
            {
                countryName = country.CountryName;
                return true;
            }
            countryName = "NA";
            return false;
        }

        public bool GetCurrencyCode(string countryCode, out string currencyCode)
        {
            if (_Countries.TryGetValue(countryCode, out var country))
            {
                currencyCode = country.CurrencyCode;
                return true;
            }
            currencyCode = "NA";
            return false;
        }
    }
}
