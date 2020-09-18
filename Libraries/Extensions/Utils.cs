using System;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class Utils
    {
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static bool HasValueGreaterThan(this int? data, int valueToCheck)
        {
            return data != null && data > valueToCheck;
        }

        public static bool HasValueGreaterThan(this double? data, double valueToCheck)
        {
            return data != null && data > valueToCheck;
        }

        public static bool HasValueGreaterThan(this decimal? data, decimal valueToCheck)
        {
            return data != null && data > valueToCheck;
        }

        public static bool HasValueGreaterThan(this short? data, short valueToCheck)
        {
            return data != null && data > valueToCheck;
        }
    }
}
