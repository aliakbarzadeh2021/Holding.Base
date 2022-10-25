using System;
using System.Collections.Generic;
using Holding.Base.CommandBus.Messages;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.Dtos
{
    /// <summary>
    /// با دریافت تعداد، به همان میزان آخرین سینک اینفو ها برحسب زمان بازگشت داده شود
    /// </summary>
    public interface ISyncStepDetails<TCommand> where TCommand : ICommand
    {
        Guid Id { get; set; }
        SyncVersion SyncVersion { get; set; }
        IList<SyncDataPacket<TCommand>> FilteredSyncDataPackets { get; set; }
    }


    internal class SyncStepDetails<TCommand> : ISyncStepDetails<TCommand> where TCommand : ICommand
    {
        public Guid Id { get; set; }
        public SyncVersion SyncVersion { get; set; }
        public IList<SyncDataPacket<TCommand>> FilteredSyncDataPackets { get; set; }
    }
}