using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal abstract class Asset
    {
        public string Type { private set; get; }
        public string Brand { private set; get; }
        public string Model { private set; get; }

        public Asset(string type, string brand, string model)
        {
            Type = type;
            Brand = brand;
            Model = model;
        }
    }

    internal class Computer : Asset
    {
        public Computer(string brand, string model) :
                  base("Computer", brand, model)
        {
        }
    }

    internal class Phone : Asset
    {
        public Phone(string brand, string model) :
               base("Phone", brand, model)
        {
        }
    }

    internal abstract class Assets
    {
        // Avalable Asset Types = Possible Object Types
        protected static List<string> _AssetTypes = new() { "Computer", "Phone" };

        // Dictionary of Brand/Model List
        protected Dictionary<string, List<string>> _Assets;
    }

    internal class Computers : Assets
    {
        public Computers()
        {
            _Assets = new Dictionary<string, List<string>>
            {
                {"Asus", new List<string> {"w234" } },
                {"Lenovo", new List<string> {"Yoga 530", "Yoga 730" } },
                {"HP", new List<string> {"Elitebook" } }
            };
        }
    }

    internal class Phones : Assets
    {
        public Phones()
        {
            _Assets = new Dictionary<string, List<string>>
            {
                {"iPhone", new List<string> {"5", "6", "8", "11", "X" } },
                {"Motorola", new List<string> {"Razr" } },
                {"Nokia", new List<string> {"6110" } }
            };
        }
    }
}
