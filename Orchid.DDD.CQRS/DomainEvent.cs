using System;

namespace Orchid.DDD.CQRS
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime OcurrendOn
        {
            get;
            protected set;
        }

        public DomainEvent()
        {
            OcurrendOn = DateTime.UtcNow;
        }
    }
}
