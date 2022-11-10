using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Table
    {
        const string ESC = "\u001b";
        const string CSI = "\u001b[";
        private AssetList _Data;

        public Table(ref AssetList data)
        {
            _Data = data;
        }

        public void Display()
        {
            Console.WriteLine();
            Console.WriteLine($"{CSI}36m============================================================================================={CSI}0m");
            Console.WriteLine($"{CSI}97m                                            Asset List");
            Console.WriteLine($"{CSI}36m============================================================================================={CSI}0m");
            Console.WriteLine($"{CSI}32m{"Type",-10}{"Brand",-10}{"Model",-10}{"Office",-10}{"Purchase Date",-15}{"Price/USD",-9}  {"Currency",-10}{"Local Price Today",-17}{CSI}0m");
            Console.WriteLine($"{CSI}36m---------------------------------------------------------------------------------------------{CSI}0m");

            List<Purchase> sortedList = new List<Purchase>();
            _Data.GetOrderedList(ref sortedList);

            int month;
            int color;
            foreach (var l in sortedList)
            {
                month = 36 - _Data.LifeTimeCalcMonth(l.PurchaseDate);
                if (month >= 6)
                    color = 39;
                else if (month < 3)
                    color = 31;
                else
                    color = 33;

                string purchaseDate = _Data.GetPurchaseDateString(l) ; //  l.Item.PurchaseDate.ToString("yyyy-MM-dd");
                double localPurchasePrice = _Data.GetLocalPurchasePrice(l);
                // double localPurchasePrice = l.Item.PurchasePrice * l.Office.Currency.ExcangeRateUSD;
                Console.WriteLine($"{CSI}{color}m{l.Item.Type,-10}{l.Item.Brand,-10}{l.Item.Model,-10}{l.Office.Name,-10}{purchaseDate,-15}" +
                    $"{l.PurchasePrice,9:N2}  {l.Office.CurrencyInfo.Code,-10}{localPurchasePrice,17:N2}");
            }
            Console.WriteLine($"{CSI}36m============================================================================================={CSI}0m");
        }
    }
}
