using System;

namespace Holding.Base.DomainModel.Events
{
    public interface IDomainEvent
    {
        DateTime EventDate { get; }
    }
}

