
namespace Holding.Base.Infrastructure.Messaging
{
    public interface INeedToWaitForReply : IMessage
    {
        void Validate();
    }
}
