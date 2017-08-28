using System.Collections.Generic;

namespace Orchid.DDD.CQRS.Abstractions
{
    public interface IAggregateRoot<TKey> : Orchid.DDD.Domain.IAggregateRoot<TKey>, IEventPublisher
    {
    }
}
