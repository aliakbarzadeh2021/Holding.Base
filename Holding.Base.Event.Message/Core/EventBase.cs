using System;
using System.Linq;

namespace Holding.Base.Event.Message.Core
{
    [Serializable]
    public class EventBase
    {
        public EventBase()
        {
            CorrelationId = Guid.NewGuid();
            CreateDate = TimeSpan.FromTicks( DateTime.Now.Ticks );
        }

        public Guid CorrelationId { get; set; }

        public virtual void Validate()
        {
        }

        public TimeSpan CreateDate
        {
            get;
            private set;
        }

        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            var publicProperties = GetType().GetProperties();

            if ( !publicProperties.Any() ) return hashCode;

            foreach ( var value in publicProperties.Select( item => item.GetValue( this , null ) ) )
            {
                if ( value != null )
                {

                    hashCode = hashCode * ( ( changeMultiplier ) ? 59 : 114 ) + value.GetHashCode();

                    changeMultiplier = !changeMultiplier;
                }
                else
                    hashCode = hashCode ^ ( index * 13 );//only for support {"a",null,null,"a"} <> {null,"a","a",null}
            }

            return hashCode;
        }
    }
}
