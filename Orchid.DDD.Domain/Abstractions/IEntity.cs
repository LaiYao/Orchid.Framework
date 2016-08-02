using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orchid.Core.Abstractions;

namespace Orchid.DDD.Domain
{
    public interface IEntity<TKey> : IHasKey<TKey>
    {
        bool IsTransient();
    }
}
