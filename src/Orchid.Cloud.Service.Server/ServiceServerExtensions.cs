using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Abstractions;
using Orchid.Cloud.Abstractions;
using Microsoft.Extensions.Configuration;
using Orchid.Core.Utilities;

namespace Orchid.Cloud.Service.Server
{
    public static class ServiceServerExtensions
    {
        public static ICloudApplicationBuilder UseServiceServer(this ICloudApplicationBuilder builder,IConfiguration configuration)
        {
            return builder.Use(new ServiceServerMiddleware(configuration));
        }
    }
}
