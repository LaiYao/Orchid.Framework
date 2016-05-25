using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Cache
{
    public class CacheClient : ICache
    {
        #region | Implements of ICache |

        public bool Put<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public bool Put(string key, string region, object value)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public object Get(string key, string region)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key, string region)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Clear(string region)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
