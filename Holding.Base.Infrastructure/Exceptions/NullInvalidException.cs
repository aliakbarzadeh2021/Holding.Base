using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    /// <summary>
    /// مدیریت خطا برای رشته های با مقادیر خالی
    /// </summary>
    public class NullInvalidException:DomainException
    {
        private readonly string _nullObj;
        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="nullObj">شی نال</param>
        public NullInvalidException(string nullObj)
        {
            _nullObj = nullObj;
        }

        public override string Message
        {
            get { return string.Format("'{0}' نمی تواند نال باشد.", _nullObj); }
        }
    }
}
