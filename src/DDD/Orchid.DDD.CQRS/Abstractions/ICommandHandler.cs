namespace Orchid.DDD.CQRS.Abstractions
{
    public interface ICommandHandler<TCommand> : IHandler<TCommand>
     where TCommand : ICommand
    {
    }
}