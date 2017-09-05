using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Cloud.ServiceRegistry.Abstractions;
using Orchid.Cloud.ServiceRegistry.Consol;

namespace Orchid.Cloud.ServiceRegistry
{
    public static class ConsulServiceRegistryExtensions
    {
        public static IServiceCollection AddConsulRegistry(this IServiceCollection services, ConsulServiceRegistryOptions options)
        {
            return services.AddSingleton<IServiceRegistry>(new ConsulServiceRegistry(options));
        }
    }
}
