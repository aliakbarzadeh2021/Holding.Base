using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class EqualityInvalidException : DomainException
    {
        private readonly string _obj1;
        private readonly string _obj2;

        public EqualityInvalidException(string obj1, string obj2)
        {
            _obj1 = obj1;
            _obj2 = obj2;
        }

        public override string Message
        {
            get { return string.Format("'{0}' نمی تواند با '{1}' برابر باشد", _obj1, _obj2); }
        }
    }
}