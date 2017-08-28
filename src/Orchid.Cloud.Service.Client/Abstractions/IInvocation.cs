using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    public interface IInvocation
    {
        MethodInfo Method { get; }
        IClient Client { get; }
        object Invoke(object[] parameters);
    }
}
