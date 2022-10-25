using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.Implements.Channel;
using Holding.Base.Sync.Implements.SyncStep;
using Holding.Base.Sync.SyncStep;
using Holding.Base.CommandBus;

namespace Holding.Base.Sync.Implements.Installer
{
    public class KernelSyncInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(ISyncStepService<>)).ImplementedBy(typeof(SyncStepService<>)));
        }
    }
}
