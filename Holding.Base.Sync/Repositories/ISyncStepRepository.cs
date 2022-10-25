using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Repositories
{
    public interface ISyncStepRepository<TCommand> /*: IRepository<TCommand>*/ where TCommand : ICommand
    {
        void Add(SyncStep<TCommand> data);
        void Remove(Guid dataId);
        SyncStep<TCommand> FindBy(Guid id);
        void Save(SyncStep<TCommand> data);
        void SaveAll(IEnumerable<SyncStep<TCommand>> data);
        IQueryable<SyncStep<TCommand>> FindAll();
    }
}