using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Dtos;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Factory;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.CommandBus.Messages;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Sync.Service
{
    public class SyncStepService<TCommand> : ISyncStepService<TCommand> where TCommand : ICommand
    {

        private readonly ISyncStepRepository<TCommand> _syncStepRepository;

        public SyncStepService(ISyncStepRepository<TCommand> syncStepRepository)
        {
            _syncStepRepository = syncStepRepository;
        }

        public IQueryable<SyncStep<TCommand>> GetSyncSteps()
        {
            return _syncStepRepository.FindAll();
        }

        public SyncStep<TCommand> GetSyncStepBySyncVersion(SyncVersion syncVersion)
        {
            return _syncStepRepository.FindAll().SingleOrDefault(i => i.SyncVersion.Version == syncVersion.Version);
        }



        public SyncVersion CreateNewSyncVersion()
        {
            return SyncVersionFactory.Create();
        }

        public IEnumerable<ISyncStepsInfo> GetSyncStepsInfos(int countOfReturnRecord)
        {
            return GetSyncSteps().OrderByDescending(i => i.SyncVersion.DateTime).AsQueryable().Take(countOfReturnRecord).ToList().Select(syncStep => new SyncStepsInfo
            {
                Id = syncStep.Id,
                SyncVersion = syncStep.SyncVersion,
                SuccessfullDataPacketsCount = syncStep.FilteredSyncDataPackets.Count(s => s.SyncResult == SyncResult.Success),
                SyncDataPacketsCount = syncStep.FilteredSyncDataPackets.Count,
                SyncState = syncStep.IsSyncCompeleted ? (syncStep.FilteredSyncDataPackets.All(d => d.SyncResult == SyncResult.Success) ? SyncState.AllSuccess : SyncState.SomeError) : SyncState.AllError,

            });
        }

        public ISyncStepDetails<TCommand> GetSyncStepDetails(SyncVersion syncVersion)
        {
            var syncStep = GetSyncStepBySyncVersion(syncVersion);
            if (syncStep != null)
            {
                var detail = new SyncStepDetails<TCommand>
                {
                    Id = syncStep.Id,
                    SyncVersion = syncStep.SyncVersion,
                    FilteredSyncDataPackets = syncStep.FilteredSyncDataPackets

                };
                return detail;
            }
            return null;
        }

    }
}
