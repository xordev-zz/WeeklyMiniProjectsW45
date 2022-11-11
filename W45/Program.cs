using CE = UtilityLib.ConsoleExtention;
using W45;
using System.Formats.Asn1;

Console.Title = "Asset Tracking";

Console.WriteLine("Week: 44");
Console.WriteLine("Weekly mini project");
Console.WriteLine("Asset Tracking - Level 3");
Console.WriteLine("Björn Savander");

Exercises exercises = new Exercises();
exercises.w45_MiniProjectLevel3();

internal class Exercises
{
    // Currencies. Currency Code as key
    public static Currencies currencies = new Currencies
    (
        new Dictionary<string, Currency>(StringComparer.OrdinalIgnoreCase)
        {
        {"EUR", new ("Euro", $"€", 1)},
        {"SEK", new ("Swedish Krona", $"kr", 10.85)},
        {"JPY", new ("Japanese yen", $"¥", 146.43)}
        }
    );

    // Currencies. Country Code as key
    public static Countries countries = new Countries
    (
        // Case insensitive keys
        new Dictionary<string, Country>(StringComparer.OrdinalIgnoreCase)
        {
        {"ESP", new ("Spain", "EUR")},
        {"SWE", new ("Sweden", "SEK")},
        {"JPN", new ("Japan", "JPY")}
        }
    );

    // Offices, Office code as key
    public static Offices offices = new Offices
    (
        // Case insensitive keys
        new Dictionary<string, Office>(StringComparer.OrdinalIgnoreCase)
        {
        {"ESP1", new ("Spain Office 1", "ESP")},
        {"SWE1", new ("Sweden Office 1", "SWE")},
        {"JPN1", new ("Japan Office 1", "JPN")}
        }
    );

    // List of assets 
    public static AssetList assetList = new AssetList(ref countries, ref offices, ref currencies);

    public Exercises()
    {
        // Preload test data
        assetList.Add(new Computer("Asus", "W234"), "ESP1", "2019-08-10", 100.00);
        assetList.Add(new Computer("HP", "Elitebook"), "SWE1", "2022-10-01", 110.00);
        assetList.Add(new Computer("Lenovo", "Yoga 530"), "ESP1", "2022-12-01", 120.00);
        assetList.Add(new Computer("Lenovo", "Yoga 730"), "JPN1", "2022-12-01", 130.00);
        assetList.Add(new Phone("iPhone", "5"), "JPN1", "2020-01-10", 10.00);
        assetList.Add(new Phone("iPhone", "11"), "SWE1", "2022-09-01", 20.00);
        assetList.Add(new Phone("iPhone", "X"), "JPN1", "2020-01-08", 20.00);
        assetList.Add(new Phone("Motorola", "Razr"), "JPN1", "2020-01-11", 30.00);
    }

    // Menu
    public class Menu
    {
        // Display main menu
        public void DisplayMenu()
        {
            int tableWidth = 50;
            Console.WriteLine();
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);
            CE.WriteLineCenterColor("Main Menu", tableWidth, CE.TextFormatting.ForeGroundBrightWhite);
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);
            Console.WriteLine("'C'\tDisplay currencies");
            Console.WriteLine("'U'\tUpdate currency exchange rates");
            Console.WriteLine("'D'\tDisplay Assets");
            Console.WriteLine("'A'\tAdd Assets");
            Console.WriteLine("'Q'\tQuit");
        }

        // Display currency table
        public void DisplayCurrencyTable()
        {
            int tableWidth = 50;

            Console.WriteLine();
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundCyan);
            CE.WriteLineCenterColor("Currencies", tableWidth, CE.TextFormatting.ForeGroundBrightWhite);
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundCyan);
            CE.WriteLineColor($"{"Code",-10}{"ExchangeRateUSD",-15}", CE.TextFormatting.ForeGroundGreen);
            CE.WriteLineRepeatCharColor("-", tableWidth, CE.TextFormatting.ForeGroundCyan);

            foreach (var currency in currencies.CurrencyList)
            {
                CE.WriteLineColor($"{currency.Key,-10}{currency.Value.ExcangeRateUSD,15:f2}", CE.TextFormatting.ForeGroundBrightWhite);
            }
        }

        // Update currency exchange rate
        // 'q' = Quit
        public void CurrencyUpdateExchangeRate()
        {
            int tableWidth = 50;

            string helpString;
            string[] commands = new[] { "q" };
            string[] validationStrings = Array.Empty<string>();

            while (true)
            {
                Console.WriteLine();
                CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);
                CE.WriteLineCenterColor("Update ExchangeRate. 'Q' = Quit", tableWidth, CE.TextFormatting.ForeGroundBrightWhite);
                CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);

                // Read Asset Type
                helpString = $"(Currency codes: {currencies.GetCurrencyCodeString()})";
                validationStrings = currencies.GetCurrencyCodeStringArray();

                Console.WriteLine();
                string currencyCode = CE.StringReadValidation("Enter asset type: ", CE.TextFormatting.ForeGroundDefault,
                    helpString, CE.TextFormatting.ForeGroundGreen,
                    validationStrings, commands).Trim();
                if (currencyCode.ToLower() == "q")
                {
                    return;
                }

                // Read exchange rate
                double exchangeRate = CE.DoubleReadPositive("Enter Exchange rate: ");
                if (currencies.SetExcangeRateUSD(currencyCode, exchangeRate))
                {
                    CE.WriteLineColor("The currency was successfully updated!", CE.TextFormatting.ForeGroundGreen);
                }
                else
                {
                    CE.WriteLineColor("The currency could not be updated!", CE.TextFormatting.ForeGroundRed);
                }
            }
        }

        // Add assets to list of assets
        // 'q' = Quit
        public void AssetsAdd()
        {
            int tableWidth = 50;
            string helpString;
            string[] commands = new[] { "q" };
            string[] validationStrings = Array.Empty<string>();

            while (true)
            {
                Console.WriteLine();
                CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);
                CE.WriteLineCenterColor("Add new asset 'Q' = Quit", tableWidth, CE.TextFormatting.ForeGroundBrightWhite);
                CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundYellow);

                // Read Asset Type
                helpString = $"(Asset types: {Assets.GetAssetTypeString()})";
                validationStrings = Assets.GetAssetTypeStringArray();

                Console.WriteLine();
                string assetType = CE.StringReadValidation("Enter asset type: ", CE.TextFormatting.ForeGroundDefault,
                    helpString, CE.TextFormatting.ForeGroundGreen,
                    validationStrings, commands).Trim();
                if (assetType.ToLower() == "q")
                {
                    return;
                }

                // Read Brand
                helpString = $"(Brand types: {Assets.GetBrandString(assetType)})";
                validationStrings = Assets.GetBrandStringArray(assetType);

                Console.WriteLine();
                string assetBrand = CE.StringReadValidation("Enter brand: ", CE.TextFormatting.ForeGroundDefault,
                    helpString, CE.TextFormatting.ForeGroundGreen,
                    validationStrings, commands).Trim();
                if (assetBrand.ToLower() == "q")
                {
                    return;
                }

                // Read  Model
                helpString = $"(Model types: {Assets.GetModelString(assetType, assetBrand)})";
                validationStrings = Assets.GetModelStringArray(assetType, assetBrand);

                Console.WriteLine();
                string assetModel = CE.StringReadValidation("Enter model: ", CE.TextFormatting.ForeGroundDefault,
                    helpString, CE.TextFormatting.ForeGroundGreen,
                    validationStrings, commands).Trim();
                if (assetModel.ToLower() == "q")
                {
                    return;
                }

                // Read Office code
                helpString = $"(Office codes: {Offices.GetOfficeCodeString()})";
                validationStrings = Offices.GetOfficeCodeStringArray();

                Console.WriteLine();
                string officeCode = CE.StringReadValidation("Enter office code: ", CE.TextFormatting.ForeGroundDefault,
                    helpString, CE.TextFormatting.ForeGroundGreen,
                    validationStrings, commands).Trim();
                if (officeCode.ToLower() == "q")
                {
                    return;
                }

                // Read purchase date
                Console.WriteLine();
                string purchaseDate = CE.DateTimeOffsetValid("Enter purchase date (YYYY-MM-DD): ");

                // Read purchase prise
                Console.WriteLine();
                double purchasePrice = CE.DoubleReadPositive("Enter purchase price/USD : ");

                // Instanciate object based on string
                Asset? asset = AssetObjectFactory.GetObject(assetType, assetBrand, assetModel);
                if (asset == null)
                {
                    CE.WriteLineColor("Asset type  does NOT exist! Try again!", CE.TextFormatting.ForeGroundRed);
                    continue;
                }
                // Add asset to list
                assetList.Add(asset, officeCode, purchaseDate, purchasePrice);
                CE.WriteLineColor("The product was successfully added!", CE.TextFormatting.ForeGroundGreen);
            }
        }
    }
    public void w45_MiniProjectLevel3()
    {
        Menu menu = new Menu();
        Table table = new Table(ref assetList);

        bool running = true;
        do
        {
            menu.DisplayMenu();

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.C:
                    menu.DisplayCurrencyTable();
                    break;
                case ConsoleKey.U:
                    menu.CurrencyUpdateExchangeRate();
                    break;
                case ConsoleKey.D:
                    table.Display();
                    break;
                case ConsoleKey.A:
                    menu.AssetsAdd();
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.Q:
                    running = false;
                    break;
                default:
                    break;
            }
        }
        while (running);

        Console.WriteLine();
        Console.WriteLine("Done! Press any key to Exit!");
        Console.ReadKey(true);
    }
}
