using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General.LuceneEvent
{
    public interface IQuestionLuceneIndexEvent : IMessage
    {
        Guid Id { get; }
        string Text { get; }
    }
}
