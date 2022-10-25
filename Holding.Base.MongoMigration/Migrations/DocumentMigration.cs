using System;
using System.Collections.Generic;
using Holding.Base.MongoMigration.Extensions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Holding.Base.MongoMigration.Migrations
{
    /// <summary>
    /// Base class for document migration
    /// Migrate each document in collection
    /// </summary>
    public abstract class DocumentMigration : Migration 
    {
      
        public override void Up()
        {
            Update(UpgradeDocument);
        }

        public override void Down()
        {
            Update(DowngradeDocument);
        }

        protected DocumentMigration(string collectionName) : base(collectionName)
        {
        }

        private void Update(Action<BsonDocument> action)
        {
            var collection = GetCollection();

            var documentsCount = collection.Count(new BsonDocument());
            var documentIndex = 0;

            while (documentIndex < documentsCount)
            {
                var documents = GetDocuments(collection, documentIndex, 100);
                foreach (var document in documents)
                {
                    action(document);
                    collection.Update(document);
                }
                documentIndex += documents.Count;
            }
        }

        protected virtual List<BsonDocument> GetDocuments(IMongoCollection<BsonDocument> collection, int startIndex, int count)
        {
            return collection.FindAll()
                .Skip(startIndex).Limit(count).ToList();
        }

        /// <summary>
        /// Upgrade specified document
        /// </summary>
        /// <param name="document"></param>
        protected abstract void UpgradeDocument(BsonDocument document);

        /// <summary>
        /// Downgrade specified document
        /// </summary>
        /// <param name="document"></param>
        protected abstract void DowngradeDocument(BsonDocument document);
    }
}
