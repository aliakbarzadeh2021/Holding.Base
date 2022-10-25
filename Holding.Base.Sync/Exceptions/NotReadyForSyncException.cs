using System;

namespace Holding.Base.Sync.Exceptions
{
    public class NotReadyForSyncException : Exception
    {
        public NotReadyForSyncException()
        {
        }

        public NotReadyForSyncException(string message)
            : base(message)
        {
        }


        public NotReadyForSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}