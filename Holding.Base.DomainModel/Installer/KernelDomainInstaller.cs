using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.DomainModel.SeedWorks.Core;
using Holding.Base.DomainModel.SeedWorks.Services;

namespace Holding.Base.DomainModel.Installer
{
    public class KernelDomainInstaller : IWindsorInstaller
    {
        public void Install( IWindsorContainer container , IConfigurationStore store )
        {
            container.Register( Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>().LifestylePerThread() );
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleSingleton());
            container.Register( Classes.FromThisAssembly().IncludeNonPublicTypes().BasedOn<DomainService>().WithServiceDefaultInterfaces().LifestyleTransient() );
        }
    }
}
