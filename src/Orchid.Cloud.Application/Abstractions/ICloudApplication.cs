using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Orchid.Cloud.Abstractions
{
    public interface ICloudApplication
    {
        IConfigurationRoot Configuration { get; }

        Queue<IMiddleware> Pipeline { get; }

        void Start();

        void ConfigureServices(IServiceCollection services);

        void Configure(IServiceCollection services);
    }
}
