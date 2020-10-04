using System;
using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Empty<T>() => new T[0];

        public static void ForEach<T>(
            this IEnumerable<T> source,
            Action<T> action)
        {
            foreach (var element in source)
            {
                action(element);
            }
        }
    }
}
