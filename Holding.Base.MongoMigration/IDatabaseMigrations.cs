using Holding.Base.MongoMigration.Migrations;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration
{
    /// <summary>
    /// Represent the existing database migrations and allow to apply new migrations 
    /// </summary>
    /// <remarks>For internal using only</remarks>
    internal interface IDatabaseMigrations
    {
        void SaveMigrationHistory(MigrationHistory migrationHistory);
        
        IMongoDatabase GetDatabase();
    }
}