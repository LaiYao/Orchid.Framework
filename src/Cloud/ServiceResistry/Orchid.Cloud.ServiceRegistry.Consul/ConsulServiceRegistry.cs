using System;
using System.Collections.Generic;
using Consul;
using Orchid.Cloud.ServiceRegistry;
using Orchid.Cloud.ServiceRegistry.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Orchid.Core.Utilities;

namespace Orchid.Cloud.ServiceRegistry.Consol
{
    public class ConsulServiceRegistry : IServiceRegistry
    {
        #region | Fields |

        ConsulClient _client;
        ConsulServiceRegistryOptions _options;
        Dictionary<string, List<Uri>> _cache = new Dictionary<string, List<Uri>>();
        object _cacheLock = new object();
        CancellationToken _watchCancelToken = new CancellationToken();

        #endregion

        #region | Properties |

        public string Name { get; set; } = "consul service registry";

        #endregion

        #region | Ctor |

        public ConsulServiceRegistry(ConsulServiceRegistryOptions options)
        {
            //_client = new ConsulClient(;

            Task.Run((Action)Watch, _watchCancelToken);
        }

        #endregion

        #region | Implemention of IServiceRegistry |

        public void Register<TService>(Uri uri) => Register(uri, typeof(TService).FullName);

        public void Register(Uri uri, string serviceId)
        {
            Check.NotEmpty(serviceId, nameof(serviceId));
            var registration = new AgentServiceRegistration() { ID = serviceId };
            _client.Agent.ServiceRegister(registration);
        }

        public void Unregister<TService>(Uri uri) => Unregister(uri, typeof(TService).FullName);

        public void Unregister(Uri uri, string serviceId)
        {
            Check.NotEmpty(serviceId, nameof(serviceId));
            _client.Agent.ServiceDeregister(serviceId);
        }

        public IEnumerable<Uri> Lookup<TService>() => Lookup(typeof(TService).FullName);

        public IEnumerable<Uri> Lookup(string serviceId)
        {
            //_client.Agent.
            if (!_cache.ContainsKey(serviceId))
            {
                lock (_cacheLock)
                {
                    if (!_cache.ContainsKey(serviceId))
                    {
                        _cache.Add(serviceId, GetServiceUri(serviceId));
                    }
                }
            }

            return _cache[serviceId];
        }

        #endregion

        #region | Helpers |

        void Watch()
        {
            while (true)
            {
                foreach (var serviceId in _cache.Keys)
                {
                    _cache[serviceId] = GetServiceUri(serviceId);
                }
                Thread.Sleep(TimeSpan.FromSeconds(_options.WatchIntervalSeconds));
            }
        }

        List<Uri> GetServiceUri(string serviceId)
        {
            //_client.Agent.Services().Result.
            return null;
        }

        #endregion
    }
}
