using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.Configurations
{
    public interface ISyncApplicationSettings
    {
        string SyncDatabaseAddress { get; }
        string SyncDatabaseName { get; }
    }
}
