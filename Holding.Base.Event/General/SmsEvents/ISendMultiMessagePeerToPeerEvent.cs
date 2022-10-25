using System;
using Holding.Base.Event.General.SmsEvents.Dto;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.SmsEvents
{
    public interface ISendMultiMessagePeerToPeerEvent : IMessage
    {
        ISendMultiMessagePeerToPeerItem[] SmsContent { get; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        Guid SchoolId { get; set; }
        /*

                ISmsPanelConfiguration SmsPanelConfiguration { get; }
        */

    }
}