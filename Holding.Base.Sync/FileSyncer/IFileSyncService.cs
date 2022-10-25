using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Dtos;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.FileSyncer
{
    public interface IFileSyncService
    {
        IQueryable<FileSync> GetFileSyncsAfterTime(DateTime? startDateTime);
        FileSync GetLastFileSync();
        void SaveFileSync(FileSync fileSync);


        // for Dto
        IEnumerable<IFileSyncInfo> GetFileSyncInfos(int count);
        IFileSyncDetails GetFileSyncDetails(Guid id);
    }
}