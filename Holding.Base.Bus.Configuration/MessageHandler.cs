using Holding.Base.Infrastructure.Messaging;
using MassTransit;
using System;

namespace Holding.Base.Bus.Configuration
{
    public class MessageHandler
    {
        private readonly IServiceBus _bus;

        protected MessageHandler( IServiceBus bus )
        {
            _bus = bus;
        }
        
        public IServiceBus Bus
        {
            get { return _bus; }
        }

        public virtual void HandleResponse( Action action , IConsumeContext respondContext )
        {
            if ( action == null )
                throw new ArgumentNullException( "action" , "Action cannot be null..." );

            action.Invoke();
            respondContext.Respond(new AutoReply(true));
        }
    }
}
