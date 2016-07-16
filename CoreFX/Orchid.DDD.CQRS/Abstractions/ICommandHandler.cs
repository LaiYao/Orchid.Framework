namespace Orchid.DDD.CQRS
{
    public interface ICommandHandler<TCommand> : IHandler<TCommand>
     where TCommand : ICommand
    {
    }
}