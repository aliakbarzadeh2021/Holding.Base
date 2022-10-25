using System.Collections.Generic;
using Holding.Base.MongoMigration.Migrations;

namespace Holding.Base.MongoMigration
{
    /// <summary>
    /// Helper class for migrations discovery.
    /// </summary>
    internal interface IMigrationLocator
    {
        /// <summary>
        /// Discovery all migrations in specified assembly between specified versions
        /// </summary>
        /// <returns></returns>
        List<VersionedMigration> GetMigrations();
    }
}