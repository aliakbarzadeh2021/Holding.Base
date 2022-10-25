using System;
using Holding.Base.Sync.Enums;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Models
{
    public class SyncDataPacket<TCommand> where TCommand : ICommand
    {

        public SyncDataPacket(Guid id, TCommand command, DateTime crationDateTime, HostType hostType)
        {
            Id = id;
            Command = command;
            HostType = hostType;
            CrationDateTime = crationDateTime;
        }


        public Guid Id { get; set; }
        public TCommand Command { get; set; }
        public string SerializedCommand { get; set; }
        public string CommandType { get; set; }
        public string CommandName { get; set; }
        public SyncResult SyncResult { get; set; }
        public SyncLife SyncLife { get; set; }
        public string SyncError { get; set; }
        public Exception SyncException { get; set; }
        public HostType HostType { get; set; }
        public DateTime CrationDateTime { get; set; }
        public DateTime? SyncDate { get; set; }
        public bool IsSync { get; set; }

    }
}