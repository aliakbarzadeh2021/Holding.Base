using System.Linq;

namespace Holding.Base.Infrastructure.Helpers
{
    public static class HashCreator
    {
        public static int GetHashCode(object obj)
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            var publicProperties = obj.GetType().GetProperties();

            if ( !publicProperties.Any() ) return hashCode;

            foreach ( var value in publicProperties.Select( item => item.GetValue( obj , null ) ) )
            {
                if ( value != null )
                {

                    hashCode = hashCode * ( ( changeMultiplier ) ? 59 : 114 ) + value.GetHashCode();

                    changeMultiplier = !changeMultiplier;
                }
                else
                    hashCode = hashCode ^ ( index * 13 );
            }

            return hashCode;
        }
    }
}
