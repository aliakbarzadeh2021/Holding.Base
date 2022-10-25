using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class CommandSyncException:Exception
    {
        public CommandSyncException()
        {
        }

        /// <summary>
        /// یک وهله از خطای کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        public CommandSyncException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// یک وهله از خطای کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        /// <param name="innerException">خطای درونی</param>
        public CommandSyncException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}