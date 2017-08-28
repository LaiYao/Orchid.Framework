using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.EventBus.Abstractions
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>(Func<TH> handler)
           where T : IEvent
           where TH : IEventHandler<T>;

        void RemoveSubscription<T, TH>()
             where T : IEvent
           where TH : IEventHandler<T>;

        bool HasSubscriptionsForEvent<T>() where T : IEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<Delegate> GetHandlersForEvent<T>() where T : IEvent;

        IEnumerable<Delegate> GetHandlersForEvent(string eventName);
    }
}
