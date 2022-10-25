using System;
using System.Collections.Generic;
using System.Linq;

namespace Holding.Base.Infrastructure.Helpers
{
    public static class UsefullExtensions
    {
        public static bool IsEmpty( this Guid value )
        {
            return value == Guid.Empty;
        }

        public static bool IsNullOrEmpty( this Guid? value )
        {
            return value == null || value == Guid.Empty;
        }

        public static bool IsEmpty(this object obj)
        {
            return obj != null;
        }

        public static T Find<T>( this IEnumerable<T> source , Func<T , bool> predicate )
        {
            return source.FirstOrDefault( predicate );
        }

        public static TValue GetValueIfNotDefault<TObj, TValue>(this TObj obj, Func<TObj, TValue> member, TValue defaultValueOnNull = default(TValue))
        {
            if (member == null)
                throw new ArgumentNullException("member");

            if (obj == null)
                throw new ArgumentNullException("obj");

            try
            {
                return member(obj);
            }
            catch (NullReferenceException)
            {
                return defaultValueOnNull;
            }
        }
    }
}
