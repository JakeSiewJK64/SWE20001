using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace CleanArchitecture.Application.Common.Helpers
{
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ConvertNullOrEmptyTo(this string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static List<string> ToStringList(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return value.Split(";").ToList();
                }
            }
            catch (System.Exception)
            {
            }

            return new List<string>();
        }

        public static T ToObject<T>(this string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    return JsonSerializer.Deserialize<T>(value, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
            }
            catch (System.Exception)
            {
            }

            return default;
        }

        public static string NullToDash(this string value)
        {
            return string.IsNullOrEmpty(value) ? "-" : value;
        }
    }
}
