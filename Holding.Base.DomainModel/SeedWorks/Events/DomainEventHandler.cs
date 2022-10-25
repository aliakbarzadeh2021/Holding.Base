using System;
using Holding.Base.DomainModel.SeedWorks.Core;
using Holding.Base.Infrastructure.Messaging;
using MassTransit;

namespace Holding.Base.DomainModel.SeedWorks.Events
{
    public abstract class DomainEventHandler
    {
        private readonly IBus _bus;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        protected DomainEventHandler(Lazy<IUnitOfWork> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected DomainEventHandler(IBus bus, Lazy<IUnitOfWork> unitOfWork)
            : this(unitOfWork)
        {
            _bus = bus;
        }

        public IBus Bus
        {
            get { return _bus; }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork.Value; }
        }
    }
}
