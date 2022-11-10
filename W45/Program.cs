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
        {"EUR", new ("EUR", $"€", 1.00)},
        {"SEK", new ("SEK", $"kr", 10.85)},
        {"JPY", new ("JPY", $"¥", 146.43)}
    }
);

Countries countries = new Countries
(
    // Case insensitive keys
    new Dictionary<string, Country>(StringComparer.OrdinalIgnoreCase)
    {
        {"Spain", new ("Spain", currencies.GetCurrencyInfo("EUR"))},
        {"Sweden", new ("Sweden", currencies.GetCurrencyInfo("SEK"))},
        {"Japan", new ("Japan", currencies.GetCurrencyInfo("JPY"))}
    }
);

AssetList assetList = new AssetList();

assetList.Add(new Computer("Asus", "W234"), countries.GetCountry("spain"), "2019-08-10", 100.00);
assetList.Add(new Computer("HP", "Elitebook"), countries.GetCountry("Sweden"), "2022-10-01", 110.00);
assetList.Add(new Computer("Lenovo", "Yoga 530"), countries.GetCountry("Spain"), "2022-12-01", 120.00);
assetList.Add(new Computer("Lenovo", "Yoga 730"), countries.GetCountry("Japan"), "2022-12-01", 130.00);
assetList.Add(new Phone("iPhone", "5"), countries.GetCountry("Japan"), "2020-01-10", 10.00);
assetList.Add(new Phone("iPhone", "11"), countries.GetCountry("Sweden"), "2022-09-01", 20.00);
assetList.Add(new Phone("iPhone", "X"), countries.GetCountry("Japan"), "2020-01-08", 20.00);
assetList.Add(new Phone("Motorola", "Razr"), countries.GetCountry("Japan"), "2020-01-11", 30.00);

Table table = new Table(ref assetList);
table.Display();

Console.ReadKey();

internal class Purchase
{
    public Asset? Item { private set; get; }
    public Country? Office { private set; get; }
    public DateTimeOffset PurchaseDate { private set; get; }
    public double PurchasePrice { private set; get; }

    public Purchase(Asset asset, Country? country, string purchaseDate, double purchasePrice)
    {
        Item = asset;
        Office = country;
        PurchaseDate = DateTimeOffset.Parse(purchaseDate);
        PurchasePrice = purchasePrice;
    }
}

internal class AssetList
{
    private List<Purchase> _List;

    public double GetLocalPurchasePrice (Purchase purchase)
    {
        double purchasePrice = purchase.Item != null ? purchase.PurchasePrice : 0;
        double excangeRateUSD = purchase.Office != null ?
                purchase.Office.CurrencyInfo != null ? purchase.Office.CurrencyInfo.ExcangeRateUSD : 0 
                : 0;
        return purchasePrice* excangeRateUSD;
    }

    public string GetPurchaseDateString(Purchase purchase)
    {
        return $"{purchase.PurchaseDate.ToString("yyyy-MM-dd")}";
    }

    public AssetList()
    {
        _List = new List<Purchase>();
    }

    public void Add(Asset asset, Country? country, string purchaseDate, double purchasePrice)
    {
        _List.Add(new (asset, country, purchaseDate, purchasePrice));
    }

    public void GetOrderedList(ref List<Purchase> list)
    {
        list = _List.OrderBy(o => o.Office != null ? o.Office.Name : "NA").ThenBy(o => o.PurchaseDate).ToList();
    }

    public int LifeTimeCalcMonth(DateTimeOffset dtBase)
    {
        int month;

        DateTimeOffset dtNow = DateTimeOffset.Now;
        month = (dtNow.Year - dtBase.Year) * 12 + dtNow.Month - dtBase.Month + (dtNow.Day >= dtBase.Day ? 0 : -1);

        return month;
    }
}
