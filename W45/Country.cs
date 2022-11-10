using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Country
    {
        public string Name { private set; get; }
        public Currency? CurrencyInfo { private set; get; }

        public Country(string name, Currency? currencyInfo)
        {
            CurrencyInfo = currencyInfo;
            Name = name;
        }
    }

    internal class Countries
    {
        protected Dictionary<string, Country> _countries;

        public Countries(Dictionary<string, Country> countries)
        {
            _countries = countries;
        }

        public Country? GetCountry (string name)
        {
            if (_countries.TryGetValue(name, out var country))
            {
                return country;
            }
            return null;
        }
    }
}
