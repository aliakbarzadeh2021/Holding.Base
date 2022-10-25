using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Bus.Response
{
    public interface IResponseCommand : IMessage
    {


        /// <summary>
        /// پیغام برگشتی 
        /// </summary>
        string Message { get; }

        /// <summary>
        /// وضعیت انجام عملیات
        /// </summary>
        AlarmType Type { get; }

        bool Success { get; }
    }
}