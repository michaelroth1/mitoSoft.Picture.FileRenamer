using System;
using System.Globalization;

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
    }
}