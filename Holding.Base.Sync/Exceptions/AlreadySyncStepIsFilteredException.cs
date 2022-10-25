using System;

namespace Holding.Base.Sync.Exceptions
{
    public class AlreadySyncStepIsFilteredException : Exception
    {
        public override string Message
        {
            get { return "در حال حاضر سینک استپ فیلتر شده است"; }
        }
    }
}