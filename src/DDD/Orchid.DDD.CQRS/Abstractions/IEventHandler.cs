namespace Orchid.DDD.CQRS.Abstractions
{
    public interface IEventHandler<TCommand> : IHandler<TCommand>
     where TCommand : IEvent
    {
    }
}