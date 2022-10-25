using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Holding.Base.Security.Authorization.Seedwork;

namespace Holding.Base.Security.Authorization.Installer
{
    public class AuthorizationInstaller : IWindsorInstaller
    {

        public AuthorizationInstaller(bool clientAuthorizeEnabled)
        {
            AuthorizeConfig.ClientAuthorizeEnabled = clientAuthorizeEnabled;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            AuthorizeConfig.Container = container;
        }
    }
}