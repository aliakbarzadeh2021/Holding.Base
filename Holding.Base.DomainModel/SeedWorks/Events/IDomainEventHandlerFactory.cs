using System.Collections.Generic;

namespace Holding.Base.DomainModel.SeedWorks.Events
{
    public interface IDomainEventHandlerFactory
    {
        IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent)
                                                                where T : IDomainEvent;
    }

}
