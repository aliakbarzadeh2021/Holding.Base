using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class InUseException : DomainException
    {
        private readonly string _objectName;

        public InUseException(string objectName)
        {
            _objectName = objectName;
        }

        public override string Message
        {
            get { return string.Format("{0} ����� ������� ���", _objectName); }
        }
    }
}