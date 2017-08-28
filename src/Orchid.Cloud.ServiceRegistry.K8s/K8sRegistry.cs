using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.ServiceRegistry.K8s
{
    public class K8sRegistry : IRegistry
    {
        public IEnumerable<Uri> LookupConsumer<TService>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Uri> LookupProvider<TService>()
        {
            throw new NotImplementedException();
        }

        public void RegisterConsumer<TService>()
        {
            throw new NotImplementedException();
        }

        public void RegisterProvider<TService>()
        {
            throw new NotImplementedException();
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
    }
}
