using System;
using System.Linq;
using System.Reflection;
using Holding.Base.MongoMigration.Migrations;

namespace Holding.Base.MongoMigration
{
    /// <summary>
    /// Migration runner. Responsible for running migrations from specified assembly
    /// </summary>
    /// <remarks></remarks>
    public class MigrationRunner
    {
        private readonly IDatabaseMigrations _dbMigrations;
        private readonly IMigrationLocator _locator;
        private Version _fromVersion;
        private Version _toVersion;

        /// <summary>
        /// Cts
        /// </summary>
        /// <param name="migrationAssembly">Assembly that contains migrations</param>
        public MigrationRunner(Assembly migrationAssembly)
        {
            _dbMigrations = new DatabaseMigrations();
            _locator = new MigrationLocator(migrationAssembly, _dbMigrations.GetDatabase());
        }

        public void SetVersion(Version fromVersion, Version toVersion)
        {
            _fromVersion = fromVersion;
            _toVersion = toVersion;
        }

        public void Start()
        {
            if (_fromVersion < _toVersion)
                Upgrade();
            else if(_fromVersion > _toVersion)
                Downgrade();
        }

        /// <summary>
        /// Apply all migrations before specified version.
        /// </summary>
        public void Upgrade()
        {
            var availableMigrations = _locator.GetMigrations();

            var upgradeMigrations = availableMigrations
                .Where(m => m.Version.Version > _fromVersion && m.Version.Version <= _toVersion)
                .OrderBy(m => m.Version.Version).ToList();

            var lastVersion = _fromVersion;
            foreach (var migration in upgradeMigrations)
            {
                Console.Write($"Migrating Up \"{migration.Migration.CollectionName}\" to version \"{migration.Version}\"..."); 

                migration.Up();

                _dbMigrations.SaveMigrationHistory(new MigrationHistory()
                {
                    Id = Guid.NewGuid(),
                    CollectionName = migration.Migration.CollectionName,
                    Description = migration.Version.Description,
                    FromVersion = lastVersion,
                    ToVersion = migration.Version.Version,
                    MigrateDate = DateTime.Now,
                });
                lastVersion = migration.Version.Version;

                Console.WriteLine(" Completed.");
            }
        }

        /// <summary>
        /// Rool back all migrations after specified version.
        /// </summary>
        public void Downgrade()
        {
            var availableMigrations = _locator.GetMigrations();

            var downgradeMigrations = availableMigrations
                .Where(m => m.Version.Version < _fromVersion && m.Version.Version >= _toVersion)
                .OrderByDescending(m => m.Version.Version).ToList();

            var lastVersion = _fromVersion;
            foreach (var migration in downgradeMigrations)
            {
                Console.Write($"Migrating Down \"{migration.Migration.CollectionName}\" to version \"{migration.Version}\"...");

                migration.Down();

                _dbMigrations.SaveMigrationHistory(new MigrationHistory()
                {
                    Id = Guid.NewGuid(),
                    CollectionName = migration.Migration.CollectionName,
                    Description = migration.Version.Description,
                    FromVersion = lastVersion,
                    ToVersion = migration.Version.Version,
                    MigrateDate = DateTime.Now,
                });
                lastVersion = migration.Version.Version;
                Console.WriteLine(" Completed.");
            }
        }
    }
}
