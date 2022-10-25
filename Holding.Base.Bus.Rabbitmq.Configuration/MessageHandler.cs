using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using Holding.Base.Infrastructure.Messaging;


namespace Holding.Base.Bus.Rabbitmq.Configuration
{
    public class MessageHandler
    {
        private readonly IServiceBus _bus;

        /// <summary>
        /// this constractor will use until u haven't service bus in your project 
        /// </summary>
        protected MessageHandler(){}

        protected MessageHandler(IServiceBus bus)
        {
            _bus = bus;
        }

        public IServiceBus Bus
        {
            get { return _bus; }
        }

        public virtual void HandleResponse(Action action, IConsumeContext respondContext)
        {
            if (action == null)
                throw new ArgumentNullException("action", "Action cannot be null...");

            action.Invoke();
            respondContext.Respond(new AutoReply(true));
        }
    }
}
