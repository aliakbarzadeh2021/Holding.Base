 using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Holding.Base.Sync.Configurations;
 using Holding.Base.Sync.Factory;
 using Holding.Base.Sync.Filters;
 using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Strategy;
 using MongoDB.Bson.Serialization;
 using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Channel
{

    public class RawSyncDataReader<TCommand> :
        ISyncVersionInitializer<TCommand>,
        ISyncRawReader<TCommand>
        where TCommand : ICommand
    {

        private readonly IRawDataPacketRepository<TCommand> _rawDataPacketRepository;


        private RawSyncDataReader(IRawDataPacketRepository<TCommand> rawDataPacketRepository)
        {
            _rawDataPacketRepository = rawDataPacketRepository;
        }

        private SyncVersion _syncVersion;
        private List<RawDataPacket<TCommand>> _rawPackets;


        public static ISyncVersionInitializer<TCommand> Build(IWindsorContainer container)
        {
            return new RawSyncDataReader<TCommand>(container.Resolve<IRawDataPacketRepository<TCommand>>());
        }

        public ISyncRawReader<TCommand> SetSyncVersion(SyncVersion syncVersion)
        {
            _syncVersion = syncVersion;
            return this;
        }

        public IList<RawDataPacket<TCommand>> Read(IRawSyncDataReadStrategy readStrategy, Type destinationType)
        {
            var filters = readStrategy.Read();
            filters.Add(new EqFilter("SyncVersion.Version", _syncVersion.Version));
            var filterDocument = FilterFactory.Build(filters);

            var bsonRawPackets = _rawDataPacketRepository.FindAll(filterDocument);

            if (!bsonRawPackets.Any())
            {
                var newFilters = readStrategy.Read();
                newFilters.Add(new EqFilter("SyncVersion.Version", null));
                var newFilterDocument = FilterFactory.Build(newFilters);

                bsonRawPackets = _rawDataPacketRepository.FindAll(newFilterDocument);
            }

            _rawPackets = new List<RawDataPacket<TCommand>>();
            foreach (var bsonRawPacket in bsonRawPackets)
            {
                try
                {
                    var rawPacket = BsonSerializer.Deserialize<RawDataPacket<TCommand>>(bsonRawPacket);
                    _rawPackets.Add(rawPacket);
                }
                catch (Exception exception)
                {
                    // todo by a.ammari: Log exception to somewhere for checking it later...
                    Console.WriteLine(exception);
                }
            }
            
            foreach (var rawDataPacket in _rawPackets)
            {
                rawDataPacket.SyncVersion = _syncVersion;
                _rawDataPacketRepository.Save(rawDataPacket);
            }

            return _rawPackets;
        }

    }


    public interface ISyncVersionInitializer<TCommand> where TCommand : ICommand
    {
        ISyncRawReader<TCommand> SetSyncVersion(SyncVersion syncVersion);
    }


    public interface ISyncRawReader<TCommand> where TCommand : ICommand
    {
        IList<RawDataPacket<TCommand>> Read(IRawSyncDataReadStrategy readStrategy, Type destinationType);
    }
}