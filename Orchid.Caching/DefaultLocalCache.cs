using System;
using Microsoft.Framework.Caching.Memory;
using Orchid.Core.Utilities;
using Orchid.Caching.Abstractions;

namespace Orchid.Caching
{
    public class DefaultLocalCache : ICache
    {
        #region | Fields |

        private IMemoryCache _cache;

        #endregion

        #region | Ctor |

        public DefaultLocalCache(IMemoryCache cache)
        {
            Check.NotNull(cache, nameof(cache));
            _cache = cache;
        }

        #endregion

        #region | Members of ICache |

        public CacheLevel CacheLevel
        {
            get
            {
                return CacheLevel.Local;
            }
        }

        public bool Contains(string key, string region)
        {
            object value = null;
            return _cache.TryGetValue(GetActualKey(key, region), out value);
        }

        public T Get<T>(string key, string region)
        {
            return _cache.Get<T>(GetActualKey(key, region));
        }

        public void Remove(string key, string region)
        {
            _cache.Remove(GetActualKey(key, region));
        }

        public void Set<T>(string key, string region, T value, int cacheTime)
        {
            _cache.Set<T>(GetActualKey(key, region), value, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime) });
        }

        #endregion

        public string GetActualKey(string key, string region)
        {
            return region + ":" + key;
        }
    }
}
