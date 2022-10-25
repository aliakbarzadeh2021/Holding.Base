using System;
using System.Collections.Generic;

namespace Holding.Base.Cache
{
    internal static class CacheKeyDependency
    {
        public static readonly IDictionary<Type, IList<string>> CacheKeies = new Dictionary<Type, IList<string>>();
        public static void Add<T>(string cacheKey)
        {
            if (CacheKeies.ContainsKey(typeof (T)))
            {
                if (!CacheKeies[typeof (T)].Contains(cacheKey)) CacheKeies[typeof (T)].Add(cacheKey);
            }
            else
            {
                CacheKeies.Add(typeof (T), new List<string> {cacheKey});
            }
        }
    }
}
