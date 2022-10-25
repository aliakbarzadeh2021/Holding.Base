using MongoDB.Bson;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration.Migrations
{
    /// <summary>
    /// Base class for mongo migrations
    /// </summary>
    public abstract class Migration : IDbMigration
    {
        protected IMongoDatabase Database { get; private set; }
        public abstract void Up();
        public abstract void Down();
        public string CollectionName { get; }

        protected Migration(string collectionName)
        {
            CollectionName = collectionName;
        }

        void IDbMigration.UseDatabase(IMongoDatabase database)
        {
            Database = database;
        }
        
        protected IMongoCollection<BsonDocument> GetCollection()
        {
            return Database.GetCollection<BsonDocument>(CollectionName);
        }       
    }
}
