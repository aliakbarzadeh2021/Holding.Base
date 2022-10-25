using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.Infrastructure.Configuration;
using Holding.Base.Infrastructure.Querying;
using Holding.Base.QueryModel.Shared;
using Holding.Base.QueryService.SeedWorks.Core;

namespace Holding.Base.QueryService.Installer
{
    public class KernelQueryServiceInstaller : IWindsorInstaller
    {
        public void Install( IWindsorContainer container , IConfigurationStore store )
        {
            DataProviderConfigurator.AddMappingFromAssemblyOf<SchoolType>();
            container.Register( Component.For<IQueryDataProvider>().ImplementedBy<QueryDataProvider>().LifestylePerThread() );
            container.Register( Classes.FromThisAssembly().IncludeNonPublicTypes().BasedOn<QueryServiceBase>().WithServiceDefaultInterfaces().LifestyleTransient() );
        }
    }
}
