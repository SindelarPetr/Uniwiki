using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Empty<T>() => new T[0];
    }
}
