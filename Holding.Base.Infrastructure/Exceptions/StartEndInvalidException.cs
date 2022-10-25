using Holding.Base.Infrastructure.Domain;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class StartEndInvalidException : DomainException
    {
        private readonly string _start;
        private readonly string _end;

        public StartEndInvalidException(string start,string end)
        {
            _start = start;
            _end = end;
        }

        public override string Message
        {
            get { return string.Format("'{0}' نمی تواند از '{1}' بزرگتر باشد", _start,_end); }
        }
    }
}