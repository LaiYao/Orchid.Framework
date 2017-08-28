using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    public interface IInvokerContext
    {
        object[] Arguments { get; }

        MethodInfo Method { get; }

        void Invoke();
    }
}
