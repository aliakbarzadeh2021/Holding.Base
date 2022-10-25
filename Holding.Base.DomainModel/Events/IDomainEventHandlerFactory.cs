using System.Collections.Generic;

namespace Holding.Base.DomainModel.Events
{
    public interface IDomainEventHandlerFactory
    {
        IEnumerable<IDomainEventHandler<T>> GetDomainEventHandlersFor<T>(T domainEvent)
                                                                where T : IDomainEvent;
    }

}
