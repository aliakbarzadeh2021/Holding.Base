using System;
using Castle.Windsor;
using MassTransit;
using MassTransit.SubscriptionConfigurators;

namespace Holding.Base.Bus.Rabbitmq.Configuration
{
    public interface IBusConfiguration
    {
        IServiceBus GetInstance(string queueName);
		IServiceBus GetInstance(string queueName, string serverName, string userName, string password, bool inLocal = true, int defaultRetryLimit = 5);

		IServiceBus GetInstance(string queueName, bool enableMessageTracing = true, bool useLog4Net = false, bool useJsonSerializer = true, int defaultRetryLimit = 5);

        IBusConfiguration LoadConsumerFrom(IWindsorContainer container);
        IServiceBus DataBus { get; }
        IBusConfiguration AddSubscription(Action<SubscriptionBusServiceConfigurator> action);
    }
}