using System.Globalization;

namespace ContosoUniversity.Web.ExtensionMethods;

public static class TokenExtensions
{
    public static string GetLastChars(this byte[] token) => token[7].ToString(CultureInfo.CurrentCulture);
}
