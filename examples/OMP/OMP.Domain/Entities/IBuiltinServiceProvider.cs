using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Core.Abstractions;

namespace OMP.Domain.Entities
{
    public interface IProvider : INamable
    {
        ProviderRole Role { get; set; }

        string Description { get; set; }

        string ImplementionTypeName { get; set; }


    }

    public enum ProviderRole
    {
        Loadbalance,
        Logging,
        ServiceRegistry,
        MessageQueue,
        Metrics,
        Protocal
    }
}
