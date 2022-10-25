using System;
using System.Collections.Generic;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.QuestionBank
{
	public interface IExamKeyChangedEvent : IMessage
    {
		IList<Guid> ChangedExamIds { get; set; }
    }
}
