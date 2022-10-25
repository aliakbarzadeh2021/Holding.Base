using System;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;

namespace Holding.Base.Bus.Rabbitmq.Configuration
{
    public class EventServiceBus : IEventServiceBus
    {

        public EventServiceBus(IBus serviceBus)
        {
            Bus = serviceBus;
        }

        public IBus Bus { get; private set; }
    }
}