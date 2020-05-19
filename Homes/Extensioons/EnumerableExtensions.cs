using System.Collections.Generic;

namespace Homes.Extensioons
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> data)
        {
            if (data != null)
                return false;
            return true;
        }

    }
}