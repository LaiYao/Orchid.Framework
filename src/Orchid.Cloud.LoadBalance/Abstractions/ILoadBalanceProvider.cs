using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.LoadBalance.Abstractions
{
    public interface ILoadBalanceProvider
    {
        ILoadBalance Create();
    }
}
