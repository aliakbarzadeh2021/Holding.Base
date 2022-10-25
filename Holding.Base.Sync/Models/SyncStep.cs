using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Models
{
    public class SyncStep<TCommand> where TCommand : ICommand
    {
        public Guid Id { get; set; }

        public SyncVersion SyncVersion { get; set; }
        public DateTime DateTime { get { return SyncVersion.DateTime; } }

        public IList<SyncDataPacket<TCommand>> ServerSyncDataPackets { get; set; }
        public bool ExistServerSyncDataPackets { get { return ServerSyncDataPackets.Any(); } }

        public IList<SyncDataPacket<TCommand>> ClientSyncDataPackets { get; set; }
        public bool ExistClientSyncDataPackets { get { return ClientSyncDataPackets.Any(); } }

        public IList<SyncDataPacket<TCommand>> FilteredSyncDataPackets { get; set; }

        public bool IsSyncStepFiltered { get; set; }
        public bool IsSyncCompeleted { get; set; }

        public int SyncDataPacketsCount { get { return FilteredSyncDataPackets == null ? 0 : FilteredSyncDataPackets.Count; } }
    }






}