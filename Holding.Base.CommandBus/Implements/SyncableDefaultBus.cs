using System;
using Holding.Base.CommandBus.Handling;
using Holding.Base.CommandBus.Messages;
using Holding.Base.CommandBus.SyncInterfaces;
using Holding.Base.Infrastructure.Events.SyncEvent;
using Holding.Base.Infrastructure.Logging;

namespace Holding.Base.CommandBus.Implements
{
    public class SyncableDefaultBus : DefaultBus
    {
         
        private readonly IRawSyncWriter<ICommand> _rawSyncWriter;


        public SyncableDefaultBus(ILogger logger, ICommandHandlerFactory commandExecutorFactory, IRawSyncWriter<ICommand> rawSyncWriter) 
            : base(logger, commandExecutorFactory)
        {
            _rawSyncWriter = rawSyncWriter;
        }

        
        protected override void OnCommandExecutionComplete(Action onCompleted, ICommand command, DateTime startTime, Exception exception)
        {
            if (exception != null)
            {
                Logger.Error(command.GetLogMessage(), exception);
                throw exception;
            }

            //For Write Raw Sync
            _rawSyncWriter.Write(command, startTime);

            Logger.Info(command.GetLogMessage());
            if (onCompleted != null)
                onCompleted();
        }
    }
}