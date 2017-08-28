using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Cloud.ServiceRegistry.Abstractions;

namespace Orchid.Cloud.ServiceRegistry
{
    public class NullServiceRegistry : IServiceRegistry
    {
        public string Name => throw new NotImplementedException();

        public IEnumerable<Uri> Lookup<TService>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Uri> Lookup(string serviceId)
        {
            throw new NotImplementedException();
        }

        public void Register<TService>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void Register(Uri uri, string serviceId)
        {
            throw new NotImplementedException();
        }

        public void Unregister<TService>(Uri uri)
        {
            throw new NotImplementedException();
        }

        public void Unregister(Uri uri, string serviceId)
        {
            throw new NotImplementedException();
        }
    }
}
