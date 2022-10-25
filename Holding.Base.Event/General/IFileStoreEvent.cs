using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General
{
    public interface IFileStoreEvent : IMessage
    {
        string FileName { get; }
    }
}