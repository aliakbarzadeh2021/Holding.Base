using Holding.Base.Infrastructure.Exceptions;
using Holding.Base.Infrastructure.Helpers;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Globalization;
using MassTransit.RequestResponse.Configurators;

namespace Holding.Base.Bus.Configuration
{
    public static class BusExtensions
    {
        public static List<Func<IServiceBus, IEndpoint>> EndpointsForPublishEvent;
        public static Func<IServiceBus, IEndpoint> EndpointForSendCommand = bus => bus.GetEndpoint(new Uri(MsmqSettings.AllQueues[DefaultQueues.ServerWrite]));
        public static Func<IServiceBus, IEndpoint> EndpointForWebApp = bus => bus.GetEndpoint(new Uri(MsmqSettings.AllQueues[DefaultQueues.WebApp]));
        static BusExtensions()
        {
            EndpointsForPublishEvent = new List<Func<IServiceBus, IEndpoint>>
            {
                bus => bus.GetEndpoint(new Uri(MsmqSettings.AllQueues[DefaultQueues.ServerRead])),                
                bus => bus.GetEndpoint(new Uri(MsmqSettings.AllQueues[DefaultQueues.Authorization]))
            };
        }

        public static void SendCommand<TCommand>(this IServiceBus bus, TCommand command) where TCommand : CommandBase
        {
            command.Validate();
            EndpointForSendCommand(bus).Send<TCommand>(command, callback =>
            {
                callback.SetFaultAddress(bus.Endpoint.Address.Uri);
                callback.SetHeader("retry-times", Numbers.Zero.ToString(CultureInfo.InvariantCulture));
            });
        }

        public static void SendCommand<TCommand>(this IServiceBus bus, TCommand command, Action callback) where TCommand : class ,INeedToWaitForReply
        {
            if (callback == null)
                throw new NullReferenceException("Callback cannot be null...");

            command.Validate();
            EndpointForSendCommand(bus).Send<TCommand>(command, contextCallback => callback.Invoke());
        }

        public static void PublishEvent<TEvent>(this IServiceBus bus, TEvent @event) where TEvent : class
        {
            EndpointsForPublishEvent.ForEach(msmq => msmq(bus).Send<TEvent>(@event));            
        }

        public static void PublishError<TMessage>(this IServiceBus bus, TMessage message, Exception error) where TMessage : class,IMessage
        {
            var messageContext = bus.MessageContext<TMessage>();
            var pleaseRetry = true;
            var retryCount = 0;
            try
            {
                retryCount = int.Parse(messageContext.Headers["retry-times"]);
                //TODO: For deploy retryCount must be Numbers.Four
                if (retryCount == Numbers.Zero)
                {
                    var fault = ExceptionHelper.GetInnerMostException(error);
                    bus.GetEndpoint(messageContext.FaultAddress).Send(new ErrorOccurredMessage(message.CorrelationId, fault.Message, null));
                    pleaseRetry = false;
                }
            }
            catch
            {
                retryCount = -1;
            }
            finally
            {
                if (pleaseRetry)
                    messageContext.Endpoint.Send(message, callback =>
                    {
                        callback.SetFaultAddress(messageContext.FaultAddress);
                        callback.SetHeader("retry-times", (retryCount + Numbers.One).ToString(CultureInfo.InvariantCulture));
                    });
            }
        }

        public static void WaitForReplyAfterSendCommand<TCommand>(this IServiceBus bus, TCommand command, Action<InlineRequestConfigurator<TCommand>> callback) where TCommand : CommandBase, INeedToWaitForReply
        {
            command.Validate();
            EndpointForSendCommand(bus).SendRequest<TCommand>(command, bus, callback);
        }

        public static void WaitForReplyAfterPublishEvent<TEvent>(this IServiceBus bus, TEvent @event, Action<InlineRequestConfigurator<TEvent>> callback) where TEvent : class , IMessage
        {
            EndpointsForPublishEvent.ForEach(msmq => msmq(bus).SendRequest<TEvent>(@event, bus, callback));
        }

        public static void Reply<TRespond>(this IServiceBus bus, TRespond respond) where TRespond : class
        {
            EndpointForWebApp(bus).Send<TRespond>(respond);
        }
    }
}
