using System.Text;

namespace TotemAndroid.SGV.Utilities
{
    public static class StringExtensions
    {
        public static string Normalize(this string input)
        {
            var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(input);

            return Encoding.UTF8.GetString(tempBytes).ToLower();
        }
    }
}