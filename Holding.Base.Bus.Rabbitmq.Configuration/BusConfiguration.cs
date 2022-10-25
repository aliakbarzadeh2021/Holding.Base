using System;
using System.Collections.Generic;
using System.Configuration;
using Castle.Windsor;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;
using MassTransit.Log4NetIntegration;
using MassTransit.SubscriptionConfigurators;

namespace Holding.Base.Bus.Rabbitmq.Configuration
{
    public sealed class BusConfiguration : IBusConfiguration
    {
        private IServiceBus _dataBus;
        private IWindsorContainer _container;
        private List<Action<SubscriptionBusServiceConfigurator>> subscriptionActions;
        public IServiceBus DataBus
        {
            get { return _dataBus; }
        }


		public IServiceBus GetInstance(string queueName, bool enableMessageTracing = true, bool useLog4Net = false, bool useJsonSerializer = true, int defaultRetryLimit = 5)
        {
            _dataBus = ServiceBusFactory.New(config =>
            {

                var serverName = GetConfigValue("rabbitmq-server-name", "localhost");
                var userName = GetConfigValue("rabbitmq-username", "");
                var password = GetConfigValue("rabbitmq-password", "");
                var queueUri = "rabbitmq://" + serverName + "/Bus_" + queueName + "?prefetch=64";

                if (userName != "")
                {
                    config.UseRabbitMq(cnf =>
                    {
                        cnf.ConfigureHost(new Uri(queueUri), h =>
                        {
                            h.SetUsername(userName);
                            h.SetPassword(password);
                        });
                    });
                }
                else
                {
                    config.UseRabbitMq();
                }


                if (enableMessageTracing)
                {
                    config.EnableMessageTracing();
                }

                if (useLog4Net)
                {
                    config.UseLog4Net();
                }

                //config.UseRabbitMqRouting();
                if (useJsonSerializer)
                {
                    config.UseJsonSerializer();
                }
                else
                {
                    config.UseXmlSerializer();
                }



                config.ReceiveFrom(queueUri);


                if (_container != null)
                {
                    config.Subscribe(subscriber => subscriber.LoadFrom(_container));
                }

                foreach (var subscriptionAction in subscriptionActions)
                {
                    config.Subscribe(subscriptionAction);
                }

	            config.SetDefaultRetryLimit(defaultRetryLimit);

            });

            return _dataBus;
        }


	    public IServiceBus GetInstance(string queueName)
        {
            _dataBus = ServiceBusFactory.New(config =>
            {

                var serverName = GetConfigValue("rabbitmq-server-name", "localhost");
                var userName = GetConfigValue("rabbitmq-username", "");
                var password = GetConfigValue("rabbitmq-password", "");
                var queueUri = "rabbitmq://" + serverName + "/Bus_" + queueName + "?prefetch=64";

                if (userName != "")
                {
                    config.UseRabbitMq(cnf =>
                    {
                        cnf.ConfigureHost(new Uri(queueUri), h =>
                        {
                            h.SetUsername(userName);
                            h.SetPassword(password);
                        });
                    });
                }
                else
                {
                    config.UseRabbitMq();
                }


                config.EnableMessageTracing();

                config.UseLog4Net();

                //config.UseRabbitMqRouting();

                config.UseJsonSerializer();


                config.ReceiveFrom(queueUri);


                if (_container != null)
                {
                    config.Subscribe(subscriber => subscriber.LoadFrom(_container));
                }


                if (subscriptionActions != null)
                    foreach (var subscriptionAction in subscriptionActions)
                    {
                        config.Subscribe(subscriptionAction);
                    }


            });

            return _dataBus;
        }





/*
		public IServiceBus GetInstance(string queueName, string serverName, string userName, string password, bool enableMessageTracing = true, bool useLog4Net = false, bool useJsonSerializer = true, bool inLocal = true, int defaultRetryLimit = 5)
        {

            var _serverName = (inLocal) ? "localhost" : serverName;

            _dataBus = ServiceBusFactory.New(config =>
            {
                var queueUri = "rabbitmq://" + _serverName + "/Bus_" + queueName + "?prefetch=64";

                if (userName != "")
                {
                    config.UseRabbitMq(cnf =>
                    {
                        cnf.ConfigureHost(new Uri(queueUri), h =>
                        {
                            h.SetUsername(userName);
                            h.SetPassword(password);
                        });
                    });
                }
                else
                {
                    config.UseRabbitMq();
                }


                if (enableMessageTracing)
                {
                    config.EnableMessageTracing();
                }

                if (useLog4Net)
                {
                    config.UseLog4Net();
                }

                //config.UseRabbitMqRouting();
                if (useJsonSerializer)
                {
                    config.UseJsonSerializer();
                }
                else
                {
                    config.UseXmlSerializer();
                }



                config.ReceiveFrom(queueUri);


                if (_container != null)
                {
                    config.Subscribe(subscriber => subscriber.LoadFrom(_container));
                }

                foreach (var subscriptionAction in subscriptionActions)
                {
                    config.Subscribe(subscriptionAction);
                }

				config.SetDefaultRetryLimit(defaultRetryLimit);

            });

            return _dataBus;
        }
*/
		public IServiceBus GetInstance(string queueName, string serverName, string userName, string password, bool inLocal = true, int defaultRetryLimit = 5)
        {
            var _serverName = (inLocal) ? "localhost" : serverName;

            _dataBus = ServiceBusFactory.New(config =>
            {
                var queueUri = "rabbitmq://" + _serverName + "/Bus_" + queueName + "?prefetch=64";

                if (userName != "")
                {
                    config.UseRabbitMq(cnf =>
                    {
                        cnf.ConfigureHost(new Uri(queueUri), h =>
                        {
                            h.SetUsername(userName);
                            h.SetPassword(password);
                        });
                    });
                }
                else
                {
                    config.UseRabbitMq();
                }


                config.EnableMessageTracing();

                config.UseLog4Net();

                //config.UseRabbitMqRouting();

                config.UseJsonSerializer();


                config.ReceiveFrom(queueUri);

				config.SetDefaultRetryLimit(defaultRetryLimit);


                if (_container != null)
                {
                    config.Subscribe(subscriber => subscriber.LoadFrom(_container));
                }


                if (subscriptionActions != null)
                    foreach (var subscriptionAction in subscriptionActions)
                    {
                        config.Subscribe(subscriptionAction);
                    }


            });

            return _dataBus;
        }





        private string GetConfigValue(string key, string defaultValue)
        {
            string value = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }



        public IBusConfiguration LoadConsumerFrom(IWindsorContainer container)
        {
            _container = container;
            return this;
        }



        public IBusConfiguration AddSubscription(Action<SubscriptionBusServiceConfigurator> action)
        {
            if (subscriptionActions == null)
            {
                subscriptionActions = new List<Action<SubscriptionBusServiceConfigurator>>();
            }
            subscriptionActions.Add(action);
            return this;
        }





    }
}