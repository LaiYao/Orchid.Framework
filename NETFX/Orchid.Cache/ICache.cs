using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Cache
{
    public interface ICache
    {
        bool Put<T>(string key, T value);

        bool Put(string key, string region, object value);

        T Get<T>(string key);

        object Get(string key, string region);

        bool Remove(string key);

        bool Remove(string key, string region);

        void Clear();

        void Clear(string region);
    }

    public interface ICache<T> : ICache, IDisposable where T:class
    {
        T this[string key] { get; set; }

        T this[string key, string region] { get; set; }
    }
}
