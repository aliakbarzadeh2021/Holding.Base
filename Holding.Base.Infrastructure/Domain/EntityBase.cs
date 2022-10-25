using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Holding.Base.Infrastructure.Domain
{
    public abstract class EntityBase<TId>
    {

        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();
        //[Required]
        public virtual TId Id { get; set; }

        public abstract void Validate();

        public override bool Equals(object entity)
        {
            return entity != null
               && entity is EntityBase<TId>
               && this == (EntityBase<TId>)entity;
        }

        public override int GetHashCode()
        {
            if (this.Id == null)
                return 0;
            return this.Id.GetHashCode();
        }

        public static bool operator ==(EntityBase<TId> entity1,
           EntityBase<TId> entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
            {
                return true;
            }

            if ((object)entity1 == null || (object)entity2 == null)
            {
                return false;
            }

            return entity1.Id.ToString() == entity2.Id.ToString();
        }

        public static bool operator !=(EntityBase<TId> entity1,
            EntityBase<TId> entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
