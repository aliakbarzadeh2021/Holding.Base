using MongoDB.Driver;

namespace Holding.Base.MongoMigration.Migrations
{
    /// <summary>
    /// Represent mongo database migration
    /// </summary>
    internal interface IDbMigration
    {
        /// <summary>
        /// Collection Name 
        /// </summary>
        string CollectionName { get; }

        /// <summary>
        /// Execute migration up
        /// </summary>
        void Up();

        /// <summary>
        /// Execute migration down
        /// </summary>
        void Down();

        /// <summary>
        /// Set a mongo database for migration
        /// </summary>
        /// <param name="database"></param>
        void UseDatabase(IMongoDatabase database);
    }
}
