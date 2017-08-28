using System;
using System.Collections.Generic;
using System.Text;
using Orchid.Core.Abstractions;

namespace Orchid.EventBus.Abstractions
{
    public interface IEvent : IHasKey<Guid>
    {
        DateTime OccurredOn { get; }
    }
}
