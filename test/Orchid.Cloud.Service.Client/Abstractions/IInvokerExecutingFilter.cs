using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    public interface IInvokerExecutingFilter
    {
        IInvokerContext Context { get; }
        void OnExecuting(IInvokerContext context);
    }
}
