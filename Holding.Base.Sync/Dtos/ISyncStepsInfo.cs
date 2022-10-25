using System;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Models;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Sync.Dtos
{
    /// <summary>
    /// با دریافت سینک ورژن یا آی دی، سینک دیتیلز بازگشت داده شود
    /// </summary>
    public interface ISyncStepsInfo
    {
        /*Id = c.Id,*/
        Guid Id { get; set; }

        /*SyncState = c.IsSyncCompeleted ?
                (c.FilteredSyncDataPackets.All(d => d.SyncResult == SyncResult.Success) ?
                    SyncState.Success
                    : SyncState.Warning)
                : SyncState.Error, */
        SyncState SyncState { get; set; } // or Enum

        /*SyncDataPacketsCount = c.FilteredSyncDataPackets.Count,*/
        int SyncDataPacketsCount { get; set; }

        /* SuccessfullDataPacketsCount = c.FilteredSyncDataPackets.Count(s => s.SyncResult == SyncResult.Success), */
        int SuccessfullDataPacketsCount { get; set; }

        /*SyncVersion = c.SyncVersion,*/
        SyncVersion SyncVersion { get; set; }

    }



    internal class SyncStepsInfo : ISyncStepsInfo
    {
        public Guid Id { get; set; }
        public SyncState SyncState { get; set; }
        public int SyncDataPacketsCount { get; set; }
        public int SuccessfullDataPacketsCount { get; set; }
        public SyncVersion SyncVersion { get; set; }

    }
}