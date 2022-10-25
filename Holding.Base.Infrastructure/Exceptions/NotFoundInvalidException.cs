using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class NotFoundInvalidException : DomainException
    {
        private readonly string _objectName;

        public NotFoundInvalidException(string objectName)
        {
            _objectName = objectName;
        }

        public override string Message
        {
            get { return string.Format("{0} یافت نشد.", _objectName); }
        }
    }
}
