using System;
using MassTransit.RequestResponse.Configurators;

namespace Holding.Base.Infrastructure.Messaging
{
    public interface IBus
    {
     
        void Publish<TEvent>(TEvent message) where TEvent : class;

        void SendCommand<TCommand>(TCommand command, Action onCompleted = null) where TCommand : CommandBase;
        
        void SendCommand<TCommand>(TCommand command, Action<InlineRequestConfigurator<TCommand>> callback)where TCommand : CommandBase;

    }



}
