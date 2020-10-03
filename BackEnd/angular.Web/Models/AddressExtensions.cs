namespace angular.Web.Models
{
    public static class AddressExtensions
    {
        public static bool IsAlikeTo(this Address address, string StreetAddress, string City, string State, string ZipCode, string Country)
        {
            return (address == null)
                ? false
                : AreSameNormalized(address.StreetAddress, StreetAddress)
                    && AreSameNormalized(address.City, City)
                    && AreSameNormalized(address.State, State)
                    && AreSameNormalized(address.ZipCode, ZipCode)
                    && AreSameNormalized(address.Country, Country);
        }

        private static string Normalize(string s)
        {
            return string.IsNullOrEmpty(s) ? s : s.ToUpper().Replace('.', ' ').Replace("  ", " ").Trim();
        }

        private static bool AreSameNormalized(string a, string b)
        {
            return Normalize(a) == Normalize(b);
        }

    }
}
