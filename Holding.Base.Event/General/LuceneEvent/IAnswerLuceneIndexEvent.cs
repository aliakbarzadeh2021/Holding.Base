using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.LuceneEvent
{
    public interface IAnswerLuceneIndexEvent : IMessage
    {
        Guid AnswerId { get; }
        string Answer { get; }
    }
}