using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime;
using Orchid.Cloud.Abstractions;
using Microsoft.Extensions.Configuration;
using Orchid.Core.Utilities;

namespace Orchid.Cloud.Service.Server
{
    public class ServiceServerMiddleware : IMiddleware
    {
        private readonly IConfiguration _configuration;

        public ServiceServerMiddleware(IConfiguration configuration)
        {
            Check.NotNull(configuration, nameof(configuration));
            _configuration = configuration;
        }

        public Task Invoke(CloudApplicationContext context)
        {
            // TODO: gRpc has a feature named 'intercepter' which is simulate with the middleware, 
            // but for current implemention with csharp, this feature isn't included.

            throw new NotImplementedException();
        }
    }
}
