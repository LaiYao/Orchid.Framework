using System;

namespace Orchid.DDD.CQRS.Abstractions
{
    public interface IDomainEvent:IEvent
    {
        DateTime OcurrendOn { get; }
    }
}
