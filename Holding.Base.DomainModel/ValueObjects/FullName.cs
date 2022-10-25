using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.DomainModel.ValueObjects
{
    /// <summary>
    /// ترکیب نام و نام خانوادگی
    /// </summary>
    public class FullName : ValueObjectBase<FullName>
    {
        public static readonly FullName Empty = default(FullName); 
        /// <summary>
        /// تابع سازنده
        /// </summary>
        /// <param name="firstName">نام</param>
        /// <param name="lastName">نام خانوادگی</param>
        public FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// نام
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// نام خانوادگی
        /// </summary>
        public string LastName { get; protected set; }

        /// <summary>
        /// بازگرداندن نام و نام خانوادگی بصورت رشته برای مثال محمد حسین عزتی
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}
