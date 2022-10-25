using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using MongoDB.Driver;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repository.Implements.Repositories
{
    internal class EmptyRawDataPacketRepository<TCommand> : IRawDataPacketRepository<TCommand> where TCommand : ICommand
    {
        public MongoDatabase MongoDatabase { get; private set; }


        public void Add(RawDataPacket<TCommand> commandPacket)
        {
            
        }

        public void Remove(Guid commandPacketId)
        {
            
        }

        public RawDataPacket<TCommand> FindBy(Guid id)
        {
            return null;
        }

        public void Save(RawDataPacket<TCommand> commandPacket)
        {
            
        }

        public void SaveAll(IEnumerable<RawDataPacket<TCommand>> commandPackets)
        {
            
        }

        public IQueryable<RawDataPacket<TCommand>> FindAll()
        {
            return null;
        }

        public IQueryable<RawDataPacket<TCommand>> FindBy(params Guid[] ids)
        {
            return null;
        }
    }
}