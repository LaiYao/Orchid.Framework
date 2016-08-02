using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot<TKey>
    {
        #region | Properties |

        public int Version { get; set; }

        #endregion
    }

    public abstract class AggregateRoot : AggregateRoot<int>, IAggregateRoot
    {
    }
}
