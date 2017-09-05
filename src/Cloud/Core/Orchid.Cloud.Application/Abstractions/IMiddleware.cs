using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Abstractions
{
    public interface IMiddleware
    {
        Task Invoke(CloudApplicationContext context);
    }
}
