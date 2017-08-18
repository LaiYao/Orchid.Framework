using System.Collections.Generic;

namespace Orchid.DDD.CQRS
{
    public abstract class AggregateRoot<TKey> : Orchid.DDD.Domain.AggregateRoot<TKey>, IAggregateRoot<TKey>
    {
        #region | Fields |

        Queue<IDomainEvent> _uncommettedEvents;

        #endregion

        #region | Properties |

        #endregion

        #region | Ctor |

        public AggregateRoot()
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
