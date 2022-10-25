using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Factory;
using Holding.Base.Sync.Filters;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Strategy;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Channel
{
    public class RawSyncDataReseter<TCommand> : IRawSyncDataReseter
        where TCommand : ICommand
    {

        private readonly IRawDataPacketRepository<TCommand> _rawDataPacketRepository;
        private IRawSyncDataReadStrategy _readStrategy;

        public RawSyncDataReseter(IRawDataPacketRepository<TCommand> rawDataPacketRepository)
        {
            _rawDataPacketRepository = rawDataPacketRepository;
        }


        public void SetRawSyncDataReadStrategy(IRawSyncDataReadStrategy readStrategy)
        {
            _readStrategy = readStrategy;
        }

        public void Reset()
        {
            var newFilters = _readStrategy.Read();
            newFilters.Add(new EqFilter("SyncVersion.Version", null));
            var newFilterDocument = FilterFactory.Build(newFilters);

            var bsonRawPackets = _rawDataPacketRepository.FindAll(newFilterDocument);

            if (bsonRawPackets.Any())
            {
                var rawPackets = new List<RawDataPacket<TCommand>>();
                foreach (var bsonRawPacket in bsonRawPackets)
                {
                    try
                    {
                        var rawPacket = BsonSerializer.Deserialize<RawDataPacket<TCommand>>(bsonRawPacket);
                        rawPackets.Add(rawPacket);
                    }
                    catch (Exception exception)
                    {
                        // todo by a.ammari: Log exception to somewhere for checking it later...
                        Console.WriteLine(exception);
                    }
                }

                var currentDateTime = DateTime.Now;
                foreach (var rawDataPacket in rawPackets)
                {
                    rawDataPacket.SyncVersion = new SyncVersion(Guid.Empty.ToString(), currentDateTime);
                    _rawDataPacketRepository.Save(rawDataPacket);
                }

            }
        }


    }

    public interface IRawSyncDataReseter
    {
        void SetRawSyncDataReadStrategy(IRawSyncDataReadStrategy readStrategy);
        void Reset();
    }
}