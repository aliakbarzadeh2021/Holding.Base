using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class CommandException : Exception
    {
        public CommandException()
        {
        }

        /// <summary>
        /// یک وهله از خطای کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        public CommandException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// یک وهله از خطای کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        /// <param name="innerException">خطای درونی</param>
        public CommandException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}