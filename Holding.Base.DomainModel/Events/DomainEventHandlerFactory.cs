using System.Collections.Generic;
using Holding.Base.DomainModel.SeedWorks;

namespace Holding.Base.DomainModel.Events
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        public IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>( T domainEvent ) where T : IDomainEvent
        {
            return DomainServiceLocator.GetAllInstances<IDomainEventHandler<T>>();
        }
    }

}
