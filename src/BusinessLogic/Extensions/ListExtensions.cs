using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> me) => !me?.Any() ?? true;
    }
}