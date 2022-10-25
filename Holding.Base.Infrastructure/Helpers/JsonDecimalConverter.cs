using System;
using System.Globalization;
using Newtonsoft.Json;


namespace Holding.Base.Infrastructure.Helpers
{
    public class JsonDecimalConverter : JsonConverter
    {
        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
                writer.WriteValue("");
            else
                writer.WriteValue(string.Format("{0:0.##}", value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var s = reader.Value.ToString();
            if (string.IsNullOrEmpty(s))
                return (object) 0;
            decimal result;

            if (Decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                return (object) result;
            }
            else
            {
                if (Decimal.TryParse(s, out result))
                    return (object) result;
                else
                    return (object) 0;
            }


        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}