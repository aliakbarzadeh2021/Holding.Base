using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Holding.Base.CommandBus.Handling;
using Holding.Base.CommandBus.Messages;
using Holding.Base.Infrastructure.Configuration;
using Holding.Base.Infrastructure.Logging;

//using Nashr.Sorna.Service.Hubs;

namespace Holding.Base.CommandBus.Implements
{
    /// <summary>
    /// Default bus
    /// </summary>
    public class DefaultBus : ICommandBus
    {
        protected readonly ILogger Logger;
        private readonly ICommandHandlerFactory _commandExecutorFactory;

        public DefaultBus(ILogger logger, ICommandHandlerFactory commandExecutorFactory)
        {
            Logger = logger;
            _commandExecutorFactory = commandExecutorFactory;
        }


        public void Send<TCommand>(TCommand command, Action onCompleted) where TCommand : ICommand
        {
            var startTime = DateTime.Now;
            Exception exception = null;
            try
            {
                using (var txn = new TransactionScope())
                {
                    GetCommandHandler<TCommand>(command.GetType()).Handle(command);
                    txn.Complete();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            OnCommandExecutionComplete(onCompleted, command, startTime, exception);
        }


        public void Send<TCommand>(IEnumerable<TCommand> commands, Action onCompleted) where TCommand : ICommand
        {
            if (commands == null || !commands.Any())
                return;

            var t = new Task(() =>
            {
                using (var txn = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(2, 0, 0)))
                {
                    foreach (var command in commands)
                    {
                        Send(command, onCompleted);
                    }
                    txn.Complete();
                }
            });

            t.Start();
        }



        protected virtual void OnCommandExecutionComplete(Action onCompleted, ICommand command, DateTime startTime, Exception exception)
        {
            if (exception != null)
            {
                Logger.Error(command.GetLogMessage(), exception);
                throw exception;
            }


/*

            #region SyncEvent Raise
            if (ApplicationSettingsFactory.GetApplicationSettings().IsSyncActive)
            {
                OnSyncEventRaise(new SyncRawDataEventArgs(command,startTime));
            }
            #endregion
*/


            Logger.Info(command.GetLogMessage());
            if (onCompleted != null)
                onCompleted();
        }

        
        private ICommandHandler<TCommand> GetCommandHandler<TCommand>(Type commandType) where TCommand : ICommand
        {
            var executor = _commandExecutorFactory.GetHandlerFor(typeof(ICommandHandler<>).MakeGenericType(commandType));//<TCommand>();
            if (executor == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " + typeof(TCommand).FullName);
            }

            return (ICommandHandler<TCommand>)executor;
        }
        

    }
}
