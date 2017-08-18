using System;

namespace Orchid.DDD.CQRS
{
    public interface IDomainEvent:IEvent
    {
        DateTime OcurrendOn { get; }
    }
}
