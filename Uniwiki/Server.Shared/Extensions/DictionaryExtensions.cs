using System.Collections.Generic;

namespace Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static TVal GetOrDefault<TKey, TVal>(this IDictionary<TKey, TVal> dictionary, TKey key, TVal defaultVal = default(TVal))
            => dictionary.TryGetValue(key, out var val) ? val : defaultVal;
    }
}
