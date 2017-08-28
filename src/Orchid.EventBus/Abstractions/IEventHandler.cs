using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.EventBus.Abstractions
{
    public interface IEventHandler<IEvent>
    {
        void Handle(IEvent @event);

        Task HandleAsync(IEvent @event);
    }
}
