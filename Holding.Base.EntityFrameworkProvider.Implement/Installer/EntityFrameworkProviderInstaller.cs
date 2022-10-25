using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.EntityFrameworkProvider.Implement.Context.Implements;
using Holding.Base.EntityFrameworkProvider.Read.Context.Interfaces;
using Holding.Base.EntityFrameworkProvider.Write.Context.Interfaces;
using Holding.Base.Infrastructure.Configuration;

namespace Holding.Base.EntityFrameworkProvider.Implement.Installer
{
    public class EntityFrameworkProviderInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Classes.FromThisAssembly().Pick().WithServiceAllInterfaces().LifestyleTransient());
            //container.Register(Component.For<IDataContext<>>().ImplementedBy<DataContext<>>())

            if (ApplicationSettingsFactory.GetApplicationSettings().IsReadOnlyDataBase)
            {
                //container.Register(Component.For(typeof(IReadOnlyContext<>)).ImplementedBy(typeof(DataContext<>)).LifestylePerThread());
                container.Register(Component.For(typeof(IReadOnlyContext<>)).ImplementedBy(typeof(DataContext<>)).LifestyleTransient());
            }
            else
            {
                //container.Register(Component.For(typeof(IDataContext<>)).ImplementedBy(typeof(DataContext<>)).LifestylePerThread());
                container.Register(Component.For(typeof(IDataContext<>)).ImplementedBy(typeof(DataContext<>)).LifestyleTransient());
            }
            
            //container.Register(Classes.FromThisAssembly().IncludeNonPublicTypes().BasedOn(typeof(DataContext<>)).WithServiceAllInterfaces().LifestyleTransient());
        }
    }
}
