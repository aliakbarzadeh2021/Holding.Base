using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException()
        {
        }

        /// <summary>
        /// یک وهله از استثنا ولیدتور کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        public CommandValidationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// یک وهله از استثنا ولیدتور کامند ایجاد میکند
        /// </summary>
        /// <param name="message">پیام خطا</param>
        /// <param name="innerException">خطای درونی</param>
        public CommandValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}