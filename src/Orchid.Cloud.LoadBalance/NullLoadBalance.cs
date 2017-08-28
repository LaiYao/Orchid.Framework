using System;
using Orchid.Cloud.LoadBalance.Abstractions;

namespace Orchid.Cloud.LoadBalance
{
    /// <summary>
    /// 负载均衡器的默认实现，将直接返回target作为实际访问地址
    /// </summary>
    public class NullLoadBalance : ILoadBalance
    {
        public string SelectActualUri(string target) => target;
    }
}
