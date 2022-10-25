using Castle.Windsor;
using MassTransit;

namespace Holding.Base.Bus.Configuration
{
    public interface IBusConfigurator
    {
        IBusConfigurator UseMsmq();
        IBusConfigurator UseLog();
        IBusConfigurator PurgeOnStartup();
        IBusConfigurator DefaultRetryLimit( int retryCount );
        IBusConfigurator AsDefault(IMessageQueue transports);
        IServiceBus CreateInstance();
        IBusConfigurator LoadConsumerFrom( IWindsorContainer container );
    }
}
