
using System;


namespace Holding.Base.Infrastructure.Events.SyncEvent
{



    public class SyncRawDataEventArgs : EventArgs
    {
        public object Value { get; internal set; }
        public DateTime DateTime { get; internal set; }
        public SyncRawDataEventArgs(object value,DateTime dateTime)
        {
            Value = value;
            DateTime = dateTime;
        }
    }



    


}
