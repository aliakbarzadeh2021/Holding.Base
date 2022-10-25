using System;
using MassTransit;

namespace Holding.Base.Bus.Configuration
{
    [Obsolete( "This class is obsolete; use the MessageHandler instead." )]
    public class HandlerMessage
    {
        private readonly IServiceBus _bus;

        protected HandlerMessage( IServiceBus bus )
        {
            _bus = bus;
        }

        public IServiceBus Bus
        {
            get { return _bus; }
        }
    }
}
