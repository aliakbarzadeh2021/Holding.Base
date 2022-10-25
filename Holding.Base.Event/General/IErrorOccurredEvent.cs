using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General
{
    public interface IErrorOccurredEvent : IMessage
    {
        Guid CorrelationId { get; }
        string Message { get; }
        string Details { get; }
        DateTime FaultTime { get; set; }
    }
}
