using System;
using Castle.Windsor;
using Holding.Base.Bus.SimpleServiceBus.Handling;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;

namespace Holding.Base.Bus.SimpleServiceBus
{
    public class IocMapBuilder : ICommandHandlerFactory
    {
        private readonly IWindsorContainer _container;

        public IocMapBuilder(IWindsorContainer container)
        {
            this._container = container;
            this._container = container;
        }

        public Consumes<TCommand>.Context GetHandlerFor<TCommand>() where TCommand : CommandBase
        {
            return _container.Resolve<Consumes<TCommand>.Context>();
        }

        

        public IConsumer GetHandlerFor(Type commandType)
        {
            return (IConsumer)_container.Resolve(commandType);
        }
    }
}
