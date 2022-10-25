using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Holding.Base.Infrastructure.Helpers
{
    public class GregorianDateConverter : DateTimeConverterBase
    {
        public override bool CanConvert( Type objectType )
        {
            return true;
        }

        public override object ReadJson( JsonReader reader , Type objectType , object existingValue , JsonSerializer serializer )
        {
            try
            {
                if ( reader.Value == null || String.IsNullOrEmpty( reader.Value.ToString() ) )
                    return null;

                DateTime value;
                if ( DateTime.TryParseExact( reader.Value.ToString() , "dd/MM/yyyy" , CultureInfo.InvariantCulture , DateTimeStyles.None , out value ) )
                    return value;

                var persianDatespl = reader.Value.ToString().Split( '/' );
                var year = int.Parse( persianDatespl[ 0 ] );
                var month = int.Parse( persianDatespl[ 1 ] );
                var day = int.Parse( persianDatespl[ 2 ] );

                return new PersianCalendar().ToDateTime( year , month , day , 0 , 0 , 0 , 0 );
            }
            catch ( Exception )
            {
                return reader.Value;
            }
        }

        public override void WriteJson( JsonWriter writer , object value , JsonSerializer serializer )
        {
            writer.WriteValue( ( ( DateTime )value ).ToString( "dd/MM/yyyy" ) );
        }
    }
}
