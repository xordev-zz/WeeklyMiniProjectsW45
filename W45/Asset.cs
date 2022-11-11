using System.Xml.Linq;
using System;

namespace W45
{
    // Class holding asset data
    internal abstract class Asset
    {
        public string Type { private set; get; } = string.Empty;
        public string Brand { private set; get; } = string.Empty;
        public string Model { private set; get; } = string.Empty;

        public Asset()
        {
        }
        public Asset(string type, string brand, string model)
        {
            Type = type;
            Brand = brand;
            Model = model;
        }
    }

    // Derived Computer asset
    internal class Computer : Asset
    {
        public Computer()
        {
        }
        public Computer(string brand, string model) :
                  base("Computer", brand, model)
        {
        }
    }

    // Derived Phone asset
    internal class Phone : Asset
    {
        public Phone()
        {
        }
        public Phone(string brand, string model) :
               base("Phone", brand, model)
        {
        }
    }

    // Factory creating objects of type based on a string
    internal static class AssetObjectFactory
    {
        private static Dictionary<string, Type> _ClassDict
            = new(StringComparer.OrdinalIgnoreCase)
            {
                {"Computer", typeof(Computer) },
                {"Phone", typeof(Phone) }
            };

        // Check if it is possible to create a class from specific string
        public static bool HasClass(string name)
        {
            return _ClassDict.ContainsKey(name);
        }

        // Get class from string
        public static Type? GetClass(string name)
        {
            Type T;
            return _ClassDict.TryGetValue(name, out T)
                ? T
                : null;
        }

        // Get all asset types in a string
        public static string GetAssetTypeString()
        {
            return string.Join(", ", _ClassDict.Keys);
        }

        // Instanciate object from string
        public static Asset? GetObject(string type, string brand, string model)
        {
            Type? T = GetClass(type);
            if (T == null)
            {
                return null;
            }
            object? o = Activator.CreateInstance(T, brand, model);
            if (o == null)
            {
                return null;
            }
            return (Asset)o;
        }
    }

    // Generic list of assets types
    internal static class Assets
    {
        public static Dictionary<string, Dictionary<string, List<string>>> _AssetList
            =
            new(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "Computer", new(StringComparer.OrdinalIgnoreCase)
                            {
                                { "Asus", new() { "w234" } },
                                { "Lenovo", new() { "Yoga 530", "Yoga 730" } },
                                { "HP", new() { "Elitebook" } }
                            }
                },
                {
                    "Phone", new(StringComparer.OrdinalIgnoreCase)
                            {
                                { "iPhone", new() { "5", "6", "8", "11", "X" } },
                                { "Motorola", new() { "Razr" } },
                                { "Nokia", new() { "6110" } }
                            }
                }
            };

        // Get string containing all asset types
        public static string GetAssetTypeString()
        {
            return string.Join(", ", _AssetList.Keys);
        }

        // Get string[] containing all asset types
        public static string[] GetAssetTypeStringArray()
        {
            return _AssetList.Keys.ToArray();
        }

        // Get string containing all brands for specific asset type
        public static string GetBrandString(string type)
        {
            if (_AssetList.TryGetValue(type, out var result))
            {

                return string.Join(", ", result.Keys);
            }
            return "NA";
        }

        // Get string[] containing all brands for specific asset type
        public static string[] GetBrandStringArray(string type)
        {
            if (_AssetList.TryGetValue(type, out var result))
            {

                return result.Keys.ToArray();
            }
            return Array.Empty<string>();
        }

        // Get string containing all models for specific brand
        public static string GetModelString(string type, string brand)
        {
            if (_AssetList.TryGetValue(type, out var typeDict))
            {
                if (typeDict.TryGetValue(brand, out var brandList))
                {
                    return string.Join(", ", brandList);
                }
            }
            return "NA";
        }

        // Get string[] containing all models for specific brand
        public static string[] GetModelStringArray(string type, string brand)
        {
            if (_AssetList.TryGetValue(type, out var typeDict))
            {
                if (typeDict.TryGetValue(brand, out var brandList))
                {
                    return brandList.ToArray();
                }
            }
            return Array.Empty<string>();
        }
    }

    // Class holding info of a purchased asset
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

    // Class holding list purchased assets
    internal class AssetList
    {
        private List<Purchase> _PurchaseList;
        private Countries _Countries;
        private Offices _Offices;
        private Currencies _Currencies;

        // Calc local price based of local currency and USD exchange rate
        public double GetLocalPurchasePrice(Purchase purchase)
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

        // Get a formated purchase date
        public string GetPurchaseDateString(Purchase purchase)
        {
            return $"{purchase.PurchaseDate.ToString("yyyy-MM-dd")}";
        }

        // Get name of office from office code
        public string GetOfficeName(Purchase purchase)
        {
            return _Offices.GetOfficeName(purchase.OfficeCode);
        }

        // Get currency code
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

        // Add asset to asset list
        public void Add(Asset asset, string officeCode, string purchaseDate, double purchasePrice)
        {
            _PurchaseList.Add(new(asset, officeCode, purchaseDate, purchasePrice));
        }

        // Get an  ordered asset list
        public void GetOrderedList(ref List<Purchase> list)
        {
            list = _PurchaseList.OrderBy(o => _Offices.GetOfficeName(o.OfficeCode)).ThenBy(o => o.PurchaseDate).ToList();
        }

        // Calc month left to life time expires 
        public int LifeTimeCalcMonth(DateTimeOffset dtBase)
        {
            int month;

            DateTimeOffset dtNow = DateTimeOffset.Now;
            month = (dtNow.Year - dtBase.Year) * 12 + dtNow.Month - dtBase.Month + (dtNow.Day >= dtBase.Day ? 0 : -1);

            return month;
        }
    }
}
