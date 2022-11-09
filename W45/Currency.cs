using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Currency
    {
        public string Code;
        public string Symbol;
        public double ExcangeRateUSD;

        public Currency(string code, string symbol, double excangeRateUSD)
        {
            Code = code;
            Symbol = symbol;
            ExcangeRateUSD = excangeRateUSD;
        }
    }

    internal class Currencies
    {
        private Dictionary<string, Currency> _list;

        public Currencies(Dictionary<string, Currency> list)
        {
            _list = list;
        }

        public Currency? GetCurrency(string code)
        {
            if (_list.TryGetValue(code, out Currency? currency))
            {
                return currency;
            }
            return null;
        }
    }
}
