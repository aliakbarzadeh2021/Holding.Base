using System;

namespace Holding.Base.Sync.Exceptions
{
    public class NotFoundPacketsForSyncException : Exception
    {
        public NotFoundPacketsForSyncException()
        {
        }

        public NotFoundPacketsForSyncException(string message)
            : base(message)
        {
        }


        public NotFoundPacketsForSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}