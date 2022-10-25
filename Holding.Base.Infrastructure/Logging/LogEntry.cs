using System;
using System.Diagnostics;
using System.Threading;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Infrastructure.Logging
{
    public class LogEntry
    {
        public string Id { get; set; }
        public Exception Exception { get; set; }
        public string LogLevel { get; set; }
        public StackTrace StackTrace { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Message { get; set; }
        public string UserId
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }

    /// <summary>
    /// لاگ مربوط به فرامین سیستم
    /// </summary>
    public class CommandLogEntry
    {
        public CommandLogEntry()
        {
            UserId = Thread.CurrentPrincipal.Identity.Name;
        }
        public int Id { get; set; }
        public string CommandName { get; set; }     
        public string UserId { get; set; }
        public TimeSpan ExecutionDuration { get; set; }
        public string Message { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public Exception Exception { get; set; }

    }
}