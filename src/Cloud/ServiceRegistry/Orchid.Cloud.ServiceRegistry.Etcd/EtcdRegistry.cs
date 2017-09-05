using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microphone.Etcd;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Orchid.Cloud.ServiceRegistry.Etcd
{
    public class EtcdRegistry : IRegistry
    {
        #region | Fields |

        const string ROOT_KEY = "OrchidCloud";
        const string DEFAULT_VERSION = "v1.0";
        const string PROVIDER_CONTAINER_KEY = "providers";
        const string CONSUMER_CONTAINER_KEY = "consumers";
        const string HOST_ENV_KEY = "OC_HOST_IDENTIFY";
        const string PORT_ENV_KEY = "OC_PORT_IDENTIFY";

        EtcdProvider _client;

        #endregion

        #region | Properties |

        #endregion

        public EtcdRegistry(ILoggerFactory loggerFactory, IOptions<EtcdOptions> optionsAccessor)
        {
            // TODO: Shutdown时应该注销已经注册的服务，该步骤应该定义在CloudApplication.Service中
            _client = new EtcdProvider(loggerFactory, optionsAccessor);
        }

        #region | Members of IRegistry |

        public IEnumerable<Uri> Lookup<TService>()
            => _client.GetServiceInstancesAsync(typeof(TService).FullName)
            .Result
            .Select(_ => new Uri($"{_.Host}:{_.Port}"));

        public IEnumerable<Uri> LookupConsumer<TService>()
            => _client.GetServiceInstancesAsync(typeof(TService).FullName)
            .Result
            .Select(_ => new Uri($"{_.Host}:{_.Port}"));

        public void RegisterConsumer<TService>()
        {
            // TODO: 通过etcd客户端注册服务，其中对用同一个服务来说，端口号可以通过配置平台指定
            var consumerKey = GetConsumerKey<TService>();
            var host = System.Environment.GetEnvironmentVariable(HOST_ENV_KEY);
            var port = System.Environment.GetEnvironmentVariable(PORT_ENV_KEY);
            _client.KeyValuePutAsync(consumerKey, $"{host}:{port}");
        }

        public void RegisterProvider<TService>()
        {
            // TODO: ServiceProviderOptions?
            var providerKey = GetProviderKey<TService>();
            var host = System.Environment.GetEnvironmentVariable(HOST_ENV_KEY);
            var port = System.Environment.GetEnvironmentVariable(PORT_ENV_KEY);
            _client.KeyValuePutAsync(providerKey, $"{host}:{port}");
        }

        public void Subscribe<TService>()
        {
            throw new NotImplementedException();
        }

        public void UnregisterConsumer<TService>()
        {
            throw new NotImplementedException();
        }

        public void UnregisterProvider<TService>()
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TService>()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region | Helper Methods |

        // 为简单化，服务注册的key组成为OrchidCloud/{ServiceName}/{Version}/providers/
        // 实际应该还有服务归类分组等信息
        // 如果存在VersionAttribute修饰，那么反射获取版本信息，否则使用默认版本号
        string GetProviderKey<TService>()
            => $"{ROOT_KEY}/{typeof(TService).FullName}/{DEFAULT_VERSION}/{PROVIDER_CONTAINER_KEY}";

        string GetConsumerKey<TService>()
            => $"{ROOT_KEY}/{typeof(TService).FullName}/{DEFAULT_VERSION}/{CONSUMER_CONTAINER_KEY}";

        #endregion
    }
}
