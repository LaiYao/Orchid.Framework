namespace Orchid.Messaging
{
    public interface IMessageSubscriber
    {
        void Subscribe<TMessage>(TMessage message);
    }
}