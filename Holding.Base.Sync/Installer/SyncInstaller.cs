using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.FileSyncer;
using Holding.Base.Sync.FileSyncInfo;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Service;
using Holding.Base.CommandBus.SyncInterfaces;

namespace Holding.Base.Sync.Installer
{
    public class SyncInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(ISyncStepService<>)).ImplementedBy(typeof(SyncStepService<>)));
            container.Register(Component.For(typeof(IRawSyncWriter<>)).ImplementedBy(typeof(RawSyncDataWriter<>)));
            container.Register(Component.For(typeof(ISyncStepRepository<>)).ImplementedBy(typeof(SyncStepRepository<>)));
            container.Register(Component.For(typeof(IRawDataPacketRepository<>)).ImplementedBy(typeof(RawDataPacketRepository<>)));
            container.Register(Component.For<IFileSyncRepository>().ImplementedBy<FileSyncRepository>());
            container.Register(Component.For<IFileSyncService>().ImplementedBy<FileSyncService>());
            container.Register(Component.For<IFileSyncInfoService>().ImplementedBy<FileSyncInfoService>());
        }
    }
}
