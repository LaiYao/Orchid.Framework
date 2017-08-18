namespace Orchid.DDD.CQRS
{
    public interface IEventHandler<TCommand> : IHandler<TCommand>
     where TCommand : IEvent
    {
    }
}