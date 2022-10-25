using System;
using Holding.Base.Event.General;
using Holding.Base.Infrastructure.Messaging;
using Magnum;

namespace Holding.Base.Event.Message.General
{
    public class ErrorOccurredEvent : IErrorOccurredEvent
    {
        private ErrorOccurredEvent( Guid correlationId , string message  , string detail)
        {
            Details = detail;
            Message = message;
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; private set; }

        public string Message { get; private set; }
        public string Details { get; private set; }
        public DateTime FaultTime { get; set; }

        public static ErrorOccurredEvent GenerateDefault( string message )
        {
            return new ErrorOccurredEvent( CombGuid.Generate() , message  , null);
        }

        public static ErrorOccurredEvent GenerateBy( Guid correlationId , string message , string details )
        {
            return new ErrorOccurredEvent( correlationId , message  ,details);
        }

        Guid IMessage.CorrelationId
        {
            get { return CorrelationId; }
            set { CorrelationId = value; }
        }
    }
}

