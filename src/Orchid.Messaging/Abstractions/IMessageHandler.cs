using System.Threading.Tasks;

namespace Orchid.Messaging
{
    public interface IMessageHandler<TMessage, TKey> where TMessage : IMessage<TKey>
    {
        void Handle(TMessage message);

        Task HandleAsync(TMessage message);
    }
}