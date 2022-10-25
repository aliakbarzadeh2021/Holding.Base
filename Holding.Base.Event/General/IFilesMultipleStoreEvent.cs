using System.Collections.Generic;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.General
{
    public interface IFilesMultipleStoreEvent : IMessage
    {
      string[] FileNames { get; }
    }
}