using System;

namespace Holding.Base.MongoMigration.Migrations
{
    /// <summary>
    /// Migration version
    /// </summary>
    public class MigrationVersion
    {

        /// <summary>
        /// Sequence number of the current migration
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Text description of the current migration
        /// </summary>
        public string Description { get; }

        public MigrationVersion(Version version)
            : this(version, string.Empty)
        {
        }

        public MigrationVersion(Version version, string description)
        {
            Version = version;
            Description = description;
        }

        public override string ToString()
        {
            return Version.ToString();
        }
    }
}
