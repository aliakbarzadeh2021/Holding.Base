using System;
using System.Transactions;
using Holding.Base.Bus.SimpleServiceBus.Handling;
using Holding.Base.Infrastructure.Messaging;
using Holding.Base.Sync.Models;
using MassTransit;
using MassTransit.Context;
using MassTransit.RequestResponse.Configurators;

namespace Holding.Base.Bus
{
    public class SimpleBus : IBus
    {

        //TODO: Uncommend and use logger
        //private readonly ILogger _logger;

        private readonly ICommandHandlerFactory _commandExecutorFactory;
        

        public SimpleBus(ICommandHandlerFactory commandExecutorFactory)
        {
            _commandExecutorFactory = commandExecutorFactory;
        }

        public void SendCommand<TCommand>(TCommand command, Action onCompleted = null) where TCommand : CommandBase
        {
            var startTime = DateTime.Now;
            Exception exception = null;
            try
            {
                using (var txn = new TransactionScope())
                {
                    var context = new ConsumeContext<TCommand>(ReceiveContext.Empty(), command);
                    GetCommandHandlerContext<TCommand>(command.GetType()).Consume(context);
                    
                    
                    txn.Complete();

                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            OnCommandExecutionCompleteAll(onCompleted, command, startTime, exception);
        }

        public void SendCommand<TCommand>(IConsumeContext<TCommand> command, Action onCompleted = null) where TCommand : CommandBase
        {
            var startTime = DateTime.Now;
            Exception exception = null;
            try
            {
                using (var txn = new TransactionScope())
                {
                    GetCommandHandlerContext<TCommand>(command.GetType()).Consume(command);

                    txn.Complete();
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            OnCommandExecutionCompleteContext(onCompleted, command, startTime, exception);
        }


        //TODO
        public void SendCommand<TCommand>(TCommand command, Action<InlineRequestConfigurator<TCommand>> callback) where TCommand : CommandBase
        {
            command.Validate();
            this.SendCommand(command);
        }


        #region Private Method
        private void OnCommandExecutionCompleteAll(Action onCompleted, CommandBase command, DateTime startTime, Exception exception)
        {
            //_logger.Command(command, exception, DateTime.Now - startTime);

            if (exception != null)
            {
                throw exception;
            }
            if (onCompleted != null)
                onCompleted();
        }


        private void OnCommandExecutionCompleteContext<TCommand>(Action onCompleted,IConsumeContext<TCommand> command, DateTime startTime, Exception exception) where  TCommand: CommandBase
        {
            //_logger.Command(command, exception, DateTime.Now - startTime);

            if (exception != null)
            {
                throw exception;
            }
            if (onCompleted != null)
                onCompleted();
        }


        private Consumes<TCommand>.Context GetCommandHandlerContext<TCommand>(Type commandType) where TCommand : CommandBase
        {
            var executor = _commandExecutorFactory.GetHandlerFor(typeof(Consumes<>.Context).MakeGenericType(commandType));//<TCommand>();
            if (executor == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " + typeof(TCommand).FullName);
            }

            return (Consumes<TCommand>.Context)executor;
        }



        private Consumes<TCommand>.All GetCommandHandlerAll<TCommand>(Type commandType) where TCommand : CommandBase
        {
            var executor = _commandExecutorFactory.GetHandlerFor(typeof(Consumes<>.All).MakeGenericType(commandType));//<TCommand>();
            if (executor == null)
            {
                throw new InvalidOperationException("No command executor registered for command type " + typeof(TCommand).FullName);
            }

            return (Consumes<TCommand>.All)executor;
        }


        #endregion





        /// <summary>
        /// this method for offline mode don't work.
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="message"></param>
        public void Publish<TEvent>(TEvent message) where TEvent : class { }

    }
}