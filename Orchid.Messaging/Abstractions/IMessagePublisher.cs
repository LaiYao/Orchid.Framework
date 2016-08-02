using System.Threading.Tasks;

namespace Orchid.Messaging
{
    public interface IMessagePublisher
    {
        void Publish<TMessage>(TMessage message);
        
        Task PublishAsync<TMessage>(TMessage message);
    }
}