using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W45
{
    internal class Office
    {
        public string OfficeName { private set; get; }
        public string CountryCode { private set; get; }

        public Office(string officeName, string countryCode)
        {
            OfficeName = officeName;
            CountryCode = countryCode;
        }
    }

    internal class Offices
    {
        private Dictionary<string, Office> _Offices;

        public Offices(Dictionary<string, Office> offices)
        {
            _Offices = offices;
        }

        public string GetOfficeName(string officeCode)
        {
            if (_Offices.TryGetValue(officeCode, out var office))
            {
                return office.OfficeName;
            }
            return "NA";
        }

        public bool GetCountryCode(string officeCode, out string countryCode)
        {
            if (_Offices.TryGetValue(officeCode, out var office))
            {
                countryCode = office.CountryCode;
                return true;
            }
            countryCode = "NA";
            return false;
        }
    }
}
