
using System;

namespace Holding.Base.DomainModel.SeedWorks.Core
{
    public interface IUnitOfWork
    {
        void Attach(Action operation);
        void RegisterEventInToBag<TEvent>(TEvent @event);
        TEvent LoadEventFromBag<TEvent>();
        void Commit();
    }
}
