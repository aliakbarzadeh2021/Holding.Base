using System;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration.Migrations
{
    internal class VersionedMigration : IDbMigration
    {
        public IDbMigration Migration { get; }
        public MigrationVersion Version { get; }

        public VersionedMigration(IDbMigration migration, MigrationVersion version)
        {
            if (migration == null)
                throw new ArgumentNullException(nameof(migration));
            if (version == null)
                throw new ArgumentNullException(nameof(version));

            Migration = migration;
            Version = version;
        }

        public void Up()
        {
            Migration.Up();
        }

        public void Down()
        {
            Migration.Down();
        }

        public void UseDatabase(IMongoDatabase database)
        {
            Migration.UseDatabase(database);
        }

        public string CollectionName => Migration.CollectionName;

    }
}
