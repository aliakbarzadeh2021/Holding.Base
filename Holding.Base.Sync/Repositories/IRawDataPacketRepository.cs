using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Filters;
using Holding.Base.Sync.Models;
using MongoDB.Bson;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repositories
{
    public interface IRawDataPacketRepository<TCommand> where TCommand : ICommand
    {
        /*void Add(TCommand data);
        void Save(SyncStep<TCommand> data);
        void SaveAll(IEnumerable<SyncStep<TCommand>> data);*/

        void Add(RawDataPacket<TCommand> commandPacket);
        void Remove(Guid commandPacketId);
        RawDataPacket<TCommand> FindBy(Guid id);
        void Save(RawDataPacket<TCommand> commandPacket);
        void SaveAll(IEnumerable<RawDataPacket<TCommand>> commandPackets);
        IList<BsonDocument> FindAll(BsonDocument filterDocument);
        
    }
}