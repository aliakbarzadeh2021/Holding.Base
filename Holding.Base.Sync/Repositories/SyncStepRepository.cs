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
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repositories
{
    public class SyncStepRepository<TCommand> : /*Repository<TCommand>,*/ ISyncStepRepository<TCommand> where TCommand : ICommand
    {
        protected readonly ISyncApplicationSettings _syncSettings;
        protected readonly IMongoDatabase _database;
        protected IMongoCollection<SyncStep<TCommand>> _collection;


        public void Add(SyncStep<TCommand> data)
        {
            _collection.InsertOne(data);
        }


        public void Remove(Guid dataId)
        {
            _collection.DeleteOne(Builders<SyncStep<TCommand>>.Filter.Eq(d => d.Id, dataId));
        }


        public SyncStep<TCommand> FindBy(Guid id)
        {
            return _collection.Find(Builders<SyncStep<TCommand>>.Filter.Eq(x => x.Id, id)).SingleOrDefault();
        }


        public void Save(SyncStep<TCommand> entity)
        {
            var filter = Builders<SyncStep<TCommand>>.Filter.Eq(d => d.Id, entity.Id);
            _collection.ReplaceOne(filter, entity);
        }


        public void SaveAll(IEnumerable<SyncStep<TCommand>> data)
        {
            foreach (var packet in data)
            {
                Save(packet);
            }
        }


        public IQueryable<SyncStep<TCommand>> FindAll()
        {
            return _collection.AsQueryable();
        }

        public SyncStepRepository()
        {
            _syncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
            //var setting = MongoServerSettings.FromUrl(new MongoUrl(_syncSettings.SyncDatabaseAddress));
            //var server = new MongoServer(setting);
            _database = new MongoClient(_syncSettings.SyncDatabaseAddress)
                .GetDatabase(_syncSettings.SyncDatabaseName);

            BsonClassMap.LookupClassMap(typeof(TCommand));

            if (!CommandAssemblyConfiguratorForSyncTools.IsConfigured)
            {
                throw new Exception("ابتدا اسمبلی کامند ها را فراهم کنید");
            }

            List<Type> types = new List<Type>();
            foreach (var assembly in CommandAssemblyConfiguratorForSyncTools.MappingAssemblies)
            {
                types.AddRange(
                    assembly.GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(TCommand)) || typeof(ICommandSubItem).IsAssignableFrom(x)));
            }

            foreach (var type in types)
            {
                //BsonClassMap.RegisterClassMap(new BsonClassMap(type));
                if (!BsonClassMap.IsClassMapRegistered(type))
                    BsonClassMap.LookupClassMap(type);
            }


            _collection = _database.GetCollection<SyncStep<TCommand>>(typeof(SyncStep<TCommand>).Name);
        }


    }
}