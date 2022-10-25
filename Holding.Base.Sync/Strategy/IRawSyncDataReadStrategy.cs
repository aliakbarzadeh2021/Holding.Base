using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Filters;
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Strategy
{
    public interface IRawSyncDataReadStrategy
    {
        IList<Filter> Read();
    }
}