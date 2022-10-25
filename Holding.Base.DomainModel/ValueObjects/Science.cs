using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.DomainModel.ValueObjects
{
    /// <summary>
    /// رشته تحصیلی
    /// </summary>
    public sealed class Science : ValueObjectBase<Science>
    {
        public Science(string title, string category)
        {
            Name = title;
            Category = category;
        }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// گروه دسته بندی
        /// </summary>
        public string Category { get; private set; }

        public static Science Empty
        {
            get
            {
                return new Science(string.Empty, string.Empty);
            }
        }

        public override bool Equals(Science obj)
        {
            if (obj == null)
                return false;

            if (obj.Name == this.Name)
                return obj.Category == this.Category;
            return false;


           // return obj.Name == Name && obj.Category == Category;
        }
    }
}
