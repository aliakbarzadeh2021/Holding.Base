using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    /// <summary>
    /// مدیریت خطا برای رشته های با مقادیر خالی
    /// </summary>
    public class StringEmptyInvalidException:DomainException
    {
        private readonly string _nullStr;
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="nullStr">رشته با مقدار خالی</param>
        public StringEmptyInvalidException(string nullStr)
        {
            _nullStr = nullStr;
        }

        public override string Message
        {
            get { return string.Format("{0} نمی تواند خالی باشد.", _nullStr); }
        }
    }
}
