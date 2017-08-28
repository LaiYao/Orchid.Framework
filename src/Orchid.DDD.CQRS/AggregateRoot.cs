using System;
using System.Collections.Generic;
using Orchid.DDD.CQRS.Abstractions;

namespace Orchid.DDD.CQRS
{
    public abstract class AggregateRoot<TKey> : Orchid.DDD.Domain.AggregateRoot<TKey>, IAggregateRoot<TKey>
    {
        #region | Properties |

        public Queue<IDomainEvent> UncommettedEvents { get; private set; } = new Queue<IDomainEvent>();

        #endregion

        public virtual void RaiseEvent(IDomainEvent @event)
            => UncommettedEvents.Enqueue(@event);

        public virtual void ClearUncommettedEvents()
            => UncommettedEvents.Clear();
    }

    public abstract class AggregateRoot : AggregateRoot<Guid>
    {
    }
}
