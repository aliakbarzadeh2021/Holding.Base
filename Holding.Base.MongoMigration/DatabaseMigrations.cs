using System;
using Holding.Base.MongoMigration.Migrations;
using MongoDB.Driver;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.MongoMigration
{
    internal class DatabaseMigrations : IDatabaseMigrations
    {
        private readonly IMongoDatabase _db;
        private readonly string _historyCollectionName = typeof(MigrationHistory).Name;

        public DatabaseMigrations()
        {
            var applicationSettings = ApplicationSettingsFactory.GetApplicationSettings();
            _db = new MongoClient(applicationSettings.MongoConnectionString)
                .GetDatabase(applicationSettings.MongoDatabaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return _db;            
        }

        public void SaveMigrationHistory(MigrationHistory migrationHistory)
        {
            migrationHistory.Id = Guid.NewGuid();

            _db.GetCollection<MigrationHistory>(_historyCollectionName)
                .InsertOne(migrationHistory);
        }

    }
}
