using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    /// <summary>
    /// 服务调用的实际处理方，主要职责包括Retry尝试，FAIL策略如Failover, Failfast, Failsafe, Failback, forking等
    /// </summary>
    public interface IInvoker : IInvokerExecutingFilter, IInvokerExecutedFilter
    {
        IInvokerOptions Options { get; }

        object Invoke(params object[] parames);
    }
}
