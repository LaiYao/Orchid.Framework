using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
{
    public interface IAggregateRootWithEvent<TKey> : IAggregateRoot<TKey>
    {
        IEnumerable<IDomainEvent> GetUncommettedEvents();

        void ClearUncommettedEvents();
    }
}
