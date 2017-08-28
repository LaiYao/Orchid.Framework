using System;
using Microsoft.Extensions.Caching.Distributed;
using Orchid.Core.Utilities;
using Orchid.Caching.Abstractions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft;
using System.Text;

namespace Orchid.Caching
{
    public class DefaultGlobalCache : ICache
    {
        #region | Fields |

        private IDistributedCache _cache;

        #endregion

        #region | Ctor |

        public DefaultGlobalCache(IDistributedCache cache)
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
                return CacheLevel.Global;
            }
        }

        public bool Contains(string key, string region)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainsAsync(string key, string region)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key, string region)
        {
            var bytes = _cache.Get(key.ComposeCacheKey(region));
            if (bytes == null || bytes.Length == 0) return default(T);

            var value = Encoding.UTF8.GetString(bytes);
            if (value == DefaultCacheManager.FACK_NULL)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<T> GetAsync<T>(string key, string region)
        {
            var bytes = await _cache.GetAsync(key.ComposeCacheKey(region));
            if (bytes == null || bytes.Length == 0) return default(T);

            var value = Encoding.UTF8.GetString(bytes);
            if (value == DefaultCacheManager.FACK_NULL)
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public void Remove(string key, string region)
        {
            _cache.Remove(key.ComposeCacheKey(region));
        }

        public async Task RemoveAsync(string key, string region)
            => await _cache.RemoveAsync(key.ComposeCacheKey(region));

        public void Set<T>(string key, string region, T value, int cacheTime)
        {
            var bytes = value == null ? Encoding.UTF8.GetBytes(DefaultCacheManager.FACK_NULL) : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

            _cache.Set(key.ComposeCacheKey(region), bytes, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(cacheTime)
            });
        }

        public async Task SetAsync<T>(string key, string region, T value, int cacheTime)
        {
            var bytes = value == null ? Encoding.UTF8.GetBytes(DefaultCacheManager.FACK_NULL) : Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value));

            await _cache.SetAsync(key.ComposeCacheKey(region), bytes, new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(cacheTime)
            });
        }

        #endregion
    }
}
