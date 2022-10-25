using System;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.CommandBus.Handling
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
        ICommandHandler<TCommand> GetHandlerFor<TCommand>() where TCommand : ICommand;

        ICommandHandler GetHandlerFor(Type commandType);
        
        //IRawDataPacketRepository<TCommand> GetRawDataPacketRepository<TCommand>() where TCommand : ICommand;
    }
}
