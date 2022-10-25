using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Holding.Base.MongoMigration.Migrations;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration
{
    internal class MigrationLocator : IMigrationLocator
    {
        private readonly Assembly _migrationsAssembly;
        private readonly IMongoDatabase _database;

        public MigrationLocator(Assembly migrationsAssembly, IMongoDatabase database)
        {
            _migrationsAssembly = migrationsAssembly;
            _database = database;
        }

        public List<VersionedMigration> GetMigrations()
        {
            var migrations =
            (
                from type in _migrationsAssembly.GetTypes()
                where typeof(IDbMigration).IsAssignableFrom(type) && !type.IsAbstract
                let attribute = type.GetCustomAttribute<MigrationAttribute>()
                where attribute != null
                orderby attribute.Version
                select new VersionedMigration((IDbMigration) Activator.CreateInstance(type),
                    new MigrationVersion(attribute.Version, attribute.Description))
            ).ToList();

            migrations.ForEach(m => m.Migration.UseDatabase(_database));

            return migrations;
        }
    }
}
