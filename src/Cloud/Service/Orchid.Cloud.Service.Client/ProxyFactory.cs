using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using Orchid.Cloud.Service.Client.Abstractions;

namespace Orchid.Cloud.Service.Client
{
    public static class ProxyFactory
    {
        #region | Fields |

        internal static readonly string PROXY_NAME_PERFIX = "__PROXY__";

        static readonly Dictionary<string, object> _proxyCache = new Dictionary<string, object>();
        static readonly object _cacheLock = new object();

        #endregion

        public static Proxy<T> CreateProxy<T>(IClient client)
        {
            var type = typeof(T);
            var proxyTypeName = PROXY_NAME_PERFIX + type.FullName;
            if (!_proxyCache.Keys.Contains(proxyTypeName))
            {
                lock (_cacheLock)
                {
                    if (!_proxyCache.Keys.Contains(proxyTypeName))
                    {
                        var proxy = new Proxy<T>(client);
                        _proxyCache.Add(proxyTypeName, proxy);
                    }
                }
            }

            return (Proxy<T>)_proxyCache[proxyTypeName];
        }
    }
}