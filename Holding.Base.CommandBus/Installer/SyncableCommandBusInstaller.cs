using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.CommandBus.Handling;
using Holding.Base.CommandBus.Implements;
using Holding.Base.CommandBus.Logging;
using Holding.Base.Infrastructure.Logging;

namespace Holding.Base.CommandBus.Installer
{
    /// <summary>
    /// باس با قابلیت سینک و ارسال ایونت در زمان ثبت کامند ها
    /// </summary>
    public class SyncableCommandBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ILogger>().ImplementedBy<Log4NetLogger>().LifestyleSingleton());
            container.Register(Component.For<ICommandBus>().ImplementedBy<SyncableDefaultBus>().LifestyleSingleton());
            container.Register(Component.For<ICommandHandlerFactory>().Instance(new CastleWindsorBuilder(container)).LifestyleSingleton());
        }
    }
}