namespace Holding.Base.DomainModel.Events
{
    public interface IDomainEventHandler<in T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
