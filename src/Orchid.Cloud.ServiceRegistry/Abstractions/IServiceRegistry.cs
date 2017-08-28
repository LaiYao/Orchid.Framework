using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Abstractions;

namespace Orchid.Cloud.ServiceRegistry.Abstractions
{
    public interface IServiceRegistry : INamable
    {
        /// <summary>
        /// 向服务中心注册服务
        /// </summary>
        /// <typeparam name="TService">要注册的服务，通常应该是声明服务契约的接口</typeparam>
        /// <param name="uri">所注册服务的uri，格式必须是{ip:port}</param>
        void Register<TService>(Uri uri);
        /// <summary>
        /// 向服务中心注册服务
        /// </summary>
        /// <param name="uri">所注册服务的uri，格式必须是{ip:port}</param>
        /// <param name="serviceId">服务的全局唯一id，如果没有，则取TService的FullName</param>
        void Register(Uri uri, string serviceId);
        /// <summary>
        /// 注销服务中心的服务
        /// </summary>
        /// <typeparam name="TService">要注册的服务，通常应该是声明服务契约的接口</typeparam>
        void Unregister<TService>(Uri uri);
        /// <summary>
        /// 注销服务中心的服务
        /// </summary>
        /// <param name="uri">所注册服务的uri，格式必须是{ip:port}</param>
        /// <param name="serviceId">服务的全局唯一id，如果没有，则取TService的FullName</param>
        void Unregister(Uri uri, string serviceId);

        IEnumerable<Uri> Lookup<TService>();
        IEnumerable<Uri> Lookup(string serviceId);
    }
}
