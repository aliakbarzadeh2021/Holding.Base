using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.DomainModel.ValueObjects
{

    /// <summary>
    /// کلاس پایه تحصیلی
    /// </summary>
    public class Level : ValueObjectBase<Level>
    {
        /// <summary>
        /// تابع سازنده
        /// </summary>
        /// <param name="name"></param>
        /// <param name="no"></param>
        public Level(string name, int no)
        {
            Name = name;
            No = no;
        }

        /// <summary>
        /// نام پایه تحصیلی
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// شماره تحصیلی
        /// </summary>
        public int No { get; private set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
