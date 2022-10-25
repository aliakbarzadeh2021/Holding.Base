using Holding.Base.DomainModel.SeedWorks;

namespace Holding.Base.DomainModel.Events
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
