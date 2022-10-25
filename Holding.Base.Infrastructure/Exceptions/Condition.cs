using Holding.Base.Infrastructure.Domain;
using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class Condition
    {
        public static void Requires( bool condition , string faultMessage )
        {
            if ( !condition )
                throw new DomainException( faultMessage );
        }

        public static void Requires<TException>( bool condition , string faultMessage ) where TException : Exception
        {
            if ( !condition )
                throw ( TException )Activator.CreateInstance( typeof( TException ) , faultMessage );
        }
    }
}
