using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration.Migrations
{
    public abstract class CollectionMigration : Migration
    {
        
        public override void Up()
        {
            Update(UpgradeCollection);
        }

        public override void Down()
        {
            Update(UpgradeCollection);
        }

        protected CollectionMigration(string collectionName) : base(collectionName)
        {
        }

        private void Update(Action<IMongoCollection<BsonDocument>> action)
        {
            var collection = GetCollection();
            action(collection);
        }

        /// <summary>
        /// Upgrade specified document
        /// </summary>
        /// <param name="collection"></param>
        protected abstract void UpgradeCollection(IMongoCollection<BsonDocument> collection);

        /// <summary>
        /// Downgrade specified document
        /// </summary>
        /// <param name="collection"></param>
        protected abstract void DowngradeCollection(IMongoCollection<BsonDocument> collection);
    }
}
