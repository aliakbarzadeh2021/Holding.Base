using System;

namespace Holding.Base.MongoMigration.Migrations
{

    public class MigrationHistory
    {
        /// <summary>
        /// Migration history id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Version befor migration executed 
        /// </summary>
        public Version FromVersion { get; set; }

        /// <summary>
        /// Version after migration executed
        /// </summary>
        public Version ToVersion { get; set; }

        /// <summary>
        /// Name of collection that migration executed on it
        /// </summary>
        public string CollectionName { get; set; }

        /// <summary>
        /// Name of aggregate that migration executed on it
        /// </summary>

        public string Description { get; set; }

        /// <summary>
        /// Migration Execution date
        /// </summary>
        public DateTime MigrateDate { get; set; }


    }
}
