using Orchid.Cloud.ServiceRegistry.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.ServiceRegistry.Default
{
    public class DefaultRegistry : IServiceRegistry
    {
        public DefaultRegistry()
        {
        }

        public List<Uri> Lookup(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void Register(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Uri uri, INotifyListener listener)
        {
            throw new NotImplementedException();
        }

        public void Unregister(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(Uri uri, INotifyListener listener)
        {
            throw new NotImplementedException();
        }
    }
}
