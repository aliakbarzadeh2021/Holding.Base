using System;
using Holding.Base.Event.General.SmsEvents.Dto;
using Holding.Base.Infrastructure.Enums;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.SmsEvents
{
    /// <summary>
    /// رویداد ارسال یک پیامک برای چند شماره تلفن
    /// </summary>
    public interface ISendMultiSmsEvent : IMessage
    {
        ISendMultiSmsItem[] SmsContente { get; }

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