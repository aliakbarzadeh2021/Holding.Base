using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class OutOfRangeInvalidException : DomainException
    {
        private readonly string _val;
        private readonly string _range;

        public OutOfRangeInvalidException(string val, string range)
        {
            _val = val;
            _range = range;
        }

        public override string Message
        {
            get { return string.Format("'{0}' نمی تواند خارج از محدوده '{1}' باشد", _val, _range); }
        }
    }
}