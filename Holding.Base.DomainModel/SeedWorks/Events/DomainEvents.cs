using Holding.Base.DomainModel.SeedWorks.Services;

namespace Holding.Base.DomainModel.SeedWorks.Events
{
    public static class DomainEvents
    {
        public static readonly IDomainEventHandlerFactory DomainEventHandlerFactory;
        static DomainEvents()
        {
            DomainEventHandlerFactory = DomainServiceLocator.GetInstance<IDomainEventHandlerFactory>();
        }

        public static void Raise<T>( T domainEvent ) where T : IDomainEvent
        {
            DomainEventHandlerFactory.GetDomainEventHandlersFor( domainEvent ).ForEach( h => h.Handle( domainEvent ) );
        }
    }

}
