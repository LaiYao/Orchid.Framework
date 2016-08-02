using System.Collections.Generic;

namespace Orchid.DDD.CQRS
{
    public interface IAggregateRoot<TKey> : Orchid.DDD.Domain.IAggregateRoot<TKey>
    {
        IEnumerable<IDomainEvent> GetUncommettedEvents();

        void ClearUncommettedEvents();
    }
}
