using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Orchid.Cloud.Service
{
    public class ServiceApplication : CloudApplication
    {
        public override void Configure(IServiceCollection services)
        {
            base.Configure(services);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
        }


    }
}
