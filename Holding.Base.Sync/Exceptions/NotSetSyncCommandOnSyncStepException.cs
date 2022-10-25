using System;

namespace Holding.Base.Sync.Exceptions
{
    public class NotSetSyncCommandOnSyncStepException : Exception
    {
        public override string Message
        {
            get { return "ابتدا باید مرحله تنظیم داده های سینک کلاینت و سرور به سینک استپ انجام گردد"; }
        }
    }
}