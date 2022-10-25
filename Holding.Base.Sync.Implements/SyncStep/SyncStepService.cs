using System.Linq;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.SyncStep;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Implements.SyncStep
{
    internal class SyncStepService<TCommand> : ISyncStepService<TCommand> where TCommand: ICommand
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
            return _syncStepRepository.FindAll().SingleOrDefault(i=>i.SyncVersion.Version==syncVersion.Version);
        }


        public void SaveSyncStep(SyncStep<TCommand> syncStep)
        {
            _syncStepRepository.Save(syncStep);
        }

        
        public void AddSyncStep(SyncStep<TCommand> syncStep)
        {
            _syncStepRepository.Add(syncStep);
        }


    }
}
