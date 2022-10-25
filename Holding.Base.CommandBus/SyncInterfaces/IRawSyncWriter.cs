using System;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.CommandBus.SyncInterfaces
{
    public interface IRawSyncWriter<TCommand> where TCommand : ICommand
    {
        void Write(TCommand command, DateTime dateTime);
    }

}
