using System;
using System.Linq;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Strategy;
using Holding.Base.CommandBus.Messages;
using Holding.Base.CommandBus.SyncInterfaces;

namespace Holding.Base.Sync.Channel
{
    public class RawSyncDataWriter<TCommand> : IRawSyncWriter<TCommand> where TCommand : ICommand
    {

        private readonly IRawDataPacketRepository<TCommand> _rawDataPacketRepository;
        private readonly IRawSyncDataWriterStrategy<TCommand> _writerStrategy;

        public RawSyncDataWriter(IRawDataPacketRepository<TCommand> rawDataPacketRepository,
            IRawSyncDataWriterStrategy<TCommand> writerStrategy)
        {
            _rawDataPacketRepository = rawDataPacketRepository;
            _writerStrategy = writerStrategy;
        }

        public void Write(TCommand command, DateTime dateTime)
        {
            if (_writerStrategy.MustBeWrite(command))
            {
                var rawDataPacket = _writerStrategy.CreateRawDataPacket(command.CommandId, command, dateTime);
                var existPacket = _rawDataPacketRepository.FindBy(rawDataPacket.Id);
                if (existPacket == null)
                {
                    _rawDataPacketRepository.Add(rawDataPacket);
                }
            }
        }


    }
}