using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
{
    public abstract class AggregateRootWithEvent<TKey> : AggregateRoot<TKey>, IAggregateRootWithEvent<TKey>
    {
        #region | Fields |

        Queue<IDomainEvent> _uncommettedEvents;

        #endregion

        #region | Properties |

        #endregion

        #region | Ctor |

        public AggregateRootWithEvent()
        {
            Initialize();
        }

        #endregion

        protected virtual void RaiseEvent(IDomainEvent @event)
            => _uncommettedEvents.Enqueue(@event);

        protected virtual void Initialize()
            => _uncommettedEvents = new Queue<IDomainEvent>();

        public void ClearUncommettedEvents()
            => _uncommettedEvents.Clear();

        public IEnumerable<IDomainEvent> GetUncommettedEvents()
            => _uncommettedEvents;
    }
}
