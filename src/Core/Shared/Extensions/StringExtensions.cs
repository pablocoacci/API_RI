using System.Text.RegularExpressions;

namespace Core.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string ScapeTokenUrl(this string token)
            => Regex.Escape(token).Replace("\\+", "\\_");

        public static string UnScapeTokenUrl(this string tokenEscaped)
        {
            var aux = tokenEscaped.Replace("\\_", "\\+");
            return Regex.Unescape(aux);
        }
    }
}
