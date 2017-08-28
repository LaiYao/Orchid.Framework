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

        static readonly Dictionary<string, object> _clientCache = new Dictionary<string, object>();
        static readonly object _cacheLock = new object();

        #endregion

        public static Proxy<T> CreateProxy<T>(IInvoker invoker)
        {
            var type = typeof(T);
            var proxyTypeName = PROXY_NAME_PERFIX + type.FullName;
            if (!_clientCache.Keys.Contains(proxyTypeName))
            {
                lock (_cacheLock)
                {
                    if (!_clientCache.Keys.Contains(proxyTypeName))
                    {
                        var client = new Proxy<T>(invoker);
                        _clientCache.Add(proxyTypeName, client);

                    }
                }
            }

            return (Proxy<T>)_clientCache[proxyTypeName];
        }
    }
}