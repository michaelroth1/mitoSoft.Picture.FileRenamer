using System;
using System.Globalization;
using System.Linq;

namespace mitoSoft.Common.Media.Extensions
{
    internal static class StringExtensions
    {
        public static DateTime ConvertToDateTime(this string value, string format)
        {
            try
            {
                var provider = CultureInfo.InvariantCulture;
                var convertedDate = DateTime.ParseExact(value, format, provider);
                return convertedDate;
            }
            catch (FormatException)
            {
                throw new FormatException(String.Format("'{0}' is not convertable.", value));
            }
        }

        public static string CleanUp(this string value)
        {
            value = value.CleanUp(Convert.ToChar(0x200e));
            value = value.CleanUp(Convert.ToChar(0x200f));
            return value;
        }

        public static string CleanUp(this string value, char charToCleanUp)
        {
            value = new string(value.Where(c => !char.IsControl(c)).ToArray());
            value = value.Replace(charToCleanUp, Convert.ToChar("#"));
            return value.Replace("#", "");
        }
    }
}