using System;
using MongoDB.Bson;

namespace Holding.Base.Sync.Filters
{
    public class EqFilter: Filter
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public EqFilter(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public EqFilter()
        {
        }

        public override BsonElement CreateFilterElement()
        {
            if (Value == null)
                return new BsonElement(Key, BsonNull.Value);


            var type = Value.GetType();
            if (type == typeof(int))
                return new BsonElement(Key, new BsonInt32((int)Value));
            if (type == typeof(long))
                return new BsonElement(Key, new BsonInt64((long)Value));
            if (type == typeof(bool))
                return new BsonElement(Key, new BsonBoolean((bool)Value));
            if (type == typeof(DateTime))
                return new BsonElement(Key, new BsonDateTime((DateTime)Value));
            if (type == typeof(double))
                return new BsonElement(Key, new BsonDouble((double)Value));
            if (type == typeof(string))
                return new BsonElement(Key, new BsonString((string)Value));
            if (type == typeof(Guid))
                return new BsonElement(Key, new BsonBinaryData((Guid)Value));


            throw new NotSupportedException($"Type {type} is not supported.");
        }
    }
}
