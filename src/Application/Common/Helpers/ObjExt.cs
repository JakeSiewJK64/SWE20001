using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace CleanArchitecture.Application.Common.Helpers
{
    public static class ObjectExt
    {
        public static string ToStringJSON(this object value)
        {
            try
            {
                if (value != null)
                {
                    return JsonSerializer.Serialize(value, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                }
            }
            catch (System.Exception)
            {
            }

            return "";
        }
    }
}
