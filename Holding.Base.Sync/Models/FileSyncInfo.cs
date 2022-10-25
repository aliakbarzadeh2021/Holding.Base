using System;
using Holding.Base.Sync.Dtos;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Sync.Models
{
    public class FileSyncInfo : IFileSyncInfo
    {
        public Guid Id { get; set; }

        public SyncState SyncState { get; set; }

        public int SyncFilesCount { get; set; }

        public int SuccessSyncFilesCount { get; set; }

        public DateTime SyncDateTime { get; set; }

    }
}
