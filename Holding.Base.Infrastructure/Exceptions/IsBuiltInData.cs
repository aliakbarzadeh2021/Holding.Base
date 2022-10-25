using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class IsBuiltInData : DomainException
    {
        private readonly string _objectName;

        public IsBuiltInData(string objectName)
        {
            _objectName = objectName;
        }

        public override string Message
        {
            get { return string.Format("{0} داده سیستمی است و قابل تغییرو حذف نمیباشد", _objectName); }
        }
    }
}