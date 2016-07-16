using System.Threading.Tasks;

namespace Orchid.Messaging
{
    public interface IMessageHandler<in TMessage>
    {
        void Handle(TMessage message);
        
        Task HandleAsync(TMessage message);
    }
}