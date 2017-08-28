using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    public interface IExecutedFilter
    {
        void OnExecuted(IInvocation invocation, object[] parameters);
    }
}
