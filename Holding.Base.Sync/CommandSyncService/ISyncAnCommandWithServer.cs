
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.CommandSyncService
{
    public interface ISyncAnCommandWithServer<TCommand> where TCommand : ICommand
    {
        void Sync(SyncDataPacket<TCommand> command);
    }
}