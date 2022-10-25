using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Infrastructure.Configuration
{
    public interface IApplicationSettings
    {
        string SqlConnectionString { get; }
        string FileEventSqlConnectionString { get; }
        string SubScriptionDbConnectionString { get; }

        string MongoConnectionString { get; }

        string MongoDatabaseName { get; }

        Language CurrentLanguage { get; }
        
        bool IsReadOnlyDataBase { get; }
        bool IsSyncActive { get; }

    }
}
