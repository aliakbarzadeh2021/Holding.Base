using Holding.Base.DomainModel.SeedWorks.Core;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;

namespace Holding.Base.DomainModel.SeedWorks.Events
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
        IUnitOfWork UnitOfWork { get; }
        IBus Bus { get; }
    }
}
