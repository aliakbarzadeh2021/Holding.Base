using System;

namespace Holding.Base.Infrastructure.Exceptions
{
    public class ExceptionHelper
    {
        public static Exception GetInnerMostException( Exception error )
        {
            while ( error.InnerException != null )
                error = error.InnerException;

            return error;
        }
    }
}
