using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Holding.Base.Sync.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected readonly ISyncApplicationSettings _syncSettings;
        protected readonly MongoDatabase _database;

        protected MongoCollection<T> _collection;

        public Repository()
        {
            _syncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
            var setting = MongoServerSettings.FromUrl(new MongoUrl(_syncSettings.SyncDatabaseAddress));
            var server = new MongoServer(setting);
            _database = server.GetDatabase(_syncSettings.SyncDatabaseName);
        }


        /*public List<T> FindByOwnerId(Guid id)
        {
            var filter =
                Builders<T>.Filter.And(
                    Builders<T>.Filter.Eq("Command.SchoolId", BsonBinaryData.Create(id)));

            return _collection.Find(filter).ToList();
        }*/


        public void Add(T data)
        {
            _collection.Insert(data);
        }


        public void Remove(Guid dataId)
        {
            _collection.Remove(Query.EQ("_id", BsonValue.Create(dataId)));
        }


        public T FindBy(Guid id)
        {
            return _collection.FindOneById(BsonValue.Create(id));
        }


        public void Save(T data)
        {
            _collection.Save(data);
        }


        public void SaveAll(IEnumerable<T> data)
        {
            foreach (var packet in data)
            {
                _collection.Save(packet);
            }
        }


        public IQueryable<T> FindAll()
        {
            return _collection.AsQueryable();
        }


        public IQueryable<T> FindBy(params Guid[] ids)
        {
            return _collection.Find(Query.In("_id", ids.Select(id => BsonValue.Create(id)))).AsQueryable();
        }

    }
}