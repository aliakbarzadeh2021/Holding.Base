using System;
using System.Linq;

namespace Holding.Base.Infrastructure.Domain
{
    /// <summary>
    /// Base class for value objects in domain.
    /// Value
    /// </summary>
    /// <typeparam name="TValueObject">The type of this value object</typeparam>
    public class ValueObjectBase<TValueObject> : IEquatable<TValueObject>
        where TValueObject : ValueObjectBase<TValueObject>
    {
        #region IEquatable and Override Equals operators

        /// <summary>
        /// <see cref="M:System.Object.IEquatable{TValueObject}"/>
        /// </summary>
        /// <param name="other"><see cref="M:System.Object.IEquatable{TValueObject}"/></param>
        /// <returns><see cref="M:System.Object.IEquatable{TValueObject}"/></returns>
        public virtual bool Equals(TValueObject other)
        {
            if ((object)other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            //compare all public properties
            var publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                return publicProperties.All(p =>
                {
                    var left = p.GetValue(this, null);
                    var right = p.GetValue(other, null);

                    if (left == null && right == null)
                        return true;
                    if (left == null || right == null)
                        return false;
                    if (typeof(TValueObject).IsAssignableFrom(left.GetType()))
                    {
                        //check not self-references...
                        return ReferenceEquals(left, right);
                    }
                    return left.Equals(right);
                });
            }
            return true;
        }
        /// <summary>
        /// <see cref="M:System.Object.Equals"/>
        /// </summary>
        /// <param name="obj"><see cref="M:System.Object.Equals"/></param>
        /// <returns><see cref="M:System.Object.Equals"/></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = obj as ValueObjectBase<TValueObject>;

            if ((object)item != null)
                return Equals((TValueObject)item);
            return false;
        }
        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        public override int GetHashCode()
        {
            var hashCode = 31;
            var changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            var publicProperties = GetType().GetProperties();

            if (!publicProperties.Any()) return hashCode;

            foreach (var value in publicProperties.Select(item => item.GetValue(this, null)))
            {
                if (value != null)
                {

                    hashCode = hashCode * ((changeMultiplier) ? 59 : 114) + value.GetHashCode();

                    changeMultiplier = !changeMultiplier;
                }
                else
                    hashCode = hashCode ^ (index * 13);//only for support {"a",null,null,"a"} <> {null,"a","a",null}
            }

            return hashCode;
        }

        public static bool operator ==(ValueObjectBase<TValueObject> left, ValueObjectBase<TValueObject> right)
        {
            if (Equals(left, null))
                return (Equals(right, null));
            return left.Equals(right);
        }

        public static bool operator !=(ValueObjectBase<TValueObject> left, ValueObjectBase<TValueObject> right)
        {
            return !(left == right);
        }

        #endregion

        protected virtual void Validate()
        {
            //AddValidationAttributeBrokenRules();

        }
    }
}
