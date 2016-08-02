using System;
using System.Threading;
using Orchid.Core.Utilities;
using Orchid.Caching.Abstractions;

namespace Orchid.Caching
{
    public class DefaultCacheManager : ICacheManager
    {
        #region | Fields |

        protected readonly ReaderWriterLockSlim _rwLock = new ReaderWriterLockSlim();

        protected readonly ICache _localCahce;

        protected readonly ICache _globalCache;

        //private const string FACK_NULL = "__[NULL]__";

        private string _defaultRegion = "__[DEFAULT]__";

        #endregion

        #region | Properties |

        public bool IsThreadSafe { get; private set; }

        #endregion

        #region | Ctor |

        public DefaultCacheManager(ICache localCahce, ICache globalCache, string defaultRegion = "", bool isThreadSafe = true)
        {
            Check.NotNull(localCahce, nameof(localCahce));
            Check.NotNull(globalCache, nameof(globalCache));

            if (localCahce.CacheLevel != CacheLevel.Local)
            {
                // TODO: EXCEPTION
                throw new ArgumentException(string.Empty);
            }

            if (_globalCache.CacheLevel != CacheLevel.Global)
            {
                // TODO: EXCEPTION
                throw new ArgumentException(string.Empty);
            }

            _localCahce = localCahce;
            _globalCache = globalCache;
            IsThreadSafe = isThreadSafe;
            if (string.IsNullOrEmpty(defaultRegion)) _defaultRegion = defaultRegion;
        }

        #endregion

        #region | Members of ICacheManager |

        public virtual void Set<T>(string key, T value, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = default(int?))
        {
            Check.NotEmpty(key, nameof(key));

            var cache = GetCurrentCache(cacheLevel);
            var regionName = string.IsNullOrEmpty(region) ? _defaultRegion : region;

            EnterWriteLock();

            cache.Set(key, regionName, value, cacheTime.HasValue ? cacheTime.Value : 5);

            ExitWriteLock();
        }

        public virtual T Get<T>(string key, Func<T> acquirer, string region = "", CacheLevel cacheLevel = CacheLevel.Global, int? cacheTime = null)
        {
            Check.NotEmpty(key, nameof(key));
            Check.NotNull(acquirer, nameof(acquirer));

            var cache = GetCurrentCache(cacheLevel);
            var regionName = string.IsNullOrEmpty(region) ? _defaultRegion : region;
            if (cache.Contains(key, regionName))
            {
                return cache.Get<T>(key, regionName);
            }

            try
            {
                EnterReadLock();

                if (!cache.Contains(key, regionName))
                {
                    var value = acquirer();
                    this.Set<T>(key, value, regionName, cacheLevel, cacheTime);

                    return value;
                }
            }
            finally
            {
                ExitReadLock();
            }

            return cache.Get<T>(key, regionName);
        }

        public virtual void Remove(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global)
        {
            Check.NotEmpty(key, nameof(key));

            var cache = GetCurrentCache(cacheLevel);
            var regionName = string.IsNullOrEmpty(region) ? _defaultRegion : region;

            EnterWriteLock();

            cache.Remove(key, regionName);

            ExitWriteLock();
        }

        public virtual void RemoveByPattern(string pattern, CacheLevel cacheLevel = CacheLevel.Global)
        {
            throw new NotImplementedException();
        }

        public virtual bool Contains(string key, string region = "", CacheLevel cacheLevel = CacheLevel.Global)
        {
            Check.NotEmpty(key, nameof(key));

            var cache = GetCurrentCache(cacheLevel);
            var regionName = string.IsNullOrEmpty(region) ? _defaultRegion : region;

            return cache.Contains(key, regionName);
        }

        public void ClearAll(CacheLevel cacheLevel = CacheLevel.Global)
        {
            throw new NotImplementedException();
        }

        public void Clear(string region = "", CacheLevel cacheLevel = CacheLevel.Global)
        {
            throw new NotImplementedException();
        }

        public void EnterReadLock()
        {
            if (IsThreadSafe) _rwLock.EnterUpgradeableReadLock();
        }

        public void ExitReadLock()
        {
            if (IsThreadSafe) _rwLock.ExitUpgradeableReadLock();
        }

        public void EnterWriteLock()
        {
            if (IsThreadSafe) _rwLock.EnterWriteLock();
        }

        public void ExitWriteLock()
        {
            if (IsThreadSafe) _rwLock.ExitWriteLock();
        }

        #endregion

        private ICache GetCurrentCache(CacheLevel cacheLevel)
        {
            return cacheLevel == CacheLevel.Local ? _localCahce : _globalCache;
        }
    }
}
