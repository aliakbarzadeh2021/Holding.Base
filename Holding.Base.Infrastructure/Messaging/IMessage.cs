using System;

namespace Holding.Base.Infrastructure.Messaging
{
    public interface IMessage
    {
        Guid CorrelationId { get; set; }
    }
}