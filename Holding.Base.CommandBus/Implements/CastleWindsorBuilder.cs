using System;
using Castle.Windsor;
using Holding.Base.CommandBus.Handling;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.CommandBus.Implements
{
    public class CastleWindsorBuilder : ICommandHandlerFactory {
        private readonly IWindsorContainer _container;

        public CastleWindsorBuilder(IWindsorContainer container)
        {
            this._container = container;
        }

        public ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : ICommand {
            return _container.Resolve<ICommandHandler<TCommand>>();
        }

        public ICommandHandler GetHandlerFor(Type commandType)
        {
            return (ICommandHandler)_container.Resolve(commandType);
        }

        //public IRawDataPacketRepository<TCommand> GetRawDataPacketRepository<TCommand>() where TCommand : ICommand
        //{
        //    return _container.Resolve<IRawDataPacketRepository<TCommand>>();
        //}
    }
}
