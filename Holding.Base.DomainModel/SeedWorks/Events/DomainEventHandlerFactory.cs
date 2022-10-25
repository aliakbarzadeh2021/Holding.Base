using System.Collections.Generic;
using Holding.Base.DomainModel.SeedWorks.Services;

namespace Holding.Base.DomainModel.SeedWorks.Events
{
    public class DomainEventHandlerFactory : IDomainEventHandlerFactory
    {
        public IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>( T domainEvent ) where T : IDomainEvent
        {
            return DomainServiceLocator.GetAllInstances<IDomainEventHandler<T>>();
        }
    }

}
