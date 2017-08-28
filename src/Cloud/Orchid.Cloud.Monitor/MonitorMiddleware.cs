using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Cloud.Abstractions;

namespace Orchid.Cloud.Monitor
{
    public class MonitorMiddleware : IMiddleware
    {
        public MonitorMiddleware()
        {
        }

        public Task Invoke(CloudApplicationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
