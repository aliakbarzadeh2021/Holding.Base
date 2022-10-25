using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Holding.Base.Infrastructure.Helpers
{
    public class NonGregorianDateConverter : DateTimeConverterBase
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
                return DateTime.Parse( reader.Value.ToString() ).FaDate();
            }
            catch ( Exception )
            {
                return reader.Value;
            }
        }

        public override void WriteJson( JsonWriter writer , object value , JsonSerializer serializer )
        {
            if ( value == null || String.IsNullOrEmpty( value.ToString() ) )
                writer.WriteValue( String.Empty );

            writer.WriteValue( ( ( DateTime )value ).FaDate() );

        }
    }
}
