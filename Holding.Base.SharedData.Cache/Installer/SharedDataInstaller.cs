using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.QueryModel.Shared;

namespace Holding.Base.SharedData.Cache.Installer
{
    public class SharedDataInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ISharedBaseData>().ImplementedBy<SharedBaseData>().LifestyleSingleton());
        }
    }
}
