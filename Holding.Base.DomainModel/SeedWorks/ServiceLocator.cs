using Castle.Windsor;

namespace Holding.Base.DomainModel.SeedWorks
{
    public class DomainServiceLocator
    {
        private static IWindsorContainer _mainContainer;

        public static void SetContainer(IWindsorContainer container)
        {
            _mainContainer = container;
        }

        public static T GetInstance<T>()
        {
            return _mainContainer.Resolve<T>();
        }

        public static T[] GetAllInstances<T>()
        {
            return _mainContainer.ResolveAll<T>();
        }
    }
}
