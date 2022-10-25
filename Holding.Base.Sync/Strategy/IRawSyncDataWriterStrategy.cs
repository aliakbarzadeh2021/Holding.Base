using System;
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Strategy
{
    public interface IRawSyncDataWriterStrategy<TCommand> where TCommand : ICommand
    {
        bool MustBeWrite(TCommand command);
        RawDataPacket<TCommand> CreateRawDataPacket(Guid commandId, TCommand command, DateTime startDateTime);
    }
}