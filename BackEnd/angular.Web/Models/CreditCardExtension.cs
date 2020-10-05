namespace angular.Web.Models
{
    public static class CreditCardExtensions
    {
        public static bool IsAlikeTo(this CreditCard CreditCard, string Name, string Number, int ExpMonth, int ExpYear, string CVV)
        {
            return (CreditCard == null)
                ? false
                : AreSameNormalized(CreditCard.Name, Name)
                    && AreSameNormalized(CreditCard.Number, Number)
                    && CreditCard.ExpMonth == ExpMonth
                    && CreditCard.ExpYear == ExpYear
                    && AreSameNormalized(CreditCard.CVV, CVV);
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
