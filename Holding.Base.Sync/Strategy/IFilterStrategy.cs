using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Strategy
{
    public interface IFilterStrategy<TCommand> where TCommand : ICommand
    {
        void Filter(SyncStep<TCommand> syncStep);
    }
}