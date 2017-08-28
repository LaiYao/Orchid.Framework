using System;
using Orchid.Cloud.LoadBalance;
using Orchid.Cloud.LoadBalance.Abstractions;

namespace Orchid.Cloud.LoadBalance.Kube
{
    public class KubeLoadBalance : ILoadBalance
    {
        public string SelectActualUri(string target)
        {
            throw new NotImplementedException();
        }
    }
}
