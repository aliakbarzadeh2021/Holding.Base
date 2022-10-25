using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    /// <summary>
    /// کلاس برای مدیریت خطا برای رشته هایی با طول نا معتبر
    /// </summary>
    public class StringLengthInvalidException:DomainException
    {
        private readonly string _strBadLength;
        private readonly int _strLength;

        /// <summary>
        /// سازنده کلاس
        /// </summary>
        /// <param name="strBadLength">رشته با طول نامعتبر</param>
        /// <param name="strLength"></param>
        public StringLengthInvalidException(string strBadLength, int strLength)
        {
            _strBadLength = strBadLength;
            _strLength = strLength;
        }

        public override string Message
        {
            get { return string.Format("طول {0} نباید بیشتر از {1} کاراکتر باشد.", _strBadLength, _strLength); }
        }
    }
}
