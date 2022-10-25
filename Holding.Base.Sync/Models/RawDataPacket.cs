using System;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Models
{
    public class RawDataPacket<TCommand> where TCommand : ICommand
    {

        public RawDataPacket(Guid id, TCommand command, DateTime crationDateTime)
        {
            Id = id;
            Command = command;
            CrationDateTime = crationDateTime;
        }

        public Guid Id { get; set; }
        public TCommand Command { get; set; }
        public string SerializedCommand { get; set; }
        public string CommandType { get; set; }
        public string CommandName { get; set; }
        public DateTime CrationDateTime { get; set; }
        public SyncVersion SyncVersion { get; set; }

    }
}