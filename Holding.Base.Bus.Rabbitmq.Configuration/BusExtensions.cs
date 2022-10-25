using System;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;
using MassTransit.RequestResponse.Configurators;

namespace Holding.Base.Bus.Rabbitmq.Configuration
{
    public static class BusExtensions
    {

        
        public static void SendCommand<TCommand>(this IServiceBus bus, TCommand command) where TCommand : CommandBase
        {
            command.Validate();
            bus.Publish(command);
            
        }

        public static void SendCommand<TCommand>(this IServiceBus bus, TCommand command, Action<InlineRequestConfigurator<TCommand>> callback) where TCommand : CommandBase
        {
            command.Validate();
            bus.PublishRequest(command, callback);

        }



        
    }



    
}