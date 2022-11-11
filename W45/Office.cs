namespace W45
{
    // Generic office info
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

    // List holding  info of offices
    internal class Offices
    {
        private static Dictionary<string, Office> _Offices;

        public Offices(Dictionary<string, Office> offices)
        {
            _Offices = offices;
        }

        // Get office name from office code
        public string GetOfficeName(string officeCode)
        {
            if (_Offices.TryGetValue(officeCode, out var office))
            {
                return office.OfficeName;
            }
            return "NA";
        }

        // Get country code from office code
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

        // Get string of all office codes
        public static string GetOfficeCodeString()
        {
            return string.Join(", ", _Offices.Keys);
        }

        // Get string[] of all office codes
        public static string[] GetOfficeCodeStringArray()
        {
            return _Offices.Keys.ToArray();
        }
    }
}
