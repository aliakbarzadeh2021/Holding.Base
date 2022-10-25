using System;

namespace Holding.Base.Bus.Response
{
    public class ResponseCommand : IResponseCommand
    {
        public ResponseCommand(Guid correlationId, AlarmType type, string message, bool success)
        {
            CorrelationId = correlationId;
            Type = type;
            Message = message;
            Type = success ? AlarmType.Success : AlarmType.Error;
            Success = success;
        }
        public Guid CorrelationId { get; set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public AlarmType Type { get; private set; }
    }
}