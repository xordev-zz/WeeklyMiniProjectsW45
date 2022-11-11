using CE = UtilityLib.ConsoleExtention;

namespace W45
{
    internal class Table
    {
        private AssetList _Data;

        public Table(ref AssetList data)
        {
            _Data = data;
        }

        // Display table with data
        public void Display()
        {
            int tableWidth = 103;

            Console.WriteLine();
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundCyan);
            CE.WriteLineCenterColor("Asset List", 100, CE.TextFormatting.ForeGroundBrightWhite);
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundCyan);
            CE.WriteLineColor($"{"Type",-10}{"Brand",-10}{"Model",-10}{"Office",-20}{"Purchase Date",-15}{"Price/USD",-9:f2}  " +
                $"{"Currency",-10}{"Local Price Today",-17:f2}", CE.TextFormatting.ForeGroundGreen);
            CE.WriteLineRepeatCharColor("-", tableWidth, CE.TextFormatting.ForeGroundCyan);

            List<Purchase> sortedList = new List<Purchase>();
            _Data.GetOrderedList(ref sortedList);

            int month;
            CE.TextFormatting color;
            foreach (var l in sortedList)
            {
                month = 36 - _Data.LifeTimeCalcMonth(l.PurchaseDate);
                if (month >= 6)
                    color = CE.TextFormatting.ForeGroundDefault;
                else if (month < 3)
                    color = CE.TextFormatting.ForeGroundRed;
                else
                    color = CE.TextFormatting.ForeGroundYellow;

                string purchaseDate = _Data.GetPurchaseDateString(l);
                double localPurchasePrice = _Data.GetLocalPurchasePrice(l);
                string officeName = _Data.GetOfficeName(l);
                string currencyCode = _Data.GetCurrencyCode(l);

                CE.WriteLineColor($"{l.Item.Type,-10}{l.Item.Brand,-10}{l.Item.Model,-10}{officeName,-20}{purchaseDate,-15}" +
                    $"{l.PurchasePrice,9:N2}  {currencyCode,-10}{localPurchasePrice,17:N2}", color);
            }
            CE.WriteLineRepeatCharColor("=", tableWidth, CE.TextFormatting.ForeGroundCyan);
        }
    }
}
