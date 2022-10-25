
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.CommandSyncService
{
    public interface ISyncAnCommandWithClient<TCommand> where TCommand : ICommand
    {
        void Sync(SyncDataPacket<TCommand> command);
    }
}