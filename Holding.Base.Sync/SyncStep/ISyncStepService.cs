using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Dtos;
using Holding.Base.Sync.Models;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Service
{
    public interface ISyncStepService<TCommand> where TCommand : ICommand
    {
        IQueryable<SyncStep<TCommand>> GetSyncSteps();
        SyncStep<TCommand> GetSyncStepBySyncVersion(SyncVersion syncVersion);
        SyncVersion CreateNewSyncVersion();




        //For Dto
        IEnumerable<ISyncStepsInfo> GetSyncStepsInfos(int countOfReturnRecord);
        ISyncStepDetails<TCommand> GetSyncStepDetails(SyncVersion syncVersion);

    }
}