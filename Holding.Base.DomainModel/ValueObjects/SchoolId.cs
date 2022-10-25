using System;
using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.DomainModel.ValueObjects
{
    public class SchoolId : ValueObjectBase<SchoolId>
    {
        public SchoolId(Guid identity)
        {
            Identity = identity;
        }

        public Guid Identity { get; set; }
    }
}
