
using Castle.Windsor;
using MassTransit;

namespace Holding.Base.Bus.Configuration
{
    public interface IBusConfiguratorWithDefaultConvention
    {
        IBusConfiguratorWithDefaultConvention AsDefault(IMessageQueue queue);        
        IBusConfiguratorWithDefaultConvention LoadConsumerFrom(IWindsorContainer container);
        IServiceBus CreateInstance();
    }
}
