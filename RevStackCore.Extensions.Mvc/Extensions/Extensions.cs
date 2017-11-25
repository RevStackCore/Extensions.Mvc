using System;
using System.Collections.Generic;
using Json = RevStackCore.Serialization.Json;
namespace RevStackCore.Extensions.Mvc
{
    public static class MvcExtensions
    {
        public static bool IsDefaultTypeValue<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default(T));
        }

        public static string ToJsonString<T>(this T src)
        {
            if (src.IsDefaultTypeValue()) return null;
            return Json.SerializeObject(src, true, true);
        }
    }
}
