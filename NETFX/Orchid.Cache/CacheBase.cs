using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Orchid.Cache
{
    public abstract class CacheBase<T> : ICache<T> where T : class
    {
        #region | Implements of ICache |
        public T this[string key]
        {
            get
            {
                return Get<T>(key);
            }
            set
            {
                Put<T>(key, value);
            }
        }

        public T this[string key, string region]
        {
            get
            {
                return Get(key, region) as T;
            }
            set
            {
                Put(key, region, value);
            }
        }

        public bool Put<T>(string key, T value) where T : class
        {
            return Put(key, "Test", value);
        }

        public abstract bool Put(string key, string region, object value);

        public T Get<T>(string key) where T : class
        {
            return Get(key, "default") as T;
        }

        public abstract object Get(string key, string region);

        public bool Remove(string key)
        {
            return Remove(key, "default");
        }

        public abstract bool Remove(string key, string region);

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Clear(string region)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion

        public virtual string GetDefaultRegion()
        {
            return typeof(T).GUID.ToString();
        }
    }
}
