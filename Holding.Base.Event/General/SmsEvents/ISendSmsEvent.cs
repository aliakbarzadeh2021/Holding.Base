using System;
using Holding.Base.Infrastructure.Enums;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.SmsEvents
{
    /// <summary>
    /// رویداد ارسال یک پیامک برای یک شماره تلفن
    /// </summary>
    public interface ISendSmsEvent : IMessage
    {
        Guid IndividualPersonId { get; }
        /// <summary>
        /// شماره تلفن همراه گیرنده
        /// </summary>
        string CellPhoneNumber { get; }

        /// <summary>
        /// متن پیامک
        /// </summary>
        string Text { get; }

        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        Guid SchoolId { get; set; }
        /*

                /// <summary>
                /// تنظیمات خط پیامک مدرسه
                /// </summary>
                ISmsPanelConfiguration SmsPanelConfiguration { get; }
        */

    }
}