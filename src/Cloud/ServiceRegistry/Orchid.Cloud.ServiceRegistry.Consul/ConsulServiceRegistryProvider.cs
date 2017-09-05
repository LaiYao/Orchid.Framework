using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Cloud.ServiceRegistry.Abstractions;

namespace Orchid.Cloud.ServiceRegistry.Consul
{
    public class ConsulServiceRegistryProvider : IServiceRegistryProvider
    {
        public string Name { get; private set; } = "consulProvider";

        public IServiceRegistry CreateServiceRegistry(RegistryOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
