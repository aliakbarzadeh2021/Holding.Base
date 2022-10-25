using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.CommandBus.Handling;
using Holding.Base.CommandBus.Implements;
using Holding.Base.CommandBus.Logging;
using Holding.Base.Infrastructure.Logging;

namespace Holding.Base.CommandBus.Installer
{
    public class CommandBusInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            container.Register(Component.For<ILogger>().ImplementedBy<Log4NetLogger>().LifestyleSingleton());
            container.Register(Component.For<ICommandBus>().ImplementedBy<DefaultBus>().LifestyleSingleton());
            container.Register(Component.For<ICommandHandlerFactory>().Instance(new CastleWindsorBuilder(container)).LifestyleSingleton());
        }
    }
}
