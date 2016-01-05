using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.DDD.Domain
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
