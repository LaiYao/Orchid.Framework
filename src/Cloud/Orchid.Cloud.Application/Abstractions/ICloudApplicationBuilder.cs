using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Abstractions
{
    public interface ICloudApplicationBuilder
    {
        IServiceProvider ApplicationServices { get; set; }

        IDictionary<string, object> Properties { get; }

        RequestDelegate Build();

        ICloudApplicationBuilder New();
        
        ICloudApplicationBuilder Use(IMiddleware middleware);
    }
}
