using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
{
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {
        int Version { get; }
    }

    public interface IAggregateRoot : IAggregateRoot<int>
    {
    }
}
