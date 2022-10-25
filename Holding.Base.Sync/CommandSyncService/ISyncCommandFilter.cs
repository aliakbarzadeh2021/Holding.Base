using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.CommandSyncService
{
    public interface ISyncCommandFilter<TCommand> where TCommand : ICommand
    {
        void Filter(IList<SyncDataPacket<TCommand>> syncDataPackets);
    }
}
