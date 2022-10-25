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
    internal class SyncStepRepository<TCommand> : ISyncStepRepository<TCommand> where TCommand : ICommand
    {
        private  readonly ISyncApplicationSettings SyncSettings;
        private  readonly MongoCollection<SyncStep<TCommand>> Collection;
        private  readonly MongoDatabase Database;

        public SyncStepRepository()
        {
            SyncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
            var setting = MongoServerSettings.FromUrl(new MongoUrl(SyncSettings.SyncDatabaseAddress));
            var server = new MongoServer(setting);
            Database = server.GetDatabase(SyncSettings.SyncDatabaseName);
            BsonClassMap.LookupClassMap(typeof(SyncStep<TCommand>));

            if (!CommandAssemblyConfigurator.IsConfigured)
            {
                throw new Exception("ابتدا اسمبلی کامند ها را فراهم کنید");
            }

            List<Type> types=new List<Type>();
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

            Collection = Database.GetCollection<SyncStep<TCommand>>(typeof(SyncStep<TCommand>).Name);
        }

        
        public  void Add(SyncStep<TCommand> syncStep)
        {
            Collection.Insert(syncStep);
        }


        public  void Remove(Guid syncId)
        {
            Collection.Remove(Query.EQ("_id", BsonValue.Create(syncId)));
        }


        public  SyncStep<TCommand> FindBy(Guid id)
        {
            return Collection.FindOneById(BsonValue.Create(id));
        }


        public  void Save(SyncStep<TCommand> syncStep)
        {
            Collection.Save(syncStep);
        }


        public  void SaveAll(IEnumerable<SyncStep<TCommand>> syncSteps)
        {
            foreach (var syncStep in syncSteps)
            {
                Collection.Save(syncStep);
            }
        }

        public  IQueryable<SyncStep<TCommand>> FindAll()
        {
            return Collection.AsQueryable();
        }


        public  IQueryable<SyncStep<TCommand>> FindBy(params Guid[] ids)
        {
            return Collection.Find(Query.In("_id", ids.Select(id => BsonValue.Create(id)))).AsQueryable();
        }


    }
}