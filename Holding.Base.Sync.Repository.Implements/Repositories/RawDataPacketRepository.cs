using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repository.Implements.Repositories
{
    internal class RawDataPacketRepository<TCommand> : IRawDataPacketRepository<TCommand> where TCommand : ICommand
    {
        private readonly ISyncApplicationSettings _syncSettings;
        private readonly MongoCollection<RawDataPacket<TCommand>> _collection;
        private readonly MongoDatabase _database;

        public RawDataPacketRepository()
        {
            _syncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
            var setting = MongoServerSettings.FromUrl(new MongoUrl(_syncSettings.SyncDatabaseAddress));
            var server = new MongoServer(setting);
            _database = server.GetDatabase(_syncSettings.SyncDatabaseName);

            BsonClassMap.LookupClassMap(typeof(RawDataPacket<TCommand>));

            if (!CommandAssemblyConfigurator.IsConfigured)
            {
                throw new Exception("ابتدا اسمبلی کامند ها را فراهم کنید");
            }

            List<Type> types = new List<Type>();
            foreach (var assembly in CommandAssemblyConfigurator.MappingAssemblies)
            {
                types.AddRange(assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(TCommand))));
            }

            foreach (var type in types)
            {
                //BsonClassMap.RegisterClassMap(new BsonClassMap(type));
                if (!BsonClassMap.IsClassMapRegistered(type))
                    BsonClassMap.LookupClassMap(type);
            }

            _collection = _database.GetCollection<RawDataPacket<TCommand>>(typeof(RawDataPacket<TCommand>).Name);

        }
        

        public  void Add(RawDataPacket<TCommand> commandPacket)
        {
            _collection.Insert(commandPacket);
        }


        public  void Remove(Guid commandPacketId)
        {
            _collection.Remove(Query.EQ("_id", BsonValue.Create(commandPacketId)));
        }


        public  RawDataPacket<TCommand> FindBy(Guid id)
        {
            return _collection.FindOneById(BsonValue.Create(id));
        }


        public  void Save(RawDataPacket<TCommand> commandPacket)
        {
            _collection.Save(commandPacket);
        }


        public  void SaveAll(IEnumerable<RawDataPacket<TCommand>> commandPackets)
        {
            foreach (var packet in commandPackets)
            {
                _collection.Save(packet);
            }
        }


        public  IQueryable<RawDataPacket<TCommand>> FindAll()
        {
            return _collection.AsQueryable();
        }


        public  IQueryable<RawDataPacket<TCommand>> FindBy(params Guid[] ids)
        {
            return _collection.Find(Query.In("_id", ids.Select(id => BsonValue.Create(id)))).AsQueryable();
        }


    }
}
