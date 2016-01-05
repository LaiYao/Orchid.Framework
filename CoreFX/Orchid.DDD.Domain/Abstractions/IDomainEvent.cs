using System;

namespace Orchid.DDD.Domain
{
    public interface IDomainEvent
    {
        DateTime OcurrendOn { get; }
    }
}
