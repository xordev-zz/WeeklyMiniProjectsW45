using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using W45;

Currencies currencies = new Currencies
(
    new Dictionary<string, Currency>
    {
        {"EUR", new ("Euro", $"€", 1.00)},
        {"SEK", new ("Swedish Krona", $"kr", 10.85)},
        {"JPY", new ("Japanese yen", $"¥", 146.43)}
    }
);

Countries countries = new Countries
(
    // Case insensitive keys
    new Dictionary<string, Country>(StringComparer.OrdinalIgnoreCase)
    {
        {"ESP", new ("Spain", "EUR")},
        {"SWE", new ("Sweden", "SEK")},
        {"JPN", new ("Japan", "JPY")}
    }
);

Offices offices = new Offices
(
    // Case insensitive keys
    new Dictionary<string, Office>(StringComparer.OrdinalIgnoreCase)
    {
        {"ESP1", new ("Spain Office 1", "ESP")},
        {"SWE1", new ("Sweden Office 1", "SWE")},
        {"JPN1", new ("Japan Office 1", "JPN")}
    }
);



AssetList assetList = new AssetList(ref countries, ref offices, ref currencies);
assetList.Add(new Computer("Asus", "W234"), "ESP1", "2019-08-10", 100.00);
assetList.Add(new Computer("HP", "Elitebook"), "SWE1", "2022-10-01", 110.00);
assetList.Add(new Computer("Lenovo", "Yoga 530"), "ESP1", "2022-12-01", 120.00);
assetList.Add(new Computer("Lenovo", "Yoga 730"), "JPN1", "2022-12-01", 130.00);
assetList.Add(new Phone("iPhone", "5"), "JPN1", "2020-01-10", 10.00);
assetList.Add(new Phone("iPhone", "11"), "SWE1", "2022-09-01", 20.00);
assetList.Add(new Phone("iPhone", "X"), "JPN1", "2020-01-08", 20.00);
assetList.Add(new Phone("Motorola", "Razr"), "JPN1", "2020-01-11", 30.00);

Table table = new Table(ref assetList);
table.Display();

Console.ReadKey();

internal class Purchase
{
    public Asset? Item { private set; get; }
    public string OfficeCode { private set; get; }
    public DateTimeOffset PurchaseDate { private set; get; }
    public double PurchasePrice { private set; get; }
    public Purchase(Asset asset, string officeCode, string purchaseDate, double purchasePrice)
    {
        Item = asset;
        OfficeCode = officeCode;
        PurchaseDate = DateTimeOffset.Parse(purchaseDate);
        PurchasePrice = purchasePrice;
    }
}

internal class AssetList
{
    private List<Purchase> _PurchaseList;
    private Countries _Countries;
    private Offices _Offices;
    private Currencies _Currencies;

    public double GetLocalPurchasePrice (Purchase purchase)
    {
        double excangeRateUSD = 0;

        string countryCode; 
        if (_Offices.GetCountryCode(purchase.OfficeCode, out countryCode))
        {
            string currencyCode;
            if (_Countries.GetCurrencyCode(countryCode, out currencyCode))
            {
                if (_Currencies.GetExcangeRateUSD(currencyCode, out excangeRateUSD))
                {
                }
            }
        }
        return purchase.PurchasePrice * excangeRateUSD;
    }

    public string GetPurchaseDateString(Purchase purchase)
    {
        return $"{purchase.PurchaseDate.ToString("yyyy-MM-dd")}";
    }
    public string GetOfficeName(Purchase purchase)
    {
        return _Offices.GetOfficeName(purchase.OfficeCode);
    }
    public string GetCurrencyCode(Purchase purchase)
    {
        string countryCode;
        if (_Offices.GetCountryCode(purchase.OfficeCode, out countryCode))
        {
            string currencyCode;
            if (_Countries.GetCurrencyCode(countryCode, out currencyCode))
            {
                return currencyCode;
            }
        }
        return "NA";
    }

    public AssetList(ref Countries countries, ref Offices offices, ref Currencies currencies)
    {
        _PurchaseList = new List<Purchase>();
        _Countries = countries;
        _Offices = offices;
        _Currencies = currencies;
    }

    public void Add(Asset asset, string officeCode, string purchaseDate, double purchasePrice)
    {
        _PurchaseList.Add(new(asset, officeCode, purchaseDate, purchasePrice));
    }

    public void GetOrderedList(ref List<Purchase> list)
    {
        list = _PurchaseList.OrderBy(o => _Offices.GetOfficeName(o.OfficeCode)).ThenBy(o => o.PurchaseDate).ToList();
//        list = _List.OrderBy(o => o.OfficeInfo != null ? o.OfficeInfo.Name : "NA").ThenBy(o => o.PurchaseDate).ToList();
    }

    public int LifeTimeCalcMonth(DateTimeOffset dtBase)
    {
        int month;

        DateTimeOffset dtNow = DateTimeOffset.Now;
        month = (dtNow.Year - dtBase.Year) * 12 + dtNow.Month - dtBase.Month + (dtNow.Day >= dtBase.Day ? 0 : -1);

        return month;
    }
}
