using System;
using System.ComponentModel;
using Holding.Base.Infrastructure.Messaging;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Models;
using MassTransit;
using MassTransit.RequestResponse.Configurators;

namespace Holding.Base.Bus
{
    public class MasstransitServiceBus : IBus
    {
        private readonly IServiceBus _serviceBus;


        public MasstransitServiceBus(IServiceBus serviceBus)
        {
            _serviceBus = serviceBus;
            
        }
         
          
        public void Publish<TEvent>(TEvent message) where TEvent : class
        {
            _serviceBus.Publish<TEvent>(message);
        }


        public void SendCommand<TCommand>(TCommand command, Action onCompleted = null) where TCommand : CommandBase
        {
            command.Validate();
            _serviceBus.Publish(command);
            
        }


        public void SendCommand<TCommand>(TCommand command, Action<InlineRequestConfigurator<TCommand>> callback) where TCommand : CommandBase
        {
            command.Validate();
            _serviceBus.PublishRequest(command, callback);

            
        }



        public void SendCommand<TCommand>(IConsumeContext<TCommand> command, Action onCompleted = null) where TCommand : CommandBase
        {
            command.Message.Validate();
            _serviceBus.Publish(command);

            
        }


 

    }


}