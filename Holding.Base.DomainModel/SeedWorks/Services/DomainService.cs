using Holding.Base.DomainModel.SeedWorks.Core;
using MassTransit;
using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.DomainModel.SeedWorks.Services
{
    public class DomainService
    {
        private readonly IBus _bus;
        private readonly Lazy<IUnitOfWork> _unitOfWork;
        public DomainService(IBus bus)
        {
            _bus = bus;
        }

        public DomainService(IBus bus , Lazy<IUnitOfWork> unitOfWork )
            : this( bus )
        {
            _unitOfWork = unitOfWork;
        }

        protected IBus Bus
        {
            get { return _bus; }
        }

        protected IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork.Value; }
        }
    }
}
