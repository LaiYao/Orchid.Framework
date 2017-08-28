using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Abstractions;

namespace Orchid.Cloud.ServiceRegistry.Abstractions
{
    public interface IServiceRegistryProvider : INamable
    {
        IServiceRegistry CreateServiceRegistry(RegistryOptions options);
    }
}
