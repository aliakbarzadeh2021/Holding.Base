using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Sync.Dtos
{
    public interface IFileSyncInfo
    {
        Guid Id { get; }
        int SuccessSyncFilesCount { get; }
        DateTime SyncDateTime { get; }
        int SyncFilesCount { get; }
        SyncState SyncState { get; }
    }
}
