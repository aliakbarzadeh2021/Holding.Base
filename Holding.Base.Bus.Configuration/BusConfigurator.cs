using Castle.Windsor;
using MassTransit;
using MassTransit.Log4NetIntegration;

namespace Holding.Base.Bus.Configuration
{
    public class BusConfigurator : IBusConfigurator
    {
        private IServiceBus _dataBus;
        private IWindsorContainer _container;
        private bool _useMsmq;
        private bool _useLog;
        private bool _purgeOnStartup;
        private int _retryCount = 4;

        public IBusConfigurator UseMsmq()
        {
            _useMsmq = true;
            return this;
        }

        public IBusConfigurator UseLog()
        {
            _useLog = true;
            return this;
        }

        public IBusConfigurator PurgeOnStartup()
        {
            _purgeOnStartup = true;
            return this;
        }

        public IBusConfigurator DefaultRetryLimit( int retryCount )
        {
            _retryCount = retryCount;
            return this;
        }

        public IBusConfigurator AsDefault(IMessageQueue queue)
        {           
             _dataBus = ServiceBusFactory.New( config =>
            {
                if ( _useMsmq )
                    config.UseMsmq( msmqConfig =>
                    {
                        msmqConfig.Configurator.ReceiveFrom( queue.Address );
                        msmqConfig.VerifyMsmqConfiguration();
                        msmqConfig.UseMulticastSubscriptionClient();
                    } );

                if ( _useLog )
                    config.UseLog4Net();
                config.EnableMessageTracing();
                config.UseJsonSerializer();
                config.SetPurgeOnStartup( _purgeOnStartup );
                config.SetDefaultRetryLimit( _retryCount );
                config.SetConcurrentConsumerLimit( 4 );
                config.Subscribe( subscriber => subscriber.LoadFrom( _container ) );
            } );
            return this;
        }

        public IServiceBus CreateInstance()
        {
            return _dataBus;
        }

        public IBusConfigurator LoadConsumerFrom( IWindsorContainer container )
        {
            _container = container;
            return this;
        }

        public static BusConfigurator HostAsService()
        {
            return new BusConfigurator();
        }

        public static BusConfiguratorWithDefaultConvention HostAsServiceWithDefaultConvention()
        {
            return new BusConfiguratorWithDefaultConvention();
        }
    }
}
