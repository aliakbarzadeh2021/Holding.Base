using System;

namespace Holding.Base.Infrastructure.Messaging
{ 
    [Serializable]
    public class DefaultRespond
    {
        public DefaultRespond( Guid correlationId )
        {
            CorrelationId = correlationId;
        }

        public Guid CorrelationId { get; set; }        
    }
}
