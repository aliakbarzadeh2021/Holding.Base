using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class DataEntityInvalidException : ApplicationException
    {
        private readonly string _inputStringName;
        private readonly string _inputStringType;

        public DataEntityInvalidException(string inputStringName, string inputStringType)
        {
            _inputStringName = inputStringName;
            _inputStringType = inputStringType;
        }

        public override string Message
        {
            get { return string.Format("نوع مجاز برای {0} {1} می باشد.", _inputStringName, _inputStringType); }
        }
    }
}
