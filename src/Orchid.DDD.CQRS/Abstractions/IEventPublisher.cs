using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.CQRS.Abstractions
{
    public interface IEventPublisher
    {
        Queue<IDomainEvent> UncommettedEvents { get; }

        void RaiseEvent(IDomainEvent @event);

        void ClearUncommettedEvents();
    }
}
