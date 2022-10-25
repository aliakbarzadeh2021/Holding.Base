using Magnum;
using System;

namespace Holding.Base.Bus.Configuration
{  
    public class ErrorOccurredMessage
    {       
        public ErrorOccurredMessage( Guid corrleationId , string message , string details )
        {
            FaultDetails = details;
            FaultMessage = message;
            CorrelationId = corrleationId;       
        }
        public string FaultMessage { get; private set; }
        public Guid CorrelationId { get;  set; }
        public string FaultDetails { get; private set; }    
    }
}
