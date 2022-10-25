using System;
using Castle.Core.Internal;
using Holding.Base.Infrastructure.Exceptions;
using MassTransit.RequestResponse;

namespace Holding.Base.Infrastructure.Messaging
{
    public class CommandBase : IMessage
    {
        public CommandBase()
        {
            CorrelationId = Guid.NewGuid();
            ToSync = false;
        }

        

        public Guid CorrelationId { get; set; }
        
        public bool ToSync { get; set; }

        public virtual void Validate()
        {}
    }

    
}