using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Subscribe<T, TH>(Func<TH> handler)
            where T : IEvent
            where TH : IEventHandler<T>;
        void Unsubscribe<T, TH>()
            where T : IEvent
            where TH : IEventHandler<T>;

        void Publish(IEvent @event);
    }
}
