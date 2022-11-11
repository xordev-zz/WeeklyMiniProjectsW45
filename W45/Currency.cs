namespace W45
{
    // Generic currency info
    internal class Currency
    {
        public string Name { private set; get; }
        public string Symbol { private set; get; }
        public double ExcangeRateUSD { set; get; }

        public Currency(string name, string symbol, double excangeRateUSD)
        {
            Name = name;
            Symbol = symbol;
            ExcangeRateUSD = excangeRateUSD;
        }
    }

    // List holding currencies
    internal class Currencies
    {
        public Dictionary<string, Currency> CurrencyList { set; get; }

        public Currencies(Dictionary<string, Currency> currencies)
        {
            CurrencyList = currencies;
        }

        // Get exchange rate from currency code
        public bool GetExcangeRateUSD(string code, out double excangeRateUSD)
        {
            if (CurrencyList.TryGetValue(code, out var currency))
            {
                excangeRateUSD = currency.ExcangeRateUSD;
                return true;
            }
            excangeRateUSD = 0;
            return false;
        }

        // Set new exchange rate on "currency code"
        public bool SetExcangeRateUSD(string code, double excahngeRate)
        {
            if (CurrencyList.TryGetValue(code, out var currency))
            {
                currency.ExcangeRateUSD = excahngeRate;
                return true;
            }
            return false;
        }

        // Get string of all currency codes
        public string GetCurrencyCodeString()
        {
            return string.Join(", ", CurrencyList.Keys);
        }

        // Get string[] of all currency codes
        public string[] GetCurrencyCodeStringArray()
        {
            return CurrencyList.Keys.ToArray();
        }
    }
}
