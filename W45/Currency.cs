using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Currency
    {
        public string Code { private set; get; }
        public string Symbol { private set; get; }
        public double ExcangeRateUSD { private set; get; }

        public Currency(string code, string symbol, double excangeRateUSD)
        {
            Code = code;
            Symbol = symbol;
            ExcangeRateUSD = excangeRateUSD;
        }
    }

    internal class Currencies
    {
        private Dictionary<string, Currency> _Currencies;

        public Currencies(Dictionary<string, Currency> currencies)
        {
            _Currencies = currencies;
        }

        //public bool HasCurrency(string code)
        //{
        //    return _list.ContainsKey(code);
        //}

        public bool GetExcangeRateUSD(string code, out double excangeRateUSD)
        {
            if (_Currencies.TryGetValue(code, out var currency))
            {
                excangeRateUSD = currency.ExcangeRateUSD;
                return true;
            }
            excangeRateUSD = 0;
            return false;
        }
    }
}
