using Castle.Windsor;
using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Holding.Base.Bus.Configuration
{
    public class BusConfiguratorWithDefaultConvention : IBusConfiguratorWithDefaultConvention
    {
        private IServiceBus _dataBus;
        private IWindsorContainer _container;
        public IBusConfiguratorWithDefaultConvention AsDefault( IMessageQueue queue )
        {
            _dataBus = ServiceBusFactory.New( config =>
            {
                config.UseMsmq( msmqConfig =>
                {
                    msmqConfig.Configurator.ReceiveFrom( queue.Address );
                    msmqConfig.VerifyMsmqConfiguration();
                    msmqConfig.UseMulticastSubscriptionClient();
                } );

                config.UseLog4Net();

                config.UseJsonSerializer();
                config.SetPurgeOnStartup( false );
                config.SetDefaultRetryLimit( 2 );
                config.SetConcurrentConsumerLimit( 4 );
                config.Subscribe( subscriber => subscriber.LoadFrom( _container ) );
            } );
            return this;
        }


        public IBusConfiguratorWithDefaultConvention LoadConsumerFrom( IWindsorContainer container )
        {
            _container = container;
            return this;
        }


        public IServiceBus CreateInstance()
        {
            return _dataBus;
        }
    }
}
