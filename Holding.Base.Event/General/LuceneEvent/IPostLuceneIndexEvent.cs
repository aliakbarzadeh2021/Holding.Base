using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.LuceneEvent
{
    public interface IPostLuceneIndexEvent : IMessage
    {
        Guid BlogId { get; }
        Guid PostId { get; }
        string Context { get; }
    }
}