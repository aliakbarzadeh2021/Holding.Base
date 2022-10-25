using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Implements.Channel
{
    internal class RawSyncChannel<TCommand> : IRawSyncChannel<TCommand> where TCommand : ICommand
    {

        private readonly IRawDataPacketRepository<TCommand> _rawDataPacketRepository;
        private IList<RawDataPacket<TCommand>> _rawPackets;
        

        public IList<RawDataPacket<TCommand>> RawPackets
        {
            get
            {
                return _rawPackets;
            }
        }

        
        public RawSyncChannel(IRawDataPacketRepository<TCommand> rawDataPacketRepository)
        {
            _rawDataPacketRepository = rawDataPacketRepository;
        }


        public IList<RawDataPacket<TCommand>> GetRawDataPackets(SyncVersion syncVersion)
        {
            var oldRawDataPackets = _rawPackets= _rawDataPacketRepository.FindAll().Where(i => i.SyncVersion == syncVersion).ToList();

            if (oldRawDataPackets!=null && !oldRawDataPackets.Any())
            {
                return oldRawDataPackets;
            }

            var newRawDataPackets = _rawDataPacketRepository.FindAll().Where(i => i.SyncVersion == null).ToList();

            foreach (var dataPacket in newRawDataPackets)
            {
                dataPacket.SyncVersion = syncVersion;
            }

            _rawDataPacketRepository.SaveAll(newRawDataPackets);

            return _rawPackets = newRawDataPackets;
        }
        

    }
}