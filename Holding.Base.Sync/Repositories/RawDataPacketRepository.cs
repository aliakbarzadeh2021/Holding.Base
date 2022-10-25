using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Filters;
using Holding.Base.Sync.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Wrappers;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repositories
{
    public class RawDataPacketRepository<TCommand> : IRawDataPacketRepository<TCommand> where TCommand : ICommand
    {
        protected readonly ISyncApplicationSettings SyncSettings;
        protected readonly IMongoDatabase Database;
        protected IMongoCollection<RawDataPacket<TCommand>> Collection;
        protected IMongoCollection<BsonDocument> CollectionRead;


        public RawDataPacketRepository()
        {
            SyncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
            //var setting = MongoServerSettings.FromUrl(new MongoUrl(_syncSettings.SyncDatabaseAddress));
            //var server = new MongoServer(setting);
            Database = new MongoClient(SyncSettings.SyncDatabaseAddress)
                .GetDatabase(SyncSettings.SyncDatabaseName);

            BsonClassMap.LookupClassMap(typeof(RawDataPacket<TCommand>));

            if (!CommandAssemblyConfiguratorForSyncTools.IsConfigured)
            {
                throw new Exception("ابتدا اسمبلی کامند ها را فراهم کنید");
            }

            var types = new List<Type>();
            foreach (var assembly in CommandAssemblyConfiguratorForSyncTools.MappingAssemblies)
            {
                types.AddRange(
                    assembly.GetTypes()
                        .Where(x => x.IsSubclassOf(typeof(TCommand))
                            || typeof(ICommandSubItem).IsAssignableFrom(x)));
            }

            foreach (var type in types)
            {
                //BsonClassMap.RegisterClassMap(new BsonClassMap(type));
                if (!BsonClassMap.IsClassMapRegistered(type))
                    BsonClassMap.LookupClassMap(type);
            }
            CollectionRead = Database.GetCollection<BsonDocument>(typeof(RawDataPacket<TCommand>).Name);
            Collection = Database.GetCollection<RawDataPacket<TCommand>>(typeof(RawDataPacket<TCommand>).Name);

        }



        public void Add(RawDataPacket<TCommand> data)
        {
            Collection.InsertOne(data);
        }


        public void Remove(Guid dataId)
        {
            Collection.DeleteOne(Builders<RawDataPacket<TCommand>>.Filter.Eq(d => d.Id, dataId));
        }


        public RawDataPacket<TCommand> FindBy(Guid id)
        {
            return Collection.Find(Builders<RawDataPacket<TCommand>>.Filter.Eq(x => x.Id, id)).SingleOrDefault();
        }


        public void Save(RawDataPacket<TCommand> entity)
        {
            var filter = Builders<RawDataPacket<TCommand>>.Filter.Eq(d => d.Id, entity.Id);
            Collection.ReplaceOne(filter, entity);
        }


        public void SaveAll(IEnumerable<RawDataPacket<TCommand>> data)
        {
            foreach (var packet in data)
            {
                Save(packet);
            }
        }


        public IList<BsonDocument> FindAll(BsonDocument filterDocument)
        {
            
            var result = CollectionRead.FindSync(filterDocument).ToList();

            return result;
        }

       
        
    }

    





}
