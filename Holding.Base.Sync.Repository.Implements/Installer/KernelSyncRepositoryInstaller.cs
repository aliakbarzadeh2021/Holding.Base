using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Repository.Implements.Repositories;
using Holding.Base.Sync.SyncStep;

namespace Holding.Base.Sync.Repository.Implements.Installer
{
    public class KernelSyncRepositoryInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof (IRawDataPacketRepository<>)).ImplementedBy(typeof (RawDataPacketRepository<>)));
            container.Register(Component.For(typeof(ISyncStepRepository<>)).ImplementedBy(typeof(SyncStepRepository<>)));
        }
    }
}
