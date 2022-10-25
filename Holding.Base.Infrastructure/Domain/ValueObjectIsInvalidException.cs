using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holding.Base.Infrastructure.Domain
{
    class ValueObjectIsInvalidException : Exception
    {
        public ValueObjectIsInvalidException(string message)
            : base(message)
        {

        }
    }
}
