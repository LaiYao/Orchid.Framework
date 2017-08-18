using System;
using Microsoft.Framework.Caching.Distributed;
using Orchid.Core.Utilities;
using Orchid.Caching.Abstractions;

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

        public T Get<T>(string key, string region)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key, string region)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, string region, T value, int cacheTime)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
