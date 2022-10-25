using System;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;

namespace Holding.Base.Bus.SimpleServiceBus.Handling
{
    /// <summary>
    /// Represents a Command Executor Factory
    /// </summary>
    public interface ICommandHandlerFactory
    {
        /// <summary>
        /// Returns Executor of a Command  of type TCommand.
        /// </summary>
        /// <typeparam name="TCommand">Command Type</typeparam>
        /// <returns>ICommandExecutor&lt;TCommand&gt;></returns>
        Consumes<TCommand>.Context GetHandlerFor<TCommand>() where TCommand : CommandBase;

        IConsumer GetHandlerFor(Type commandType);
    }
}
