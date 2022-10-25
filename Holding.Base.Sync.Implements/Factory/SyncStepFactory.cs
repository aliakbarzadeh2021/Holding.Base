using System;
using System.Collections.Generic;
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Implements.Factory
{
    public  static class SyncStepFactory
    {
        

        public static SyncStep<TCommand> Create<TCommand>() where TCommand: ICommand
        {
            SyncVersion version = SyncVersionFactory.Create();

            SyncStep<TCommand> syncStep =new SyncStep<TCommand>
            {
                SyncVersion = version,
                Id=Guid.NewGuid(),
                FileSyncItems = new List<FileSyncItem>(),
                SyncDataPackets =new List<SyncDataPacket<TCommand>>(),
                IsFileSyncStarted = false,
                IsSyncStepFiltered = false,
                IsCommandSyncCompeleted = false
            };
            
            return syncStep;
        }
    }
}