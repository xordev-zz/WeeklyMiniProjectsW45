namespace W45
{
    // Generic country info
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

    // List holding countries
    internal class Countries
    {
        private Dictionary<string, Country> _Countries;

        public Countries(Dictionary<string, Country> countries)
        {
            _Countries = countries;
        }

        // Get country name from country code
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

        // Get currency code from from country code
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
