using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Caching.Abstractions
{
    public static class CacheExtension
    {
        public static string ComposeCacheKey(this string key, string region)
        {
            return $"{region}:{key}";
        }
    }
}
