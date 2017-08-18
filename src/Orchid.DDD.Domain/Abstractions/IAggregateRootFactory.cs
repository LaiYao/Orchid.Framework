using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
{
    public interface IAggregateRootFactory<TKey>
    {
        T Create<T>() where T : IAggregateRoot<TKey>;
    }

    public interface IAggregateRootFactory
    {
        T Create<T>() where T : IAggregateRoot;
    }
}
